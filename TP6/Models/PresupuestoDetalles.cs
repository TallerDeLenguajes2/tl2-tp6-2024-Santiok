namespace Models;
public class PresupuestoDetalles
{
    private Producto producto;
    private int cantidad;

    public Producto Producto { get => producto; set => producto = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }

    //Constructores.
    public PresupuestoDetalles(Producto produ, int cant)
    {
        this.producto = produ;
        this.Cantidad = cant;
    }
}