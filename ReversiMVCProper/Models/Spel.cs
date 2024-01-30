namespace ReversiMVCProper.Models
{
    public class Spel
    {
        public int ID { get; set; }
        public string Omschrijving { get; set; }
        public string Token { get; set; }
        public int Status { get; set; }
        public int AanDeBeurt { get; set; }
        public string Speler1Token { get; set; }
        public string Speler2Token { get; set; }
        public string Bord { get; set; }
        public int Winnaar { get; set; }
    }
}
