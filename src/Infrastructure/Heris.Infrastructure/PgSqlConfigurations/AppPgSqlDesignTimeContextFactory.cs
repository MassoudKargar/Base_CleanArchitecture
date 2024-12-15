//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;

//namespace Heris.Infrastructure.PgSqlConfigurations
//{
//    public class AppPgSqlDesignTimeContextFactory : IDesignTimeDbContextFactory<BaseDbContext>
//    {
//        public BaseDbContext CreateDbContext(string[] args)
//        {
//            Console.WriteLine("Beginning Migrations");

//            string MigrationConStr = "Server=localhost;Port=5432;Database=sample;User Id=postgres;Password=mysecretpassword;";

//            Console.WriteLine($"{MigrationConStr}");

//            Task.Delay(2000);

//            var optionsBuilder = new DbContextOptionsBuilder<BaseDbContext>();

//            optionsBuilder.UseNpgsql(MigrationConStr);

//            return new BaseDbContext(optionsBuilder.Options);
//        }
//    }
//}
