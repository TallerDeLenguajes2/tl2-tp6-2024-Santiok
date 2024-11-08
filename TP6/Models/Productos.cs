namespace Models;
public class Productos
{
    private int idProducto;
    private string? descripcion;
    private int precio;


    public int IdProducto { get => idProducto; set => idProducto = value; }
    public string? Descripcion { get => descripcion; set => descripcion = value; }
    public int Precio { get => precio; set => precio = value; }

    //Constructores.
    public Productos()
    {
        idProducto = -1;
        descripcion = string.Empty;
        precio = 0;
    }

    public Productos(int id, string descrip, int prec)
    {
        this.idProducto = id;
        this.descripcion = descrip;
        this.precio = prec;
    }
}