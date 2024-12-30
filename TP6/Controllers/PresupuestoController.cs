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
}