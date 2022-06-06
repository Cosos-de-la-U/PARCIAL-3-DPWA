﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL_3_DPWA.Models;
using PARCIAL_3_DPWA.Models.ViewModel.ExperienciaUsuario;

namespace PARCIAL_3_DPWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienciaUsuarioController : ControllerBase
    {
        private readonly railwayContext _context;

        public ExperienciaUsuarioController(railwayContext context)
        {
            _context = context;
        }

        // GET: api/ExperienciaUsuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExperienciaByUsuario>>> GetExperienciaByUsuarios()
        {
            if (_context.ExperienciaByUsuarios == null)
            {
                return NotFound();
            }
            // Sacando datos ordenados
            var ExperienciaByUsuario = await (from exU in _context.ExperienciaByUsuarios
                                                 orderby exU.Id_usuario ascending
                                                 select exU).ToListAsync();

            List<ExperienciaUsuarioModel> ListaExperienciaModel = new List<ExperienciaUsuarioModel>();
            foreach (ExperienciaByUsuario Experiencia in ExperienciaByUsuario)
            {
                //Obteniendo usuario id
                var usuarioU_name = ObtenerU_nameUsuario(Experiencia.Id_usuario ?? default(int)).Result;

                // Verificando que hayan dados en la lista
                if (ListaExperienciaModel.Count != 0)
                {
                    // Verificando que no se repitan
                    if (ListaExperienciaModel.LastOrDefault().U_name.Equals(usuarioU_name))
                    {
                        continue;
                    };
                }

                //Uniendo
                ExperienciaUsuarioModel ExperienciaModel = new ExperienciaUsuarioModel
                {
                    U_name = usuarioU_name,
                    Nombre_proyecto = Experiencia.Nombre_proyecto,
                    Rol = Experiencia.Rol,
                    Resumen = Experiencia.Resumen,
                    Responsabilidades = Experiencia.Responsabilidades,
                    Tecnologias = Experiencia.Tecnologias
                };

                ListaExperienciaModel.Add(ExperienciaModel);

            }

            return Ok(ListaExperienciaModel);
        }

        // GET: api/ExperienciaUsuario/5
        [HttpGet("{u_name}")]
        public async Task<ActionResult<ExperienciaByUsuario>> GetExperienciaByUsuario(String u_name)
        {
            //Obteniendo usuario id
            var usuarioId = ObtenerIdUsuario(u_name).Result;

            var ExperienciaUsuario = ObtenerObjetoExperienciaByUsuarios(usuarioId).Result;

            //Uniendo
            ExperienciaUsuarioModel ExperienciaModel = new ExperienciaUsuarioModel
            {
                U_name = u_name,
                Nombre_proyecto = ExperienciaUsuario.Nombre_proyecto,
                Rol = ExperienciaUsuario.Rol,
                Resumen = ExperienciaUsuario.Resumen,
                Responsabilidades = ExperienciaUsuario.Responsabilidades,
                Tecnologias = ExperienciaUsuario.Tecnologias
            };

            if (ExperienciaModel == null)
            {
                return NotFound($"El usuario {u_name} no tiene experiencia 😓");
            }

            return Ok(ExperienciaModel);
        }

        // PUT: api/ExperienciaUsuario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{u_name}")]
        public async Task<IActionResult> PutExperienciaByUsuario(String u_name, PutExperienciaUsuarioModel experienciaByUsuario)
        {
            // idUsuario
            var idUsuario = ObtenerIdUsuario(u_name).Result;

            // Extrayecto objeto usuario
            ExperienciaByUsuario? ExperienciaDb = await (from ex in _context.ExperienciaByUsuarios
                                                               where ex.Id_usuario == idUsuario
                                                               select ex).FirstOrDefaultAsync();

            // Modificando el objeto
            ExperienciaDb.Nombre_proyecto = experienciaByUsuario.Nombre_proyecto;
            ExperienciaDb.Rol = experienciaByUsuario.Rol;
            ExperienciaDb.Resumen = experienciaByUsuario.Resumen;
            ExperienciaDb.Responsabilidades = experienciaByUsuario.Responsabilidades;
            ExperienciaDb.Tecnologias = experienciaByUsuario.Tecnologias;

            _context.Entry(ExperienciaDb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExperienciaByUsuarioExists(idUsuario))
                {
                    return NotFound($"El usuario no existe 😓");
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/ExperienciaUsuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExperienciaByUsuario>> PostExperienciaByUsuario(ExperienciaUsuarioModel experienciaByUsuario)
        {
            if (_context.GradoAcademicoByUsuarios == null)
            {
                return Problem("Entity set 'railwayContext.ExperienciaAcademicoByUser' es nulo.");
            }

            //Obteniendo usuario id
            var usuarioId = ObtenerIdUsuario(experienciaByUsuario.U_name).Result;

            if (ObtenerObjetoExperienciaByUsuarios(usuarioId).Result != null)
            {
                return BadRequest($"El usuario {experienciaByUsuario.U_name} ya tiene un experienciaByUsuarioData 😓");
            }

            //Juntando todo
            ExperienciaByUsuario experienciaByUsuarioData = new ExperienciaByUsuario
            {
                Nombre_proyecto = experienciaByUsuario.Nombre_proyecto,
                Rol = experienciaByUsuario.Rol,
                Resumen = experienciaByUsuario.Resumen,
                Responsabilidades = experienciaByUsuario.Responsabilidades,
                Tecnologias = experienciaByUsuario.Tecnologias,
                Id_usuario = usuarioId
            };

            _context.ExperienciaByUsuarios.Add(experienciaByUsuarioData);
            await _context.SaveChangesAsync();

            return await GetExperienciaByUsuario(experienciaByUsuario.U_name);
        }

        // DELETE: api/ExperienciaUsuario/5
        [HttpDelete("{u_name}")]
        public async Task<IActionResult> DeleteExperienciaByUsuario(String u_name)
        {
            //Obteniendo usuario id
            var usuarioId = ObtenerIdUsuario(u_name).Result;

            //Obteniendo Experiencia id
            var ExperienciaId = ObtenerIdExperiencia(usuarioId).Result;

            if (_context.ExperienciaByUsuarios == null)
            {
                return NotFound($"El usuario {u_name} no se encontro 😓");
            }
            var ExperienciaByUsuario = await _context.ExperienciaByUsuarios.FindAsync(ExperienciaId);
            if (ExperienciaByUsuario == null)
            {
                return NotFound($"El usuario {u_name} no se encontro 😓");
            }

            _context.ExperienciaByUsuarios.Remove(ExperienciaByUsuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExperienciaByUsuarioExists(int id)
        {
            return (_context.ExperienciaByUsuarios?.Any(e => e.IdExperienciaByUsuario == id)).GetValueOrDefault();
        }
        private async Task<int> ObtenerIdUsuario(String u_name)
        {
            //Obteniendo usuario id
            return await (from u in _context.Usuarios
                          where u.U_name == u_name
                          select u.Id_usuario).FirstOrDefaultAsync();
        }
        private async Task<String> ObtenerU_nameUsuario(int idUsuario)
        {
            //Obteniendo usuario id
            return await (from u in _context.Usuarios
                          where u.Id_usuario == idUsuario
                          select u.U_name).FirstOrDefaultAsync();
        }
        private async Task<int> ObtenerIdExperiencia(int idUsuario)
        {
            //Obteniendo id ExperienciaByUsuario
            return await (from ex in _context.ExperienciaByUsuarios
                          where ex.Id_usuario == idUsuario
                          select ex.IdExperienciaByUsuario).FirstOrDefaultAsync();
        }
        private async Task<ExperienciaByUsuario> ObtenerObjetoExperienciaByUsuarios(int idUsuario)
        {
            //Obteniendo ExperienciaByUsuario
            return await (from exU in _context.ExperienciaByUsuarios
                          where exU.Id_usuario == idUsuario
                          select exU).FirstOrDefaultAsync();
        }
    }
}
