using Database.Service.API.Data.AerendeData.AerendeEntities.AerendeContext;
using Database.Service.API.Data.FakturaData.FakturaEntities.FakturaContext;
using Database.Service.API.DataAccess.Seeders.Interfaces;
using ResponseModels.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Service.API.DataAccess.Seeders
{
    public class InvoiceSeeder : IInvoiceSeeder
    {
        public AerendeContext _aerendeContext { get; }
        public InvoiceContext _invoiceContext { get; }

        public InvoiceSeeder(AerendeContext aerendeContext, InvoiceContext invoiceContext)
        {
            _aerendeContext = aerendeContext;
            _invoiceContext = invoiceContext;
        }



        public void SeedInvoices()
        {
            double discount = 0.20;

            double one = 30 * 2666;
            double two = 45 * 1500;
            double three = 35 * 2000;
            double totalSum = (one + two + three) * discount;

            var now = DateTime.Now;

            List<Invoice> invoices = new List<Invoice>()
            {
                new Invoice(){
                    IssueDate = now,
                    DueDate = now.AddDays(25),
                    Discount = discount,
                    TotalSum = totalSum,
                                    }
            };

            _invoiceContext.Invoices.AddRange(invoices);
            _invoiceContext.SaveChanges();
        }

    }
}
