using Microsoft.AspNetCore.Mvc;
using backend.Context;
using backend.Entities;


namespace backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
 public class AutenticacaoController : ControllerBase
{
    private readonly FinalDBContext _context;

    public AutenticacaoController(FinalDBContext dbContext)
    {
        _context = dbContext;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] Cadastro cadastro)
    {
        var user = _context.Cadastros
            .AsEnumerable() // Transforma a consulta em memória para garantir case sensitivity
            .FirstOrDefault(u => u.nickname.Equals(cadastro.nickname, StringComparison.Ordinal) 
                                 && u.senhaCadastro.Equals(cadastro.senhaCadastro, StringComparison.Ordinal));

        if (user == null)
        {
            return Unauthorized();
        }

        return Ok(new { Message = "Logado!", CadastroId = user.Id });
    }
}
}