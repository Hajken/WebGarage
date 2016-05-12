namespace WebGarage.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebGarage.DAL.GarageContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DAL.GarageContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var vehicles = new List<Vehicle>();


            var models = new string[]
            {
                "Ford",
                "BMW",
                "Volvo",
                "Mercedes",
                "Saab",
                "Opel",
            };


            var rnd = new Random(new System.DateTime().Millisecond);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var regNumberRandom = RandomString(rnd, 6);
                    var numberOfWheels = rnd.Next(1,5);

                    var modelRandom = models[rnd.Next(0, models.Length)];

                    var colorRandom = (Colors)rnd.Next(0,5);
                    var vehicleTypesRandom = (VehicleTypes)rnd.Next(0, 5);

                    var vehicle = new Vehicle
                    {
                        RegistrationNumber = regNumberRandom,
                        NumberOfWheels = numberOfWheels,
                        Model = modelRandom,
                        Color = colorRandom,
                        VehicleType = vehicleTypesRandom
                    };

                    vehicles.Add(vehicle);
                }
            }

            context.Vehicles.AddOrUpdate(p => p.RegistrationNumber, vehicles.ToArray());
        }

        private static string RandomString(Random random, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
