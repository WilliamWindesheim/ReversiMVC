using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReversiMVCProper.Data;
using ReversiMVCProper.Models;

namespace ReversiMVCProper.Controllers
{
    public class SpelersController : Controller
    {
        private readonly ReversiDbContext _context;
        private readonly ReversiApiService _service;

        public SpelersController(ReversiApiService service, ReversiDbContext context)
        {
            _service = service;
            _context = context;
        }

        // GET: Spelers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Spelers.ToListAsync());
        }

        // GET: Spelers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Spelers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guid,Naam,AantalGewonnen,AantalVerloren,AantalGelijk")] Speler speler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(speler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(speler);
        }
        
        // GET: Spelers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speler = await _context.Spelers.FindAsync(id);
            if (speler == null)
            {
                return NotFound();
            }
            return View(speler);
        }
        
        // GET: Spelers/EditRole/5
        public async Task<IActionResult> EditRole(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speler = await _context.Spelers.FindAsync(id);
            if (speler == null)
            {
                return NotFound();
            }

            if (speler.Roles == RolesEnum.Speler)
            {
                return Unauthorized();
            }
            return View(speler);
        }

        // POST: Spelers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Guid,Naam,Roles,AantalGewonnen,AantalVerloren,AantalGelijk")] Speler speler)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId != speler.Guid)
            {
                var currentSpeler = await _context.Spelers.FirstOrDefaultAsync(speler => speler.Guid == currentUserId);
                if (currentSpeler == null || currentSpeler.Roles == RolesEnum.Speler)
                    return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpelerExists(speler.Guid))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(speler);
        }
        // GET: Spelers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();
            
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentSpeler = await _context.Spelers
                .FirstOrDefaultAsync(m => m.Guid == currentUserId);
            if (currentSpeler == null)
                return NotFound();
            if (currentSpeler.Roles == RolesEnum.Speler)
                return Unauthorized();

            var speler = await _context.Spelers
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (speler == null)
                return NotFound();

            return View(speler);
        }

        // POST: Spelers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var speler = await _context.Spelers.FindAsync(id);
            _context.Spelers.Remove(speler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpelerExists(string id)
        {
            return _context.Spelers.Any(e => e.Guid == id);
        }
    }
}
