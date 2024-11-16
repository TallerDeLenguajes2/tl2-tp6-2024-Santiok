namespace Repositorios;
using Microsoft.Data.Sqlite;
using Models;

public class PresupuestosRepository : IRepository<Presupuesto>
{
    private readonly string cadenaDeConexion = "Data Source=bd/Tienda.db;Cache=Shared";

    //Insertar presupuesto.
    public void Insertar(Presupuesto pres)
    {
        using(var connection = new SqliteConnection(cadenaDeConexion))
        {
            connection.Open();

            string query = "INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES ($nom, $fec)";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("$nom", pres.NombreDestinatario);
            command.Parameters.AddWithValue("$fec", pres.FechaCreacion);

            connection.Close();
        }
    }

     //Eliminar presupuesto.
    public void Eliminar(int id)
    {
        using (var connection = new SqliteConnection(cadenaDeConexion))
        {
            connection.Open();

            string query = "DELETE FROM Presupuestos WHERE idPresupuestos = $id_pasado;";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("$id_pasado", id);

            connection.Close();
        }
    }

    //Listar los presupuestos.
    public List<Presupuesto> Listar()
    {
        List<Presupuesto> listaPres = new List<Presupuesto>();
        using (var connection = new SqliteConnection(cadenaDeConexion))
        {
            connection.Open();

            string query = "SELECT * FROM Productos;";

            SqliteCommand command = new SqliteCommand(query, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    var pres = new Presupuesto();
                    pres.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                    pres.NombreDestinatario = reader["NombreDestinatario"].ToString();
                    pres.FechaCreacion = reader["FechaCreacion"].ToString();
                    listaPres.Add(pres);
                }
            }
            connection.Close();
        }
        return listaPres;
    }

    //Modificar un presupuesto.
    public void Modificar(int id, Presupuesto pres)
    {
        using(var connection = new SqliteConnection(cadenaDeConexion))
        {
            connection.Open();

            string query = "UPDATE Presupuetos (NombreDestinatario, FechaCreacion) VALUES ($nom, $fec) WHERE idProductos = $id_pasado;";

            SqliteCommand command = new SqliteCommand(query, connection);

            command.Parameters.AddWithValue("$nom", pres.NombreDestinatario);
            command.Parameters.AddWithValue("$fec", pres.FechaCreacion);
            command.Parameters.AddWithValue("$id_pasado", pres.IdPresupuesto);

            connection.Close();
        }
    }

    //Obtener presupuestos. ----?
    public Presupuesto ObtenerPresupuestos(int id)
    {
       using(var connection = new SqliteConnection(cadenaDeConexion))
       {
        connection.Open();

        string queryPresupuesto = "SELECT idPresupuesto,NombreDestinatario FROM Presupuestos WHERE idPresupuesto = @idPresupuesto";

        SqliteCommand commandPresupuesto = new SqliteCommand(queryPresupuesto, connection);

        commandPresupuesto.Parameters.AddWithValue("@idPresupuesto",id);

        using(SqliteDataReader reader = commandPresupuesto.ExecuteReader())
        {
            while (reader.Reead())
            {
                int idPresupuesto = reader.GetInt32(0);
                string nombreDestinatario = reader.GetString(1);
                List<PresupuestoDetalle> detalles = new List<PresupuestoDetalle>();

                string queryDetalles = @"SELECT idProducto,Descripcion,Precio,Cantidad FROM PresupuestosDetalle
                        INNER JOIN Productos USING(idProducto)
                        WHERE idPresupuesto = @idPresupuesto";   

                SqliteCommand commandDetalles = new SqliteCommand(queryDetalles, connection);  
                commandDetalles.Parameters.AddWithValue("@idPresupuesto",idPresupuesto);

                using (SqliteDataReader reader2 = commandDetalles.ExecuteReader())
                {
                    while (reader2.Read())
                    {
                        Productos p = new Productos(reader2.GetInt32(0),reader2.GetString(1),reader2.GetInt32(2));
                        detalles.Add(new PresupuestoDetalle(p,reader2.GetInt32(3)));
                    }
                }
                presupuesto = new Presupuesto(idPresupuesto,nombreDestinatario, detalles);    
            }
        }
        connection.Close();
       }
        return presupuesto;
    }

    //Agregar producto y una cantidad.
    public bool AgregarProductoYCantidad(int idPresupuesto,int idProducto,int cantidad)
    {
        try
        {
            using(var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, cantidad) VALUES (@idPresupuesto, @idProducto, @cantidad);";

                var command = new SqliteCommand(query, connection);

                command.Parameters.AddWithValue("@idPresupuesto",idPresupuesto);
                command.Parameters.AddWithValue("@idProducto",idProducto);
                command.Parameters.AddWithValue("@cantidad",cantidad);

                int filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
        return false;
    }
    }

