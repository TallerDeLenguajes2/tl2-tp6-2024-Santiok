namespace Models;
public class Producto
{
    private int idProducto;
    private string? descripcion;
    private int precio;


    public int IdProducto { get => idProducto; set => idProducto = value; }
    public string? Descripcion { get => descripcion; set => descripcion = value; }
    public int Precio { get => precio; set => precio = value; }

    //Constructores.
    /*
    public Producto()
    {
        idProducto = -1;
        descripcion = string.Empty;
        precio = 0;
    }
    */
    public Producto()
    {
        
    }
    public Producto(int id, string descrip, int prec)
    {
        this.idProducto = id;
        this.descripcion = descrip;
        this.precio = prec;
    }
}