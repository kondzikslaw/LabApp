namespace LabApp.Entities
{
    public class Test : EntityBase
    {
        public string? TestName { get; set; }

        public string? TestType { get; set; }

        public override string ToString() => $"Id: {Id}, Test name: {TestName}, Test type: {TestType}";
    }
}
