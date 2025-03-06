#nullable enable
using System;

namespace ParkingLotApp
{
    internal static class Program
    {
        public static void Main()
        {
            ParkingLot? parkingLot = null;

            while (true)
            {
                var input = Console.ReadLine();
                if (input == "exit") break;
                if (input != null)
                {
                    var command = input.Split(' ');

                    switch (command[0])
                    {
                        case "create_parking_lot":
                            parkingLot = new ParkingLot(int.Parse(command[1]));
                            break;
                        case "park":
                            var vehicle = new Vehicle(
                                command[1], 
                                Enum.Parse<VehicleType>(command[3]), 
                                command[2]);
                            parkingLot?.AddVehicle(vehicle);
                            break;
                        case "leave":
                            parkingLot?.GetParkingPrice(int.Parse(command[1]));
                            parkingLot?.RemoveVehicle(int.Parse(command[1]));
                            break;
                        case "status":
                            parkingLot?.GetStatus();
                            break;
                        case "type_of_vehicles":
                            parkingLot?.GetNumberOfVehiclesByType(command[1]);
                            break;
                        case "registration_numbers_for_vehicles_with_ood_plate":
                            parkingLot?.GetOddRegistrationNumbers();
                            break;
                        case "registration_numbers_for_vehicles_with_event_plate":
                            parkingLot?.GetEvenRegistrationNumbers();
                            break;
                        case "registration_numbers_for_vehicles_with_colour":
                            parkingLot?.GetRegistrationNumbersByColor(command[1]);
                            break;
                        case "slot_number_for_registration_number":
                            parkingLot?.GetLotNumberByRegistrationNumber(command[1]);
                            break;
                        case "slot_numbers_for_vehicles_with_colour":
                            parkingLot?.GetLotNumbersByColor(command[1]);
                            break;
                        default:
                            Console.WriteLine("Invalid command");
                            break;
                    }
                }
            }
        }
    }
}