namespace Models;
public class Presupuesto
{
    public static double IVA = 0.21;
    private int idPresupuesto;
    private string nombreDestinatario;
    private List<PresupuestosDetalles> detalle;
    private string fechaCreacion;

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public List<PresupuestosDetalles> Detalle { get => detalle; set => detalle = value; }
    public string FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }

    //Constructores.
    public Presupuesto()
    {
        idPresupuesto = -1;
        nombreDestinatario = string.Empty;
        detalle = new List<PresupuestosDetalles>();
        fechaCreacion = string.Empty;
    }
    public Presupuesto(int idPresupuesto, string nombreDestinatario, List<PresupuestosDetalles> detalle)
    {
        this.idPresupuesto = idPresupuesto;
        this.nombreDestinatario = nombreDestinatario;
        this.detalle = detalle;
    }

    //Calcular monto.
    public double MontoPresupuesto()
    {
        int total = 0;

        foreach (var det in detalle)
        {
            total = total + det.Producto.Precio*det.Cantidad;
        }
        return total;
    }
    //Calcular monto con iva.
    public double MontoPresupuestoConIva()
    {
        return MontoPresupuesto() + MontoPresupuesto()*IVA;
    }
    //Cantidad de productos.
    public int CantidadProductos()
    {
        int cant = 0;

        foreach (var det in detalle)
        {
            cant = cant + det.Cantidad;
        }
        return cant;
    }

    public void CargarDetallesPresupuesto(List<PresupuestosDetalles> presupuestoDetalles)
    {
        detalle=presupuestoDetalles;
    }

   public void AgregarProducto(Producto nuevoProducto, int cantidadProducto)
    {
        PresupuestosDetalles nuevoPresupuestoDetalle = new PresupuestosDetalles();
        nuevoPresupuestoDetalle.CargarProducto(nuevoProducto);
        nuevoPresupuestoDetalle.Cantidad = cantidadProducto;

    }
}