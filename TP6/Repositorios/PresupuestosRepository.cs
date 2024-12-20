namespace Repositorios;
using Microsoft.Data.Sqlite;
using Models;

public class PresupuestosRepository : IPresupuestoRepository
{
    private string cadenaDeConexion;

    public PresupuestosRepository(string cadena)
    {
        this.cadenaDeConexion = cadena;
    }

    public bool CrearPresupuesto(Presupuesto presupuesto)
    {
        string query = "INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@nombre, @fecha)";
        int cantidadFilas = 0;

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            SqliteCommand command = new SqliteCommand(query, conexion);
            command.Parameters.Add(new SqliteParameter("@nombre", presupuesto.NombreDestinatario));
            command.Parameters.Add(new SqliteParameter("@fecha", presupuesto.FechaCreacion));
            cantidadFilas = command.ExecuteNonQuery();
            conexion.Close();
        }
        return cantidadFilas > 0;
    }

    public List<Presupuesto> ListarPresupuestos()
    {
        string query = "SELECT * FROM Presupuestos";
        List<Presupuesto> listaPresupuestos = new List<Presupuesto>();

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            SqliteCommand command = new SqliteCommand(query, conexion);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Presupuesto presupuesto = new Presupuesto();
                    presupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                    presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                    presupuesto.FechaCreacion = reader["FechaCreacion"].ToString();
                    listaPresupuestos.Add(presupuesto);
                }
            }
            conexion.Close();
        }
        return listaPresupuestos;
    }

    public Presupuesto ObtenerPresupuestoPorId(int id)
    {
        string query = "SELECT * FROM Presupuestos WHERE id = @id";
        Presupuesto presupuesto = new Presupuesto();

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            SqliteCommand command = new SqliteCommand(query, conexion);
            command.Parameters.Add(new SqliteParameter("@id", id));

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int idPresupuesto = reader.GetInt32(0);
                    string nombreDestinatario = reader.GetString(1);
                    List<PresupuestosDetalles> detalles = new List<PresupuestosDetalles>();

                    string queryDetalles = @"SELECT idProducto,Descripcion,Precio,Cantidad FROM PresupuestosDetalle
                                             INNER JOIN Productos USING(idProducto)
                                             WHERE idPresupuesto = @idPresupuesto";
                
                    SqliteCommand commandDetalles = new SqliteCommand(queryDetalles, conexion);  
                    commandDetalles.Parameters.AddWithValue("@idPresupuesto",idPresupuesto);

                    using (SqliteDataReader reader2 = commandDetalles.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            Producto p = new Producto(reader2.GetInt32(0),reader2.GetString(1),reader2.GetInt32(2));
                            detalles.Add(new PresupuestosDetalles(p,reader2.GetInt32(3)));
                        }
                    }
                    presupuesto = new Presupuesto(idPresupuesto,nombreDestinatario, detalles);
                }
            }
        }
        return presupuesto;
    }
    
    public bool AgregarProductoAPresupuesto(int idPresupuesto, int idProducto, int cantidad)
    {
        string query = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idPresupuesto, @idProducto, @cantidad)";
        int cantidadFilas = 0;

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            SqliteCommand command = new SqliteCommand(query, conexion);
            command.Parameters.Add(new SqliteParameter("@idPresupuesto", idPresupuesto));
            command.Parameters.Add(new SqliteParameter("@idProducto", idProducto));
            command.Parameters.Add(new SqliteParameter("@cantidad", cantidad));
            cantidadFilas = command.ExecuteNonQuery();
            conexion.Close();
        }
        return cantidadFilas > 0;
    }
    
    public bool EliminarPresupuesto(int id)
    {
        string query = "DELETE FROM Presupuestos WHERE id = @id";
        int cantidadFilas = 0;

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            SqliteCommand command = new SqliteCommand(query, conexion);
            command.Parameters.Add(new SqliteParameter("@id", id));
            cantidadFilas = command.ExecuteNonQuery();
            conexion.Close();
        }
        return cantidadFilas > 0;
    }
}
