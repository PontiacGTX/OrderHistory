using Microsoft.EntityFrameworkCore;
using OrderHistory.Data.Entity;
using OrderHistory.Domain.Repository;
using OrderHistory.Domain.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Domain.Context
{
    public class DbFeed:IDisposable
    {

        private bool disposedValue;
        private DBContext _ctx;

        public DbFeed(DBContext context)
        {
            _ctx = context;
        }


        public async Task FeedDB()
        {
          
            var random = new Random();
            string[] productNames = { "iPhone 12", "Samsung Galaxy S21", "MacBook Air", "Dell XPS 13", "HP Pavilion", "Lenovo Legion Y540", "Asus ROG Zephyrus G14", "Acer Predator Triton 500", "MSI GS66 Stealth", "Razer Blade 15", "Microsoft Surface Pro 7", "Google Pixelbook Go", "Logitech G513", "Corsair K95 RGB Platinum", "HyperX Alloy Elite RGB", "Razer Huntsman Elite", "Corsair K100 RGB", "HyperX Pulsefire Surge", "Corsair K70 RGB MK.2", "Logitech G Pro Wireless" };
            for (int i = 0; i < productNames.Length; i++)
            {
                Product product = new Product {  Name = productNames[i] };
                try
                {
                     _ctx.Product.Add(product);
                }
                catch (Exception)
                {

                }
            }


            string[] firstNames = { "John", "Jane", "Michael", "Emily", "David", "Sarah", "Alexander", "Olivia", "William", "Ava" };
            string[] lastNames = { "Doe", "Doe", "Smith", "Brown", "Johnson", "Clark", "Jones", "Taylor", "Wilson", "Hall" };
            for (int i = 1; i <= 20; i++)
            {
                Member member = new Member { Id = i };
                member.Name = $"{firstNames[random.Next(firstNames.Length)]} {lastNames[random.Next(lastNames.Length)]}";
                try
                {
                    var st = _ctx.Database.CreateExecutionStrategy();
                    await st.ExecuteAsync(async () =>
                    {
                        using (var transaction = _ctx.Database.BeginTransaction())
                        {
                            _ctx.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Member] ON");
                                 _ctx.Member.Add(member);
                            
                            try
                            {
                                await _ctx.SaveChangesAsync();
                                transaction.Commit();
                                
                            }
                            catch (Exception ex)
                            {

                            }

                        }

                    });
                }
                catch (Exception)
                {

                }
            }

            _ctx.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Member] OFF");
            for (int i = 1; i <= 50; i++)
            {
                int productId = random.Next(1, ((int) _ctx.Product.Count()));
                int memberId = random.Next(1, ((int)_ctx.Member.Count()));
                Order order = new Order {  ProductId = productId, MemberId = memberId, DateOrder =DateTime.Now};
                try
                {
                    _ctx.Order.Add(order);
                }
                catch (Exception ex)
                {

                }
            }

            await _ctx.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _ctx = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DbFeed()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
