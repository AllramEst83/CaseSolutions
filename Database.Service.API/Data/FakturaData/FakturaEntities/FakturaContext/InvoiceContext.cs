
using Microsoft.EntityFrameworkCore;
using ResponseModels.DatabaseModels;

namespace Database.Service.API.Data.FakturaData.FakturaEntities.FakturaContext
{
    public class InvoiceContext : DbContext
    {
        public InvoiceContext(DbContextOptions<InvoiceContext> options) : base(options)
        {

        }

        public DbSet<Invoice> Invoices { get; set; }  

        //dotnet ef migrations add --> namn på migrationen: start
        //dotnet ef database update
    }
}
