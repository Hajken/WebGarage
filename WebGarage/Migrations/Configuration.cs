namespace WebGarage.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
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
            var rnd = new Random(new System.DateTime().Millisecond);

            var members = new List<Member>();

            for (int i = 0; i < 20; i++)
            {
                var m = new Member
                {
                    FirstName = RandomString(rnd, 5),
                    LastName = RandomString(rnd, 10),
                    PersonNumber = RandomStringOnlyNumbers(rnd, 12),
                };

                members.Add(m);
            }

            context.Members.AddOrUpdate(x => x.PersonNumber, members.ToArray());

            context.SaveChanges();

            var vts = new string[]
            {
                "Car",
                "Truck",
                "Boat",
                "AirPlane",
                "Bicycle",
                "Motorcycle",
                "Other",
            };

            var sizes = new Dictionary<string, int>()
            {
                { "Car", 1 },
                { "Truck", 4 },
                { "Boat", 2 },
                { "AirPlane", 3 },
                { "Bicycle", 1 },
                { "Motorcycle", 1 },
                { "Other", 1 },
            };

            var vehicleTypes = new List<VehicleType>();

            foreach (var item in vts)
            {
                var vt = new VehicleType
                {
                    Name = item,
                    Size = sizes[item],
                };

                vehicleTypes.Add(vt);
            }

            context.VehicleTypes.AddOrUpdate(x => x.Name, vehicleTypes.ToArray());

            context.SaveChanges();

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

            var lotNumber = 0;

            foreach (var member in members)
            {
                var numberVehicles = rnd.Next(0, 3);

                for (int j = 0; j < numberVehicles; j++)
                {
                    var registrationNumber = RandomString(rnd, 6);
                    var numberOfWheels = rnd.Next(1, 5);
                    var model = models[rnd.Next(0, models.Length)];
                    var color = (Vehicle.Colors)rnd.Next(0, 6);
                    var vehicleType = RandomVehicleType(rnd, vehicleTypes);

                    var parkingSpaces = new List<ParkingSpace>();

                    for (int i = 0; i < vehicleType.Size; i++)
                    {
                        lotNumber++;

                        var ps = new ParkingSpace
                        {
                            Lot = lotNumber,
                        };

                        parkingSpaces.Add(ps);
                    }

                    context.ParkingSpaces.AddOrUpdate(parkingSpaces.ToArray());

                    var vehicle = new Vehicle
                    {
                        Member = member,
                        RegistrationNumber = registrationNumber,
                        NumberOfWheels = numberOfWheels,
                        Model = model,
                        Color = color,
                        VehicleType = vehicleType,
                        ParkingSpaces = parkingSpaces.ToArray(),
                    };

                    vehicles.Add(vehicle);
                }
            }

            context.Vehicles.AddOrUpdate(x => x.RegistrationNumber, vehicles.ToArray());

            context.SaveChanges();

            var lotsLeft = 200 - lotNumber;

            for (int i = 0; i < lotsLeft; i++)
            {
                lotNumber++;

                var ps = new ParkingSpace
                {
                    Lot = lotNumber,
                };

                context.ParkingSpaces.Add(ps);
            }

            context.SaveChanges();

            var pss = from p in context.ParkingSpaces
                      select p;
            var n = 0;

            foreach (var p in pss)
            {
                n++;

                if (n % 20 == 0)
                {
                    p.Edge = true;

                    n = 0;
                }
            }

            context.SaveChanges();
        }

        private static string RandomString(Random random, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string RandomStringOnlyNumbers(Random random, int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private VehicleType RandomVehicleType(Random rand, List<VehicleType> vehicletypes)
        {
            var skip = (int)(rand.NextDouble() * vehicletypes.Count());

            return vehicletypes.OrderBy(o => o.ID).Skip(skip).Take(1).First();
        }

        private Vehicle RandomVehicle(Random rand, List<Vehicle> vehicles)
        {
            var skip = (int)(rand.NextDouble() * vehicles.Count());

            return vehicles.OrderBy(o => o.ID).Skip(skip).Take(1).First();
        }

        private ParkingSpace RandomParkingSpace(Random rand, List<ParkingSpace> parkingSpaces)
        {
            var skip = (int)(rand.NextDouble() * parkingSpaces.Count());

            return parkingSpaces.OrderBy(o => o.ID).Skip(skip).Take(1).First();
        }
    }
}
