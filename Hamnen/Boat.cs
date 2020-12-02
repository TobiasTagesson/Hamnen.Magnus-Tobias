using System;
using static Hamnen.Helpers;

namespace Hamnen
{
    public enum NumberOfSlotsInDock { RowBoatHalfSlot =1 , RowBoatPair = 1, SpeedBoat = 1, SailBoat, CargoShip = 4 }

    internal class Boat
    {
        public string Id { get; set; }
        public int Weight { get; set; }
        public int TopSpeed { get; set; }
        public NumberOfSlotsInDock NumberOfSlotsInDock { get; set; }
        public int StaysForDays { get; set; }
        public BoatType Type { get; set; }
    }
    internal class CargoShip : Boat
    {
        public int ContainersOnShip { get; set; }
    }
    internal class SpeedBoat : Boat
    {
        public int HorsePower { get; set; }
    }
    internal class SailBoat : Boat
    {
        public int LengthInFeet { get; set; }
    }
    internal class RowBoat : Boat
    {
        public int MaxNoOfPassengers { get; set; }

    }

    internal class RowBoatPair : Boat
    {
        public RowBoatPair()
        {
            Type = BoatType.Roddbåtspar;
            StaysForDays = 1;
        }
       
        public int MaxNoOfPassengers { get; set; }
        public Tuple <Boat, Boat> RowBoatTuple { get; set; }
    }
}

