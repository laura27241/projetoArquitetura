using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoradoresService.Data;
using MoradoresService.Models;

[Route("api/[controller]")]
[ApiController]
public class MoradoresController : ControllerBase
{
    private readonly MoradoresContext _context;

    public MoradoresController(MoradoresContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Morador>>> GetMoradores()
    {
        try
        {
            var moradores = await _context.Moradores.ToListAsync();
            return Ok(moradores);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao acessar os moradores: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Morador>> PostMorador(Morador morador)
    {
        try
        {
            _context.Moradores.Add(morador);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMoradores), new { id = morador.Id }, morador);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao adicionar o morador: {ex.Message}");
        }
    }
}
