using Microsoft.AspNetCore.Mvc;
using Models;
using Repositorios;
namespace Controllers;

public class ProductoController : Controller
{
    private readonly ILogger<ProductoController> _logger;
    private readonly ProductoRepository productoRepository;
    public ProductoController(ILogger<ProductoController> logger)
    {
        _logger = logger;
        productoRepository = new ProductoRepository("Data Source=BD/Tienda.db;Cache=Shared");
    }


    [HttpGet]
    public IActionResult Index()
    {
        var productos = productoRepository.ListarProductos();
        return View(productos);
    }


    [HttpGet]
    public IActionResult CrearProducto()
    {
        return View();
    }


    [HttpPost]
    public IActionResult CrearProducto(Producto producto)
    {
        productoRepository.CrearProducto(producto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarProducto(int id)
    {
        return View(productoRepository.ObtenerProductoPorId(id));
    }


    [HttpPost]
    public IActionResult ModificarProducto(int id, Producto producto)
    {
        productoRepository.ModificarProducto(id, producto);
        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult EliminarProducto(int id)
    {
        return View(productoRepository.ObtenerProductoPorId(id));
        
    }


    //[HttpDelete]Con esto no funciona, pero porque?.
    public IActionResult ConfirmarEliminacion(int id)
    {
        productoRepository.EliminarProducto(id);
        return RedirectToAction("Index");
    }
}