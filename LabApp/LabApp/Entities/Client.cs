namespace LabApp.Entities
{
    public class Client : EntityBase
    {
        public string? ClientName { get; set; }

        public string? ClientAddressStreet { get; set; }

        public string? ClientAddressNumber { get; set; }

        public string? ClientAddressCity { get; set; }

        public string? ClientAddressPostalCode { get; set; }

        public override string ToString() => $"Id: {Id}, Client name: {ClientName}, Client address: {ClientAddressStreet} {ClientAddressNumber}, {ClientAddressCity} {ClientAddressPostalCode}";
    }
}
