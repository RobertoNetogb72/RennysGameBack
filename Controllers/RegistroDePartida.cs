using Microsoft.AspNetCore.Mvc;
using backend.Context;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroDePartidaController : ControllerBase
    {
        private readonly FinalDBContext _context;

        public RegistroDePartidaController(FinalDBContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> ObterRegistroDePartidaPorId(int id)
        {
            var registrodepartidaEncontrado = await _context.RegistroDePartidas.FindAsync(id);
            if (registrodepartidaEncontrado == null)
            {
                return NotFound($"Registro de Partida com ID {id} não encontrado.");
            }

            return Ok(registrodepartidaEncontrado);
        }


        [HttpPost]
        public async Task<IActionResult> CriarRegistroDePartida(RegistroDePartida registroDePartida)
        {
            if (registroDePartida == null)
            {
                return BadRequest("Dados inválidos para criar um pedido.");
            }

            var cadastroExistente = await _context.Cadastros.FindAsync(registroDePartida.CadastroId);
            if (cadastroExistente == null)
            {
                return BadRequest("Usuario não encontrado. O pedido deve estar associado a um usuario existente.");
            }

            registroDePartida.Cadastro = cadastroExistente;


            _context.RegistroDePartidas.Add(registroDePartida);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterRegistroDePartidaPorId), new { id = registroDePartida.Id }, registroDePartida);
        }

    

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarRegistroDePartida(int id, RegistroDePartida registroDePartida)
    {
        if (id != registroDePartida.Id)
        {
            return BadRequest("ID do Registro de Partida não corresponde aos dados fornecidos.");
        }

        var registroDePartidaExistente = await _context.RegistroDePartidas.FindAsync(id);
        if (registroDePartidaExistente == null)
        {
            return NotFound($"Pedido com ID {id} não encontrado.");
        }


        registroDePartidaExistente.qntPartida = registroDePartida.qntPartida;
        registroDePartidaExistente.qntVitoria = registroDePartida.qntVitoria;
        registroDePartidaExistente.qntDerrota = registroDePartida.qntDerrota;
        registroDePartidaExistente.qntEmpate = registroDePartida.qntEmpate;
        await _context.SaveChangesAsync();

        return Ok(registroDePartidaExistente);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirRegistroDePartida(int id)
    {
        var registroDePartidaParaExcluir = await _context.RegistroDePartidas.FindAsync(id);
        if (registroDePartidaParaExcluir == null)
        {
            return NotFound($"Registro de Partida com ID {id} não encontrado.");
        }

        _context.RegistroDePartidas.Remove(registroDePartidaParaExcluir);
        await _context.SaveChangesAsync();

        return NoContent();
    }

[HttpGet("rankingVitoria")]
public async Task<IActionResult> GetRankingVitoria()
{
    var ranking = await _context.RegistroDePartidas
        .Include(p => p.Cadastro) // Certifica-se de carregar o Cadastro relacionado
        .OrderByDescending(p => p.qntVitoria)
        .Select(p => new
        {
            Usuario = p.Cadastro != null ? p.Cadastro.nickname : "Desconhecido",
            Partidas = p.qntPartida,
            Vitorias = p.qntVitoria,
            Derrotas = p.qntDerrota,    
            Empate =  p.qntEmpate
        })
        .ToListAsync();
 
    // Aqui, você pode usar o ILogger ou Console.WriteLine para verificar o que está sendo retornado
    foreach (var item in ranking)
    {
        Console.WriteLine($"Usuario: {item.Usuario}, Partidas: {item.Partidas}, Vitorias: {item.Vitorias}, Derrotas: {item.Derrotas}, Empate: {item.Empate}");
    }
 
    return Ok(ranking);
}
[HttpGet("rankingPart")]
public async Task<IActionResult> GetRankingPart()
{
    var ranking = await _context.RegistroDePartidas
        .Include(p => p.Cadastro) // Certifica-se de carregar o Cadastro relacionado
        .OrderByDescending(p => p.qntPartida)
        .Select(p => new
        {
            Usuario = p.Cadastro != null ? p.Cadastro.nickname : "Desconhecido",
            Partidas = p.qntPartida,
            Vitorias = p.qntVitoria,
            Derrotas = p.qntDerrota,    
            Empate =  p.qntEmpate
        })
        .ToListAsync();
 
    // Aqui, você pode usar o ILogger ou Console.WriteLine para verificar o que está sendo retornado
    foreach (var item in ranking)
    {
        Console.WriteLine($"Usuario: {item.Usuario}, Partidas: {item.Partidas}, Vitorias: {item.Vitorias}, Derrotas: {item.Derrotas}, Empate: {item.Empate}");
    }
 
    return Ok(ranking);

}

[HttpGet("rankingDer")]
public async Task<IActionResult> GetRankingDer()
{
    var ranking = await _context.RegistroDePartidas
        .Include(p => p.Cadastro) // Certifica-se de carregar o Cadastro relacionado
        .OrderByDescending(p => p.qntDerrota)
        .Select(p => new
        {
            Usuario = p.Cadastro != null ? p.Cadastro.nickname : "Desconhecido",
            Partidas = p.qntPartida,
            Vitorias = p.qntVitoria,
            Derrotas = p.qntDerrota,    
            Empate =  p.qntEmpate
        })
        .ToListAsync();
 
    // Aqui, você pode usar o ILogger ou Console.WriteLine para verificar o que está sendo retornado
    foreach (var item in ranking)
    {
        Console.WriteLine($"Usuario: {item.Usuario}, Partidas: {item.Partidas}, Vitorias: {item.Vitorias}, Derrotas: {item.Derrotas}, Empate: {item.Empate}");
    }
 
    return Ok(ranking);

}

[HttpGet("rankingEmpate")]
public async Task<IActionResult> GetRankingEmpate()
{
    var ranking = await _context.RegistroDePartidas
        .Include(p => p.Cadastro) // Certifica-se de carregar o Cadastro relacionado
        .OrderByDescending(p => p.qntEmpate)
        .Select(p => new
        {
            Usuario = p.Cadastro != null ? p.Cadastro.nickname : "Desconhecido",
            Partidas = p.qntPartida,
            Vitorias = p.qntVitoria,
            Derrotas = p.qntDerrota,    
            Empate =  p.qntEmpate
        })
        .ToListAsync();
 
    // Aqui, você pode usar o ILogger ou Console.WriteLine para verificar o que está sendo retornado
    foreach (var item in ranking)
    {
        Console.WriteLine($"Usuario: {item.Usuario}, Partidas: {item.Partidas}, Vitorias: {item.Vitorias}, Derrotas: {item.Derrotas}, Empate: {item.Empate}");
    }
 
    return Ok(ranking);

}


    }}