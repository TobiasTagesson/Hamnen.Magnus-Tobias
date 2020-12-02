using System;
using System.Collections.Generic;
using System.Linq;

namespace Hamnen
{
    public class Dock
    {
        private readonly Helpers _helper;
        private Boat[] _boatSlots;
        private List<Boat> _rejectedBoats;
        private int day = 0;

        public Dock()
        {
            _helper = new Helpers();
            _boatSlots = new Boat[26];
            _rejectedBoats = new List<Boat>();
        }

        public void StartDockManager()
        {
            while (true)
            {
                MakeNewDay();
                BoatDepartures();
                HandleBoatArrivals();
                PrintResult();
                CountDownTimeToDeparture();
            }
        }

        private void HandleBoatArrivals()
        {
            //generate 5 random boats
            foreach (var boat in _helper.RandomBoatGenerator(5))
            {
                HandleBoat(boat);
            }
        }

        private void HandleBoat(Boat boat)
        {
            
            //find empty slot for this boat
            var result = GetEmptySlot(boat);

            //result 0 == no empty slot matching this boat
            if (result == 0)
            {
                _rejectedBoats.Add(boat);
            }
            else
            {
                AddBoatToEmptySlot(boat, result);
            }
        }

        private void AddBoatToEmptySlot(Boat boat, int slot)
        {
            for (int i = 0; i < (int)boat.NumberOfSlotsInDock; i++)
            {
                _boatSlots[slot] = boat;
                slot++;
            }
        }



        private int GetEmptySlot(Boat boat)
        {
            //Begin check with slot 1 in Dock
            for (int slot = 1; slot < _boatSlots.Length; slot++)
            {
                // Check if the slot already contains a single rowboat and also if the boat object is a rowboat - Add them both to a Rowboatpair-tuple
                if (_boatSlots[slot] != null && boat.Type == Helpers.BoatType.Roddbåt && _boatSlots[slot].Type == Helpers.BoatType.Roddbåt)
                {
                    CreateRowBoatPairAndAddItToSlot(boat, slot);
                    return default;
                }
                //if slot is available: null
                else if (_boatSlots[slot] == null)
                {
                    bool slotIsAvailable = true;

                    int spaceRequired = slot + (int)boat.NumberOfSlotsInDock;
                    for (int x = slot; x < spaceRequired; x++)
                    {
                        if (_boatSlots.Length <= x)
                        {
                            return default;
                        }

                        //if slot 'x' is not empty set available to false
                        if (_boatSlots[x] != null)
                        {
                            slot = x;
                            slotIsAvailable = false;
                            break;
                        }
                    }
                    //when found available slot return
                    if (slotIsAvailable)
                    {
                        return slot;
                    }
                }
            }
            return default;
        }

        private void CreateRowBoatPairAndAddItToSlot(Boat boat, int slot)
        {
            // Replace single rowboat in slot with rowboatpair tuple
            var rowBoatPair = new RowBoatPair();
            rowBoatPair.RowBoatTuple = new Tuple<Boat, Boat>(_boatSlots[slot], boat);
            _boatSlots[slot] = rowBoatPair;
        }

        private void CountDownTimeToDeparture()
        {
            foreach (var boat in _boatSlots.Distinct())
            {
                if (boat != null)
                    boat.StaysForDays--;
            }
        }

        private void MakeNewDay()
        {
            day++;
        }

        private void BoatDepartures()
        {
            for (int slot = 1; slot < _boatSlots.Length; slot++)
            {
                if (_boatSlots[slot] != null && _boatSlots[slot].StaysForDays == 0)
                {
                    _boatSlots[slot] = null;
                }
            }
        }

        private void PrintResult()
        {
            PrintHeader();

            PrintDock();

            Console.WriteLine("\r\nTryck valfri tangent för att fortsätta....");
            Console.ReadKey();
        }

        private void PrintHeader()
        {
            Console.Clear();
            Console.WriteLine("Dag: " + day);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Avvisade: " + _rejectedBoats.Count());
            Console.ResetColor();
            Console.WriteLine("Plats\tTyp av båt\tBåtID\tDagar kvar");
        }

        private void PrintDock()
        {
            for (int slot = 1; slot < _boatSlots.Length; slot++)
            {
                //if slot is not empty
                if (_boatSlots[slot] != null)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    var boat = _boatSlots[slot];

                    if(_boatSlots[slot].Type == Helpers.BoatType.Roddbåtspar)
                        {
                            var rowBoatPair = new RowBoatPair();
                            rowBoatPair = (RowBoatPair)_boatSlots[slot];
                            Console.WriteLine($"{slot}.\t{rowBoatPair.RowBoatTuple.Item1.Type}\t\t{rowBoatPair.RowBoatTuple.Item1.Id} \t\t{rowBoatPair.RowBoatTuple.Item1.StaysForDays}");
                            Console.WriteLine($"{slot}.\t{rowBoatPair.RowBoatTuple.Item2.Type}\t\t{rowBoatPair.RowBoatTuple.Item2.Id} \t\t{rowBoatPair.RowBoatTuple.Item2.StaysForDays}");
                            Console.ResetColor();

                    }

                    else
                    {
                    Console.WriteLine($"{slot}.\t{boat.Type}\t{boat.Id} \t{boat.StaysForDays}");
                    Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{slot}.\tledig plats");
                    Console.ResetColor();
                }
            }
        }
    }
}
