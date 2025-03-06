using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingLotApp
{
    public class ParkingLot
    {
        private readonly int _parkingLotSize;
        private readonly Dictionary<int, Vehicle> _parkedVehicles = new();

        public ParkingLot(int parkingLotSize)
        {
            _parkingLotSize = parkingLotSize;
            Console.WriteLine($"Created a parking lot with {parkingLotSize} slots");
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (_parkedVehicles.Count >= _parkingLotSize)
            {
                Console.WriteLine("Sorry, parking lot is full");
                return;
            }
            
            var lotNumber = Enumerable.Range(1, _parkingLotSize).Except(_parkedVehicles.Keys).First();
            _parkedVehicles.Add(lotNumber, vehicle);
            Console.WriteLine($"Allocated slot number: {lotNumber}");
        }

        public void RemoveVehicle(int lotNumber)
        {
            if (!_parkedVehicles.ContainsKey(lotNumber))
            {
                Console.WriteLine("Not found");
                return;
            }
            
            _parkedVehicles.Remove(lotNumber);
            Console.WriteLine($"Slot number {lotNumber} is free");
        }

        public void GetStatus()
        {
            
        }
    }
}