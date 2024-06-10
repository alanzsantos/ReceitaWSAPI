using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceitaWSAPI.Data;
using ReceitaWSAPI.Models;
using ReceitaWSAPI.Services;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PedidosController : ControllerBase
{
    private readonly ReceitaWSContext _context;
    private readonly ReceitaWsService _receitaWsService;

    public PedidosController(ReceitaWSContext context, ReceitaWsService receitaWsService)
    {
        _context = context;
        _receitaWsService = receitaWsService;
    }

    [HttpPost]
    public async Task<ActionResult<Pedido>> PostPedido(string cnpj)
    {
        // Obter os dados da empresa da API ReceitaWS
        var resultado = await _receitaWsService.GetCompanyDataAsync(cnpj);

        var pedido = new Pedido
        {
            CNPJ = cnpj,
            Resultado = resultado.Replace("\n", "").Replace(@"\", "")

        };

        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Pedido>> GetPedido(int id)
    {
        var pedido = await _context.Pedidos.FindAsync(id);

        if (pedido == null)
        {
            return NotFound();
        }

        return pedido;
    }
    
}
