namespace MRA.Users.Data
{
    public class DbInitializer
    {
        public static void Initialize(MRAIdentityDBContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
