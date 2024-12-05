using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxasService.Data;
using TaxasService.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;

[Route("api/[controller]")]
[ApiController]
public class TaxasController : ControllerBase
{
    private readonly TaxasContext _context;

    public TaxasController(TaxasContext context)
    {
        _context = context;
    }

    // GET: api/Taxas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Taxa>>> GetTaxas()
    {
        try
        {
            var taxas = await _context.Taxas.ToListAsync();
            return Ok(taxas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao acessar as taxas: {ex.Message}");
        }
    }

    // POST: api/Taxas
    [HttpPost]
    public async Task<ActionResult<Taxa>> PostTaxa(Taxa taxa)
    {
        try
        {
            _context.Taxas.Add(taxa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaxas), new { id = taxa.Id }, taxa);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao adicionar a taxa: {ex.Message}");
        }
    }

    // PUT: api/Taxas/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTaxa(int id, Taxa novaTaxa)
    {
        try
        {
            var taxa = await _context.Taxas.FindAsync(id);
            if (taxa == null)
            {
                return NotFound("Taxa não encontrada.");
            }

          
            taxa.Residencia = novaTaxa.Residencia;
            taxa.Valor = novaTaxa.Valor;
            taxa.DataVencimento = novaTaxa.DataVencimento;

            _context.Entry(taxa).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar taxa: {ex.Message}");
        }
    }

    // PUT: api/Taxas/alterar-taxa/{taxaId}
    [HttpPut("alterar-taxa/{taxaId}")]
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
