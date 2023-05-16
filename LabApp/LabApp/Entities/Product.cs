namespace LabApp.Entities
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

        public override string ToString() => $"Id: {Id}, Product name: {ProductName}, Product Type: {ProductType}, Arrival Temperature: {ArrivalTemperature}°C, Storage Temperature: {StorageTemperature}°C, Container: {Container}, Arrival Date: {ArrivalDate}, Registration Date: {RegistrationDate}";
    }
}
