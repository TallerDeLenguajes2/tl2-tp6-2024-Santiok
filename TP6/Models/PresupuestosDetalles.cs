namespace Models;
public class PresupuestosDetalles
{
    private Productos producto;
    private int cantidad;

    public Productos Producto { get => producto; set => producto = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }

    //Constructores.
    public PresupuestosDetalles(Productos produ, int cant)
    {
        this.producto = produ;
        this.Cantidad = cant;
    }
}