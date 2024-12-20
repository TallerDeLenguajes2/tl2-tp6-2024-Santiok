namespace Repositorios;
using Microsoft.Data.Sqlite;
using Models;


public class ProductoRepository : IProductoRepository
{
    private string cadenaDeConexion;

    public ProductoRepository(string cadena)
    {
        this.cadenaDeConexion = cadena;
    }

    public bool CrearProducto(Producto producto)
    {
        string query = "INSERT INTO Productos (Descripcion, Precio) VALUES (@descripcion, @precio)";
        int cantidadFilas = 0;

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            SqliteCommand command = new SqliteCommand(query, conexion);
            command.Parameters.Add(new SqliteParameter("@descripcion", producto.Descripcion));
            command.Parameters.Add(new SqliteParameter("@precio", producto.Precio));
            cantidadFilas = command.ExecuteNonQuery();
            conexion.Close();
        }
        return cantidadFilas > 0;
    }

    public bool ModificarProducto(int id, Producto producto)
    {
        string query = "UPDATE Productos (Descripcion, Precio) VALUES (@descripcion, @precio) WHERE idProducto = @id";
        int cantidadFilas = 0;

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            SqliteCommand command = new SqliteCommand(query, conexion);
            command.Parameters.Add(new SqliteParameter("@descripcion", producto.Descripcion));
            command.Parameters.Add(new SqliteParameter("@precio", producto.Precio));
            command.Parameters.Add(new SqliteParameter("@id", id));
            cantidadFilas = command.ExecuteNonQuery();
            conexion.Close();
        }
        return cantidadFilas > 0;
    }

    public bool ModificarNombre(int id, string nuevoNom)
    {
        string query =  "UPDATE Productos SET Descripcion = @descripcion WHERE idProducto = @id";;
        int cantidadFilas = 0;

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            SqliteCommand command = new SqliteCommand(query, conexion);
            command.Parameters.Add(new SqliteParameter("@descripcion", nuevoNom));
            command.Parameters.Add(new SqliteParameter("@id", id));
            cantidadFilas = command.ExecuteNonQuery();
            conexion.Close();
        }
        return cantidadFilas > 0;
    }

    public List<Producto> ListarProductos()
    {
        string query = "SELECT * FROM Productos";
        List<Producto> listaProductos = new List<Producto>();

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            SqliteCommand command = new SqliteCommand(query, conexion);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Producto producto = new Producto();
                    producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Precio = Convert.ToInt32(reader["Precio"]);
                    listaProductos.Add(producto);
                }
            }
            conexion.Close();
        }
        return listaProductos;
    }

    public Producto ObtenerProductoPorId(int id)
    {
        string query = "SELECT * FROM Productos WHERE id = @id";
        Producto producto = null;

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            SqliteCommand command = new SqliteCommand(query, conexion);
            command.Parameters.Add(new SqliteParameter("@id", id));

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    producto = new Producto
                    {
                        IdProducto = Convert.ToInt32(reader["idProducto"]),
                        Descripcion = reader["Descripcion"].ToString(),
                        Precio = Convert.ToInt32(reader["Precio"])
                    };
                }
            }
            conexion.Close();
        }
        return producto;
    }

    public bool EliminarProducto(int id)
    {
        string query = "DELETE FROM Productos WHERE id = @id";
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