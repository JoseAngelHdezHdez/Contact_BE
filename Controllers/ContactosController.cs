using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Contactos_BE.Context;
using Contactos_BE.Models;

namespace Contactos_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactosController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public ContactosController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Contactos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contacto>>> GetContactos()
        {
          if (_context.Contactos == null)
          {
              return NotFound();
          }
            return await _context.Contactos.ToListAsync();
        }

        // GET: api/Contactos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contacto>> GetContacto(int id)
        {
          if (_context.Contactos == null)
          {
              return NotFound();
          }
            var contacto = await _context.Contactos.FindAsync(id);

            if (contacto == null)
            {
                return NotFound();
            }

            return contacto;
        }

        // PUT: api/Contactos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContacto(int id, Contacto contacto)
        {
            if (id != contacto.Id)
            {
                return BadRequest();
            }

            _context.Entry(contacto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contactos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contacto>> PostContacto(Contacto contacto)
        {
          if (_context.Contactos == null)
          {
              return Problem("Entity set 'AplicationDbContext.Contactos'  is null.");
          }
            _context.Contactos.Add(contacto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContacto", new { id = contacto.Id }, contacto);
        }

        // DELETE: api/Contactos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContacto(int id)
        {
            if (_context.Contactos == null)
            {
                return NotFound();
            }
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }

            _context.Contactos.Remove(contacto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactoExists(int id)
        {
            return (_context.Contactos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
