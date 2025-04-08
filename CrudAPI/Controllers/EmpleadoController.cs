using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CrudAPI.Context;
using CrudAPI.DTOs;
using CrudAPI.Entities;
using Microsoft.EntityFrameworkCore;
using CrudAPI.Services;


namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly EmpleadoService _empleadoService;

        public EmpleadoController(EmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<EmpleadoDTO>>> Get()
        {
            return Ok(await _empleadoService.Lista());
        }


        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<ActionResult<EmpleadoDTO>> Get(int id)       {

            var empleado = await _empleadoService.ListarUno(id);
            if (empleado is null)
            {
                return NotFound();
            }
            return Ok(empleado);
        }

        [HttpPost]
        [Route("guardar")]
        public async Task<ActionResult<EmpleadoDTO>> Guardar(EmpleadoDTO empleadoDTO)
        {
            var empleado = await _empleadoService.GuardarEmpleado(empleadoDTO);
            if (empleado is null)
            {
                return NotFound();
            }
            return Ok("Empleado guardado");

        }

        [HttpPut]
        [Route("editar")]
        public async Task<ActionResult<EmpleadoDTO>> Editar(EmpleadoDTO empleadoDTO)
        {
            var empleado = await _empleadoService.EditarEmpleado(empleadoDTO);
            if (empleado is null)
            {
                return NotFound();
            }
            return Ok(empleadoDTO);

        }


        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<ActionResult<EmpleadoDTO>> Eliminar(int id)
        {
            var empleado = await _empleadoService.EliminarEmpleado(id);
            if (empleado== false)
            {
                return NotFound();
            }
            return Ok("Empleado eliminado");
        }

    }
}
