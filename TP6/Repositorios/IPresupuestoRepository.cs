namespace Repositorios;
using Models;
public interface IPresupuestoRepository
{
    public bool CrearPresupuesto(Presupuesto presupuesto);
    public List<Presupuesto> ListarPresupuestos();
    public Presupuesto ObtenerPresupuestoPorId(int id);
    public bool AgregarProductoAPresupuesto(int id, int idProducto, int cantidad);
    public bool EliminarPresupuesto(int id);

}