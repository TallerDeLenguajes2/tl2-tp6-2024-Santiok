namespace Repositorios;
using Models;
public interface IProductoRepository
{
    public bool CrearProducto(Producto producto);
    public bool ModificarProducto(int id, Producto producto);
    public List<Producto> ListarProductos();
    public Producto ObtenerProductoPorId(int id);
    public bool EliminarProducto(int id);
    public bool ModificarNombre(int id, string nuevoNom);
}