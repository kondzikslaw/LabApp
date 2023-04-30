namespace LabApp.Entities
{
    public class Product : EntityBase
    {
        public string? ProductName { get; set; }

        public string? ProductType { get; set; }

        public override string ToString() => $"Id: {Id}, Product name: {ProductName}, Product Type: {ProductType}";
    }
}
