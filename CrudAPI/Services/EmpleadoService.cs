using CrudAPI.Context;
using CrudAPI.DTOs;
using CrudAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CrudAPI.Services
{
    public class EmpleadoService
    {
        private readonly AppDbContext _context;

        public EmpleadoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmpleadoDTO>> Lista()
        {
            var listaDTO = new List<EmpleadoDTO>();
            var listaDB = await _context.Empleados.Include(p => p.PerfilReferencia).ToListAsync();

            foreach (var item in listaDB)
            {
                listaDTO.Add(new EmpleadoDTO
                {
                    IdEmpleado = item.IdEmpleado,
                    NombreCompleto = item.NombreCompleto,
                    Sueldo = item.Sueldo,
                    IdPerfil = item.IdPerfil,
                    NombrePerfil = item.PerfilReferencia?.Nombre
                });

            }

            return listaDTO;
        }

        public async Task<EmpleadoDTO> ListarUno(int id)
        {
            var empleadoDTO = new EmpleadoDTO();
            var empleadoDB = await _context.Empleados.Include(p => p.PerfilReferencia)
                .Where(e => e.IdEmpleado == id).FirstAsync();

            empleadoDTO.IdEmpleado = id;
            empleadoDTO.NombreCompleto = empleadoDB.NombreCompleto;
            empleadoDTO.Sueldo = empleadoDB.Sueldo;
            empleadoDTO.IdPerfil = empleadoDB.IdPerfil;
            empleadoDTO.NombrePerfil = empleadoDB.PerfilReferencia.Nombre;

            return empleadoDTO;
        }

        public async Task<EmpleadoDTO> GuardarEmpleado(EmpleadoDTO empleadoDTO)
        {
            var empleadoDB = new Empleado
            {
                NombreCompleto = empleadoDTO.NombreCompleto,
                Sueldo = empleadoDTO.Sueldo,
                IdPerfil = empleadoDTO.IdPerfil
            };

            _context.Empleados.Add(empleadoDB);
            await _context.SaveChangesAsync();

            return empleadoDTO;

        }

        public async Task<EmpleadoDTO> EditarEmpleado(EmpleadoDTO empleadoDTO)
        {
            var empleadoDB = await _context.Empleados.FindAsync(empleadoDTO.IdEmpleado);

            if (empleadoDB is null)
            {
                return null; // Indica que el empleado no fue encontrado
            }

            empleadoDB.NombreCompleto = empleadoDTO.NombreCompleto;
            empleadoDB.Sueldo = empleadoDTO.Sueldo;
            empleadoDB.IdPerfil = empleadoDTO.IdPerfil;

            _context.Empleados.Update(empleadoDB);
            await _context.SaveChangesAsync();

            return empleadoDTO;
        }

        public async Task<bool> EliminarEmpleado(int id)
        {
            /*
            var empleadoDB = await _context.Empleados
               .Where(e => e.IdEmpleado == id).FirstOrDefaultAsync();
           */

            var empleadoDB = await _context.Empleados.FindAsync(id);

            if (empleadoDB is null)
            {
                return false; // Indica que no se encontró el empleado
            }

            _context.Empleados.Remove(empleadoDB);
            await _context.SaveChangesAsync();
            return true; // Indica que la eliminación fue exitosa
        }

    }
}
