namespace LabApp.DataAccess.Data.Entities
{
    public class Test : EntityBase
    {
        public string TestName { get; set; }

        public string TestType { get; set; }

        public decimal Price { get; set; }

        public override string ToString() => $"Test:\n" +
            $"\tId: {Id},\n" +
            $"\tTest name: {TestName},\n" +
            $"\tTest type: {TestType},\n" +
            $"\tPrice: {Price:N2}zł";
    }
}
