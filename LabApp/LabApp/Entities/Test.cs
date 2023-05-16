namespace LabApp.Entities
{
    public class Test : EntityBase
    {
        public string TestName { get; set; }

        public string TestType { get; set; }

        public decimal Price { get; set; }

        public override string ToString() => $"Id: {Id}, Test name: {TestName}, Test type: {TestType}, Price: {Price:N2}zł";
    }
}
