using CrudAPI.Context;
using CrudAPI.DTOs;
using CrudAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Services
{
    public class PerfilService
    {
        private readonly AppDbContext _context;

        public PerfilService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PerfilDTO>> Lista()
        {
            var listaDTO = new List<PerfilDTO>();
            var listaDB = await _context.Perfiles.ToListAsync();

            foreach (var item in listaDB)
            {
                listaDTO.Add(new PerfilDTO
                {
                    IdPerfil = item.IdPerfil,
                    Nombre = item.Nombre
                });

            }

            return listaDTO;
        }

        public async Task<PerfilDTO> ListarUno(int id)
        {
            var perfilDTO = new PerfilDTO();
            var perfilDB = await _context.Perfiles.Include(e => e.EmpleadosReferencia)
                .Where(p => p.IdPerfil== id).FirstAsync();

            perfilDTO.IdPerfil = id;
            perfilDTO.Nombre = perfilDB.Nombre;
            
            

            return perfilDTO;
        }

        public async Task<PerfilDTO> GuardarPerfil(PerfilDTO perfilDTO)
        {
            var perfilDB = new Perfil
            {
                Nombre = perfilDTO.Nombre                
            };

            _context.Perfiles.Add(perfilDB);
            await _context.SaveChangesAsync();

            return perfilDTO;

        }

        public async Task<PerfilDTO?> EditarPerfil(PerfilDTO perfilDTO)
        {
            var perfilDB = await _context.Perfiles.FindAsync(perfilDTO.IdPerfil);

            if (perfilDB is null)
            {
                return null; // Indica que el empleado no fue encontrado
            }
            
            perfilDB.IdPerfil = perfilDTO.IdPerfil;
            perfilDB.Nombre = perfilDTO.Nombre;


            _context.Perfiles.Update(perfilDB);
            await _context.SaveChangesAsync();

            return perfilDTO;
        }

        public async Task<bool> EliminarPerfil(int id)
        {
            /*
            var empleadoDB = await _context.Empleados
               .Where(e => e.IdEmpleado == id).FirstOrDefaultAsync();
           */

            var perfilDB = await _context.Perfiles.FindAsync(id);

            if (perfilDB is null)
            {
                return false; // Indica que no se encontró el empleado
            }

            _context.Perfiles.Remove(perfilDB);
            await _context.SaveChangesAsync();
            return true; // Indica que la eliminación fue exitosa
        }



    }
}
