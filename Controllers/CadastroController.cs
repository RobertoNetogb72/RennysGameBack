using Microsoft.AspNetCore.Mvc;
using backend.Context;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private readonly FinalDBContext _context;

        public CadastroController(FinalDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cadastro>>> GetCadastro()
        {
            return await _context.Cadastros.ToListAsync();
        }
        [HttpGet("BuscarPorNome")]
        public IActionResult BuscarPorNome(string nome)
        {
            var cadastros = _context.Cadastros.Where(x => x.nomeCadastro.Contains(nome));
            return Ok(cadastros);

        }
         [HttpGet("BuscarPorNick")]
        public IActionResult BuscarPorNick(string nick)
        {
            var cadastros = _context.Cadastros.Where(x => x.nickname.Contains(nick));
            return Ok(cadastros);

        }
      
        [HttpGet("{id}")]
        public async Task<ActionResult<Cadastro>> GetCadastro(int id)
        {
            var cadastro = await _context.Cadastros.FindAsync(id);

            if (cadastro == null)
            {
                return NotFound();
            }

            return cadastro;
        }

      
      
        [HttpPost]
        public async Task<ActionResult<Cadastro>> PostCadastro(Cadastro cadastro)
        {
            var user = _context.Cadastros.FirstOrDefault(u => u.nickname == cadastro.nickname || u.emailCadastro == cadastro.emailCadastro);
            if (user != null)
            {
                return Unauthorized(); 
            }
            
            _context.Cadastros.Add(cadastro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCadastro), new { id = cadastro.Id }, cadastro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCadastro(int id, Cadastro cadastro)
        {
            if (id != cadastro.Id)
            {
                return BadRequest("O Id fornecido não corresponde ao Id do cliente.");
            }

            _context.Entry(cadastro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Cliente atualizado com sucesso!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CadastroExists(id))
                {
                    return NotFound("Cliente não encontrado.");
                }
                else
                {
                    throw;
                }
            }
        }


      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCadastro(int id)
        {
            var cadastro = await _context.Cadastros.FindAsync(id);
            if (cadastro == null)
            {
                return NotFound();
            }

            _context.Cadastros.Remove(cadastro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CadastroExists(int id)
        {
            return _context.Cadastros.Any(e => e.Id == id);
        }

         
    }
}