namespace LabApp.DataAccess.Data.Entities
{
    public class Client : EntityBase
    {
        public string? ClientName { get; set; }

        public string? ClientAddressStreet { get; set; }

        public string? ClientAddressNumber { get; set; }

        public string? ClientAddressCity { get; set; }

        public string? ClientAddressPostalCode { get; set; }

        public override string ToString() => $"Client:\n" +
            $"\tId: {Id},\n" +
            $"\tClient name: {ClientName},\n" +
            $"\tClient address: {ClientAddressStreet} {ClientAddressNumber}, {ClientAddressCity} {ClientAddressPostalCode}";
    }
}
