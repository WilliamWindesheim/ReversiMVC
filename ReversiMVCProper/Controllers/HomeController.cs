using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReversiMVCProper.Data;
using ReversiMVCProper.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReversiMVCProper.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ReversiApiService _service;
        private readonly ReversiDbContext _context;

        public HomeController(ILogger<HomeController> logger, ReversiDbContext context, ReversiApiService service)
        {
            _logger = logger;
            _service = service;
            _context = context;
        }

        public IActionResult Index()
        {
            //Check daar of er al een record voor de betreffende speler bestaat in de reversiDb database.
            //Als dat niet zo is, maak dan een nieuw speler - record aan in de reversiDb database.Het Guid neem je over van de guid zoals deze in identity - database is opgenomen.
            //Als je de defaults niet veranderd hebt, is de naam van deze database Application waarschijnlijk iets in de trant van 'aspnet-ReversiMvcApp-0232467C-DF24-4311-B2C0-DFBF56B76E9E'.
            var item = User.FindFirst(ClaimTypes.NameIdentifier);
            if (item != null) //logged in
            {
                var currentuserid = item.Value;
                if (!_context.Spelers.Any(s => s.Guid == currentuserid)) //logged in for the first time
                {
                    _context.Spelers.Add(new Speler() { Guid = currentuserid, Naam = "anoniem" });
                    _context.SaveChanges();
                }
                else //logged in before
                    if (GetActiveSpel() != "") 
                        return RedirectToAction("Play", "Spels", new {id = GetActiveSpel()});
                return RedirectToAction("Index", "Spels");
            }
            return Redirect("Identity/Account/Login");
        }

        public string GetActiveSpel()
        {
            var item = User.FindFirst(ClaimTypes.NameIdentifier);
            if (item == null) 
                return "";
            var userid = item.Value;
            Spel spel = _service.GetSpelFromSpeler(userid);
            if (spel == null) 
                return "";
            return spel.ID.ToString();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
