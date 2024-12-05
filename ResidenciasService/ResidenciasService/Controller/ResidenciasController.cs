using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResidenciasService.Data;
using ResidenciasService.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using TaxasService.Models;
using MoradoresService.Models;

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

            var moradores = JsonSerializer.Deserialize<IEnumerable<Morador>>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Ok(moradores);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar moradores: {ex.Message}");
        }
    }

    [HttpPut("alterar-taxa")]
    public async Task<IActionResult> AlterarTaxa([FromBody] Taxa novaTaxa)
    {
        try
        {
            return Ok($"Taxa com valor {novaTaxa.Valor} alterada com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao alterar taxa: {ex.Message}");
        }
    }

}
