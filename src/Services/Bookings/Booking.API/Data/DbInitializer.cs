namespace MRA.Bookings.Data
{
    public class DbInitializer
    {
        public static void Initialize(MRABooksDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
