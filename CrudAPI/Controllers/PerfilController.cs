using Microsoft.AspNetCore.Mvc;

using CrudAPI.Context;
using CrudAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using CrudAPI.Services;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly PerfilService _perfilService;

        public PerfilController(PerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<PerfilDTO>>> Get()
        {
            return Ok(await _perfilService.lista());
        }

    }
}
