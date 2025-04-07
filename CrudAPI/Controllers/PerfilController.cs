using CrudAPI.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PerfilController(AppDbContext context        )
        {
            _context = context;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult> Get()
        {
            var listaPerfil = await _context.Perfiles.ToListAsync();
            return Ok(listaPerfil);
        }

    }
}
