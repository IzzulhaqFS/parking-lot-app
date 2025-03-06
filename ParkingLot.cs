using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingLotApp
{
    public class ParkingLot
    {
        private readonly int _parkingLotSize;
        private readonly Dictionary<int, Vehicle> _parkedVehicles = new();
        private Dictionary<int, DateTime> _parkingDates = new();
        private readonly int _parkingPrice = 5000;

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
            _parkingDates.Add(lotNumber, DateTime.Now);
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
            _parkingDates.Remove(lotNumber);
            Console.WriteLine($"Slot number {lotNumber} is free");
        }

        public void GetParkingPrice(int lotNumber)
        {
            if (!_parkedVehicles.TryGetValue(lotNumber, out var vehicle))
            {
                Console.WriteLine("Not found");
                return;
            }

            var registrationNumber = vehicle.RegistrationNumber;
            
            var checkInDate = _parkingDates[lotNumber];
            var difference = DateTime.Now - checkInDate;
            var totalHours = difference.Hours;

            if (totalHours == 0) totalHours = 1;
            
            var price = totalHours * _parkingPrice;
            Console.WriteLine($"Parking price for {registrationNumber}: {price}");
        }

        public void GetStatus()
        {
            Console.WriteLine("Slot No.\tRegistration No.\tType\tColor");
            foreach (var vehicle in _parkedVehicles)
            {
                Console.WriteLine($"{vehicle.Key}\t\t{vehicle.Value.RegistrationNumber}\t\t{vehicle.Value.VehicleType}\t{vehicle.Value.Color}");
            }
        }

        public void GetAvailableLotNumber()
        {
            var availableLot = _parkingLotSize - _parkedVehicles.Count;
            Console.WriteLine(availableLot);
        }

        public void GetOccupiedLotNumber()
        {
            Console.WriteLine(_parkedVehicles.Count);
        }

        public void GetNumberOfVehiclesByType(string vehicleType)
        {
            var numberOfVehicles = _parkedVehicles.Values
                .Count(vehicle => vehicle.VehicleType == Enum.Parse<VehicleType>(vehicleType));
            Console.WriteLine(numberOfVehicles);
        }

        public void GetOddRegistrationNumbers()
        {
            var registrationNumbers = _parkedVehicles.Values
                .Where(vehicle => int.TryParse(
                    new string(vehicle.RegistrationNumber.Where(char.IsDigit).ToArray()), 
                    out var lastDigit) && lastDigit % 2 == 1)
                .Select(vehicle => vehicle.RegistrationNumber);
            var enumerable = registrationNumbers.ToList();
            Console.WriteLine(enumerable.Any() ? string.Join(", ", enumerable) : "No vehicle found");
        }
        
        public void GetEvenRegistrationNumbers()
        {
            var registrationNumbers = _parkedVehicles.Values
                .Where(vehicle => int.TryParse(
                    new string(vehicle.RegistrationNumber.Where(char.IsDigit).ToArray()), 
                    out var lastDigit) && lastDigit % 2 == 0)
                .Select(vehicle => vehicle.RegistrationNumber);
            var enumerable = registrationNumbers.ToList();
            Console.WriteLine(enumerable.Any() ? string.Join(", ", enumerable) : "No vehicle found");
        }

        public void GetRegistrationNumbersByColor(string color)
        {
            var registrationNumbers = _parkedVehicles.Values
                .Where(vehicle => vehicle.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                .Select(vehicle => vehicle.RegistrationNumber);
            var enumerable = registrationNumbers.ToList();
            Console.WriteLine(enumerable.Any() ? string.Join(", ", enumerable) : "No vehicle found");
        }

        public void GetLotNumbersByColor(string color)
        {
            var lotNumbers = _parkedVehicles
                .Where(pair => pair.Value.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                .Select(pair => pair.Key);
            var enumerable = lotNumbers.ToList();
            Console.WriteLine(enumerable.Any() ? string.Join(", ", enumerable) : $"No {color} vehicle found");
        }

        public void GetLotNumberByRegistrationNumber(string registrationNumber)
        {
            var lotNumber = _parkedVehicles
                .FirstOrDefault(pair =>
                    pair.Value.RegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase))
                .Key;
            Console.WriteLine(lotNumber > 0 ? lotNumber : "Not found");
        }
    }
}