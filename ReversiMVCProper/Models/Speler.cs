using System.ComponentModel.DataAnnotations;

namespace ReversiMVCProper.Models
{
    public enum RolesEnum
    {
        Speler,
        Mediator,
        Beheerder
    }
    public class Speler
    {
        [Key]
        public string Guid { get; set; }

        public string Naam { get; set; }
        public RolesEnum Roles { get; set; }

        public int AantalGewonnen { get; set; }

        public int AantalVerloren { get; set; }

        public int AantalGelijk { get; set; }
    }
}
