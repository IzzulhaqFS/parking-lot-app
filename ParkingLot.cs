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
            Console.WriteLine("Slot No.\tRegistration No.\tType\tColor");
            foreach (var vehicle in _parkedVehicles)
            {
                Console.WriteLine($"{vehicle.Key}\t\t{vehicle.Value.RegistrationNumber}\t\t{vehicle.Value.VehicleType}\t{vehicle.Value.Color}");
            }
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