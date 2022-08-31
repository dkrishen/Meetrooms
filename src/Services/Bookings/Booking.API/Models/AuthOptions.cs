namespace MRA.Bookings.Models
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenLifeTime { get; set; }
    }
}
