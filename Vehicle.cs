namespace ParkingLotApp
{
    public class Vehicle
    {
        public string RegistrationNumber { get; set; }
        public VehicleType VehicleType { get; set; }
        public string Color { get; set; }

        public Vehicle(string registrationNumber, VehicleType vehicleType, string color)
        {
            RegistrationNumber = registrationNumber;
            VehicleType = vehicleType;
            Color = color;
        }
    }
}