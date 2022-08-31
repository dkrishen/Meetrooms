namespace MRA.Rooms.Data
{
    public class DbInitializer
    {
        public static void Initialize(MRARoomsDBContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
