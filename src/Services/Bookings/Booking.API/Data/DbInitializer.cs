namespace MRA.Bookings.Data
{
    public class DbInitializer
    {
        public static void Initialize(MRABookingsDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
