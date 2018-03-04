using GigHub.Models;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GigHub.IntegrationTests
{
    //[SetUpFixture]
    public class GlobalSetUp
    {
        //[OneTimeSetUp] //LD "Nunit" attribute
        public void SetUp()
        {
            // ##########################################################################
            //LD HO PROBLEMI DI COMPATIBILITA CON LE VERSIONI NUNIT, L'HO DOVUTO ESCLUDERE
            // perche altrimenti non mi funziona l'esecuzione del test:
            // [Test]
            // public void Mine_WhenCalled_ShouldReturnUpcomingGigs()
            // ##########################################################################

            //MigrateDbToLatestVersion();
            //Seed();
        }

        private static void MigrateDbToLatestVersion()
        {
            var configuration = new GigHub.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();

            if (context.Users.Any())
                return;

            context.Users.Add(new ApplicationUser {  UserName = "user1", Name = "user1", Email = "-", PasswordHash = "-" });
            context.Users.Add(new ApplicationUser {  UserName = "user2", Name = "user2", Email = "-", PasswordHash = "-" });
            context.SaveChanges();
        }
    }
}