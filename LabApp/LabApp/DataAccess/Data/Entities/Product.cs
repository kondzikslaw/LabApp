namespace LabApp.DataAccess.Data.Entities
{
    public class Product : EntityBase
    {
        public string ProductName { get; set; }

        public string ProductType { get; set; }

        public decimal ArrivalTemperature { get; set; }

        public decimal StorageTemperature { get; set; }

        public string? Container { get; set; }

        public string ArrivalDate { get; set; }

        public string RegistrationDate { get; set; }

        public override string ToString() => $"Product:\n" +
            $"\tId: {Id},\n" +
            $"\tProduct name: {ProductName},\n" +
            $"\tProduct Type: {ProductType},\n" +
            $"\tArrival Temperature: {ArrivalTemperature}°C,\n" +
            $"\tStorage Temperature: {StorageTemperature}°C,\n" +
            $"\tContainer: {Container},\n" +
            $"\tArrival Date: {ArrivalDate},\n" +
            $"\tRegistration Date: {RegistrationDate}";
    }
}
