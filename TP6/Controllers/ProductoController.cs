using Microsoft.AspNetCore.Mvc;
using Models;
using Repositorios;
namespace TP6.Controllers;


[ApiController]
[Route("[controller]")]

public class ProductoController : Controller
{

    private readonly ILogger<HomeController> _logger;
    ProductoRepository productoRepository = new ProductoRepository();

    public ProductoController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    public ActionResult<List<Producto>> Index()
    {
        try
        {
            return View(productoRepository.Listar());
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}