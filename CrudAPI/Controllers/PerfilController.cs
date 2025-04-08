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
            return Ok(await _perfilService.Lista());
        }


        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<ActionResult<PerfilDTO>> Get(int id)
        {

            var perfil = await _perfilService.ListarUno(id);
            if (perfil is null)
            {
                return NotFound();
            }
            return Ok(perfil);
        }

        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult<PerfilDTO>> Guardar(PerfilDTO perfilDTO)
        {
            var perfil = await _perfilService.GuardarPerfil(perfilDTO);
            if (perfil is null)
            {
                return NotFound();
            }
            return Ok("Perfil guardado");

        }

        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult<PerfilDTO>> Editar(PerfilDTO perfilDTO)
        {
            var perfil = await _perfilService.EditarPerfil(perfilDTO);
            if (perfil is null)
            {
                return NotFound();
            }
            return Ok(perfilDTO);

        }


        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<ActionResult<PerfilDTO>> Eliminar(int id)
        {
            var perfil = await _perfilService.EliminarPerfil(id);
            if (perfil == false)
            {
                return NotFound();
            }
            return Ok("perfil eliminado");
        }

    }
}
