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
        string query = "SELECT * FROM Presupuestos WHERE idPresupuesto = @id";
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
        string query = "DELETE FROM Presupuestos WHERE idPresupuesto = @id";
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


    public bool ModificarPresupuesto(int id, Presupuesto nuevoPresupuesto)
    {
        string query = "UPDATE Presupuestos SET NombreDestinatario = @NombreDestinatario , FechaCreacion = @FechaCreacion WHERE idPresupuesto = @id ";
        int cantidadFilas = 0;

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            SqliteCommand command = new SqliteCommand(query, conexion);
            command.Parameters.Add(new SqliteParameter("@NombreDestinatario", nuevoPresupuesto.NombreDestinatario));
            command.Parameters.Add(new SqliteParameter("@FechaCreacion", nuevoPresupuesto.FechaCreacion));
            command.Parameters.Add(new SqliteParameter("@id", id));
            cantidadFilas = command.ExecuteNonQuery();
            conexion.Close();
        }
        return cantidadFilas > 0;
    }



     public List<PresupuestosDetalles> ListarDetallePresupuesto(int idPresupuesto)
    {
        List<PresupuestosDetalles> listaDetalle = new List<PresupuestosDetalles>();
        string query = "SELECT * FROM PresupuestosDetalle INNER JOIN Productos USING(idProducto) WHERE  idPresupuesto = @idPres";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand command = new SqliteCommand(query, conexion);
            conexion.Open();
            command.Parameters.Add(new SqliteParameter("@idPres", idPresupuesto));
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Producto producto= new Producto();  
                    var presupuestoDetalle = new PresupuestosDetalles();
                    var productoRepository= new ProductoRepository();
                    presupuestoDetalle.Cantidad= Convert.ToInt32(reader["Cantidad"]);
                    producto.IdProducto= (Convert.ToInt32(reader["idProducto"]));
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Precio = Convert.ToInt32(reader["Precio"]);
                    presupuestoDetalle.CargarProducto(producto);
                    listaDetalle.Add(presupuestoDetalle);
                }
            }
            conexion.Close();

        }
        return listaDetalle;
    }
    

    public void CrearNuevoDetalle(PresupuestosDetalles detalles) 
    {
        string query = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idPres, @idProd, @Cantidad)";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            var command = new SqliteCommand(query, conexion);
            command.Parameters.Add(new SqliteParameter("@idPres", detalles.IdPresupuesto));
            command.Parameters.Add(new SqliteParameter("@idProd", detalles.Producto.IdProducto));
            command.Parameters.Add(new SqliteParameter("@Cantidad", detalles.Cantidad));
            command.ExecuteNonQuery();
            conexion.Close();
        }
    }
}
