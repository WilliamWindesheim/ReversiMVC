using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReversiMVCProper.Data;
using ReversiMVCProper.Models;

namespace ReversiMVCProper.Controllers
{
    public class SpelsController : Controller
    {
        private readonly ReversiApiService _service;
        private readonly ReversiDbContext _context;

        public SpelsController(ReversiApiService service, ReversiDbContext context)
        {
            _service = service;
            _context = context;
        }
        
        // GET: spels
        public async Task<IActionResult> Index()
        {
            //TODO als je al in een actief spel zit redirect naar spel
            try{
                var items = _service.GetAllOpenSpellen();
                return View(items);
            } catch(Exception ex)
            {
                return View(new List<Spel>() { new Spel(){ Omschrijving=ex.Message + " | " + ex.ToString()} });
            }
        }
        //Get: Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        //Get: Play
        public async Task<IActionResult> Play(int id)
        {
            if (id == 0)
                return RedirectToAction("Index");
            var spel = _service.GetSpel(id);
            var item = User.FindFirst(ClaimTypes.NameIdentifier);
            if (item == null)
                return Redirect("Identity/Account/Login");
            var currentUserId = item.Value;
            if (spel == null)
                return NotFound();
            if (spel.Speler1Token != currentUserId)
                _service.JoinSpel(currentUserId, id);
            return View(spel);

        }

        // POST: Spels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Omschrijving")] Spel spel)
        {
            var item = User.FindFirst(ClaimTypes.NameIdentifier);
            if (item == null)
                return Unauthorized();

            var currentuserid = item.Value;
            Speler speler = _context.Spelers.FirstOrDefault(s => s.Guid == currentuserid);
            if (speler == null)
            {
                speler = new Speler() { Guid = currentuserid, Naam = "anoniem" };
                _context.Spelers.Add(speler);
                _context.SaveChanges();
            }
            if (ModelState.IsValid)
            {
                _service.CreateSpel(speler.Guid, spel.Omschrijving);
            }
            return View(spel);
        }
        
        // GET: Spels/endgame
        [HttpGet("endgame")]
        public async Task<IActionResult> EndGame(int id)
        {
            var spel = _service.GetSpel(id);
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (spel == null)
                return Redirect(Url.Action("Index", "Spelers"));
            if (spel.Winnaar == 0 || spel.Winnaar == 3)
                return Redirect(Url.Action("Index", "Spelers"));
            Speler speler1 = _context.Spelers.FirstOrDefault(s => s.Guid == spel.Speler1Token);
            Speler speler2 = _context.Spelers.FirstOrDefault(s => s.Guid == spel.Speler2Token);
            if (speler1 == null || speler2 == null)
                return Redirect(Url.Action("Index", "Spelers"));

            var winnaar = spel.Winnaar == 1 ? speler1.Guid : (spel.Winnaar == 2 ? speler2.Guid : String.Empty);
            
            if (winnaar == String.Empty)
            {
                speler1.AantalGelijk++;
                speler2.AantalGelijk++;
            } else if (speler1.Guid == winnaar){
                speler1.AantalGewonnen++;
                speler2.AantalVerloren++;
            } else{
                speler1.AantalVerloren++;
                speler2.AantalGewonnen++;
            }
            
            _context.Entry(speler1).State = EntityState.Modified;
            _context.Entry(speler2).State = EntityState.Modified;
            _context.SaveChanges();
            _service.EndGame(id);

            return Redirect(Url.Action("Index", "Spelers"));
        }
        private bool SpelExists(int id)
        {
            return _context.Spel.Any(e => e.ID == id);
        }
    }
}
