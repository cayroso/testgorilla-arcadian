using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.DbContexts
{
    public static class AppDbContextInitializer
    {
        static Random _rnd = new Random((int)DateTime.UtcNow.Ticks);

        public static async Task Initialize(AppDbContext ctx)
        {
            if (ctx.Transactions.Any())
                return;

            await SeedTransactions(ctx);

            await ctx.SaveChangesAsync();
        }

        static async Task SeedTransactions(AppDbContext ctx)
        {
            var transactions = new List<Transaction>();

            var numberOfTrasactions = _rnd.Next(10, 21);

            for (var index = 1; index < numberOfTrasactions; index++)
            {
                var now = DateTime.UtcNow;
                var day = _rnd.Next(1, 28);

                var date = DateTime.Parse($"{now.Year}-{now.Month}-{day}");

                var transaction = new Transaction
                {
                    Name = $"Transaction #{index.ToString("D4")}",
                    Cost = _rnd.Next(1, 101),
                    Date = date
                };

                transactions.Add(transaction);
            }

            await ctx.AddRangeAsync(transactions);
        }
    }
}
