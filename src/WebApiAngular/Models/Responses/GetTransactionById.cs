namespace WebApiAngular.Models.Responses
{
    public class GetTransactionById
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Cost { get; set; }
    }
}
