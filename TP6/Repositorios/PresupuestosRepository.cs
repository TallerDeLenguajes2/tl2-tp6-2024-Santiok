namespace Repositorios;
using Microsoft.Data.Sqlite;
using Models;
////
public class PresupuestosRepository : IRepository<Presupuestos>
{
    private readonly string cadenaDeConexion = "Data Source=bd/Tienda.db;Cache=Shared";

    //Insertar presupuesto. --
    public void Insertar(Presupuestos pres)
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

    //Modificar un presupuesto. --
    public void Modificar(int id, Presupuestos pres)
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

    //Listar los presupuestos.--
    public List<Presupuestos> Listar()
    {
        List<Presupuestos> listaPres = new List<Presupuestos>();
        using (var connection = new SqliteConnection(cadenaDeConexion))
        {
            connection.Open();

            string query = "SELECT * FROM Productos;";

            SqliteCommand command = new SqliteCommand(query, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    var pres = new Presupuestos();
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

    //Obtener presupuestos. ----?
    public Presupuestos ObtenerPresupuestos(int id)
    {
       var pres = new Presupuestos();

        using (var connection = new SqliteConnection(cadenaDeConexion))
        {
            connection.Open();

            string query = "SELECT p.idPresupuesto, p.NombreDestinatario, p.FechaCreacion, pr.idProducto, pr.Descripcion, pr.Precio, pd.Cantidad    FROM Presupuestos p         WHERE idProductos = $id_pasado     LEFT JOIN PresupuestosDetalles pd   USING(idPresupuesto)       LEFT JOIN Presupuestos  pr USING(idPresupuesto);";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("$id_pasado", pres.IdPresupuesto);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    /*pres.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                    pres.NombreDestinatario = reader["NombreDestinatario"].ToString();
                    pres.FechaCreacion = reader["FechaCreacion"].ToString();*/
                    var producto = new Productos(sqlReader.GetInt32(3), sqlReader.GetString(4), sqlReader.GetInt32(5));
                    var detalle = new PresupuestosDetalles(sqlReader.GetInt32(6), producto);
                    pres.Detalle.Add(detalle);
                }
            }

            connection.Close();
        }
        return pres;
    }

    //Eliminar presupuesto.--
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
    }
