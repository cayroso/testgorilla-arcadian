﻿namespace webapi.Models.Responses
{
    public class GetTransactions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Cost { get; set; }
    }
}
