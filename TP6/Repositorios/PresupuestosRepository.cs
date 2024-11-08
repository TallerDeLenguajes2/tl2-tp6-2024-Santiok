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
       var pres = new Presupuesto();
       var detalles = new List<PresupuestoDetalles>(); 

        using (var connection = new SqliteConnection(cadenaDeConexion))
        {
            connection.Open();

            string query = @"SELECT idPresupuesto, NombreDestinatario, idProducto, Cantidad FROM Presupuestos p
                        INNER JOIN PresupuestosDetalle pd USING(idPresupuesto)
                        WHERE p.idPresupuesto = @id_pasado;";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("$id_pasado", id);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    /*
                    var producto = new Producto(sqlReader.GetInt32(3), sqlReader.GetString(4), sqlReader.GetInt32(5));
                    var detalle = new PresupuestoDetalles(sqlReader.GetInt32(6), producto);
                    pres.Detalle.Add(detalle);
                    */
                }
            }

            connection.Close();
        }
        return pres;
    }
    }


