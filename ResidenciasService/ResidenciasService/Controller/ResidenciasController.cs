using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResidenciasService.Data;
using ResidenciasService.Models;

[Route("api/[controller]")]
[ApiController]
public class ResidenciasController : ControllerBase
{
    private readonly ResidenciasContext _context;

    public ResidenciasController(ResidenciasContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Residencia>>> GetResidencias()
    {
        try
        {
            var residencias = await _context.Residencias.ToListAsync();
            return Ok(residencias);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao acessar as residências: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Residencia>> PostResidencia(Residencia residencia)
    {
        try
        {
            _context.Residencias.Add(residencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetResidencias), new { id = residencia.Id }, residencia);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao adicionar a residência: {ex.Message}");
        }
    }

    [HttpGet("moradores")]
    public async Task<IActionResult> GetMoradoresFromMoradoresService()
    {
        using var httpClient = new HttpClient();
        try
        {
            var response = await httpClient.GetAsync("http://localhost:5002/api/Moradores");
            response.EnsureSuccessStatusCode();

            var moradores = await response.Content.ReadAsAsync<IEnumerable<Morador>>();
            return Ok(moradores);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar moradores: {ex.Message}");
        }
    }

    [HttpPut("alterar-taxa")]
    public async Task<IActionResult> AlterarTaxa(int taxaId, Taxa novaTaxa)
    {
        using var httpClient = new HttpClient();
        try
        {
            var response = await httpClient.PutAsJsonAsync($"http://localhost:5000/api/Taxas/{taxaId}", novaTaxa);
            response.EnsureSuccessStatusCode();

            return Ok("Taxa alterada com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao alterar taxa: {ex.Message}");
        }
    }
}
