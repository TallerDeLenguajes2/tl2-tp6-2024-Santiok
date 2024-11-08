namespace Repositorios;
using Microsoft.Data.Sqlite;
using Models;


public class ProductoRepository : IRepository<Producto>
{
    private readonly string cadenaDeConexion = "Data Source=bd/Tienda.db;Cache=Shared";

    //Inserto un producto.
    public void Insertar(Producto prod)
    {
        using(var connection = new SqliteConnection(cadenaDeConexion))
        {
            connection.Open();

            string query = "INSERT INTO Productos (Descripcion, Precio) VALUES ($desc, $precio)";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("$desc", prod.Descripcion);
            command.Parameters.AddWithValue("$precio", prod.Precio);

            connection.Close();
        }
    }

    //Modifico un producto existente.
    public void Modificar(int id, Producto prod)
    {
        using(var connection = new SqliteConnection(cadenaDeConexion))
        {
            connection.Open();

            string query = "UPDATE Productos(Descripcion, Precio) VALUES ($desc, $precio) WHERE idProductos = $id_pasado;";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("$desc", prod.Descripcion);
            command.Parameters.AddWithValue("$precio", prod.Precio);
            command.Parameters.AddWithValue("$id_pasado", prod.IdProducto);

            connection.Close();
        }
    }

    //Listo los productos registrados.
    public List<Producto> Listar()
    {
        List<Producto> listaProd = new List<Producto>();
        using (var connection = new SqliteConnection(cadenaDeConexion))
        {
            connection.Open();

            string query = "SELECT * FROM Productos;";
            SqliteCommand command = new SqliteCommand(query, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    var prod = new Producto();
                    prod.IdProducto = Convert.ToInt32(reader["idProducto"]);
                    prod.Descripcion = reader["Descripcion"].ToString();
                    prod.Precio = Convert.ToInt32(reader["Precio"]);
                    listaProd.Add(prod);
                }
            }
            connection.Close();
        }
        return listaProd;
    }

    //Obtengo detalles del producto.
    public Producto Obtener(int id)
    {
        var prod = new Producto();

        using (var connection = new SqliteConnection(cadenaDeConexion))
        {
            connection.Open();

            string query = "SELECT * FROM Productos WHERE idProductos = $id_pasado;";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("$id_pasado", prod.IdProducto);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    prod.IdProducto = Convert.ToInt32(reader["idProducto"]);
                    prod.Descripcion = reader["Descripcion"].ToString();
                    prod.Precio = Convert.ToInt32(reader["Precio"]);
                }
            }

            connection.Close();
        }
        return prod;
    }

//Elimino un producto.
    void IRepository<Producto>.Eliminar(int id)
    {
        using (var connection = new SqliteConnection(cadenaDeConexion))
        {
            connection.Open();

            string query = "DELETE FROM Productos WHERE idProductos = $id_pasado;";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("$id_pasado", id);

            connection.Close();
        }
    }
}