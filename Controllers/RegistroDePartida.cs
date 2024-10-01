using Microsoft.AspNetCore.Mvc;
using backend.Context;
using backend.Entities;

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



}
}