using System;
using System.Collections.Generic;
using System.Linq;

namespace Hamnen
{
    internal class Helpers
    {
        internal enum BoatType { Motorbåt, Segelbåt, Lastfartyg, Roddbåt, Roddbåtspar }

        private readonly Random _random;

        internal Helpers()
        {
            _random = new Random();
        }

        private int GenerateRandomInt(int min, int max)
        {
            Random random = new Random();

            return random.Next(min, max);
        }

        private string GenerateRandomId()
        {
            var random = new Random();
            int length = 3;

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        internal IEnumerable<Boat> RandomBoatGenerator(int numberOfBoats)
        {
            var randomBoatList = new List<Boat>();


            for (int i = 0; i < numberOfBoats; i++)
            {
                var type = _random.Next(4);
                if (type == (int)BoatType.Motorbåt)
                {
                    randomBoatList.Add(GenerateSpeedBoat());
                }
                else if (type == (int)BoatType.Segelbåt)
                {
                    randomBoatList.Add(GenerateSailBoat());
                }
                else if (type == (int)BoatType.Lastfartyg)
                {
                    randomBoatList.Add(GenerateCargoShip());
                }
                else if (type == (int)BoatType.Roddbåt)
                {
                    randomBoatList.Add(GenerateRowBoat());
                }
            }
            
            
            return randomBoatList.OrderByDescending(size => size.NumberOfSlotsInDock);
        }

        private Boat GenerateSpeedBoat()
        {
            return new SpeedBoat
            {

                HorsePower = _random.Next(10, 1000),
                Id = $"M-{GenerateRandomId()}",
                NumberOfSlotsInDock = NumberOfSlotsInDock.SpeedBoat,
                StaysForDays = 3,
                TopSpeed = _random.Next(0, 60),
                Weight = _random.Next(200, 3000),
                Type = BoatType.Motorbåt
            };
        }

        private Boat GenerateSailBoat()
        {
            return new SailBoat
            {
                LengthInFeet = _random.Next(10, 60),
                Id = $"S-{GenerateRandomId()}",
                NumberOfSlotsInDock = NumberOfSlotsInDock.SailBoat,
                StaysForDays = 4,
                TopSpeed = _random.Next(0, 12),
                Weight = _random.Next(800, 6000),
                Type = BoatType.Segelbåt
            };
        }

        private Boat GenerateCargoShip()
        {
            return new CargoShip
            {
                ContainersOnShip = _random.Next(0, 500),
                Id = $"L-{GenerateRandomId()}",
                NumberOfSlotsInDock = NumberOfSlotsInDock.CargoShip,
                StaysForDays = 6,
                TopSpeed = _random.Next(0, 20),
                Weight = _random.Next(3000, 20000),
                Type = BoatType.Lastfartyg
            };
        }
        private Boat GenerateRowBoat()
        {
            return new RowBoat
            {
                MaxNoOfPassengers = _random.Next(1, 7),
                Id = $"R-{GenerateRandomId()}",
                NumberOfSlotsInDock = NumberOfSlotsInDock.RowBoatHalfSlot,
                StaysForDays = 1,
                TopSpeed = _random.Next(0, 3),
                Weight = _random.Next(100, 300),
                Type = BoatType.Roddbåt
            };
        }

    }
}
