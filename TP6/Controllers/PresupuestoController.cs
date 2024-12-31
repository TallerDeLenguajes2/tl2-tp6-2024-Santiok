using Microsoft.AspNetCore.Mvc;
using Models;
using Repositorios;
namespace Controllers;

public class PresupuestoController : Controller
{
    private readonly ILogger<PresupuestoController> _logger;
    private readonly PresupuestosRepository presupuestoRepository;
    public PresupuestoController(ILogger<PresupuestoController> logger)
    {
        _logger = logger;
        presupuestoRepository = new PresupuestosRepository("Data Source=BD/Tienda.db;Cache=Shared");
    }

    [HttpGet]
    public ActionResult<List<Presupuesto>> Index()
    {
        return View(presupuestoRepository.ListarPresupuestos());
    }


    [HttpGet]
    public IActionResult CrearPresupuesto()
    {
        return View();
    }


    [HttpPost]
    public IActionResult CrearPresupuesto(Presupuesto presupuesto)
    {
        presupuestoRepository.CrearPresupuesto(presupuesto);
        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult ModificarPresupuesto(int id)
    {
        return View(presupuestoRepository.ObtenerPresupuestoPorId(id));
    }


    [HttpPost]
    public IActionResult ModificarPresupuesto(int id, Presupuesto presupuesto)
    {
        presupuestoRepository.ModificarPresupuesto(id, presupuesto);
        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult EliminarPresupuesto(int id)
    {
        return View(presupuestoRepository.ObtenerPresupuestoPorId(id));
    }


    public IActionResult ConfirmarEliminacion(int id)
    {
        presupuestoRepository.EliminarPresupuesto(id);
        return RedirectToAction("Index");
    }










    [HttpGet]
    public IActionResult MostrarPresupuestoDetalle(int id)
    {
        return View(presupuestoRepository.ListarDetallePresupuesto(id));
    }
    

    [HttpGet]
    public IActionResult AgregarProducto(int id)
    {
        PresupuestosDetalles presupuestosDetalles = new PresupuestosDetalles();
        presupuestosDetalles.IdPresupuesto = id;
        return View(presupuestosDetalles);
    }


    [HttpPost]
    public IActionResult AgregarProducto(PresupuestosDetalles presupuestosDetalles)
    {
        presupuestoRepository.CrearNuevoDetalle(presupuestosDetalles);
        return RedirectToAction("Index");
    }
}