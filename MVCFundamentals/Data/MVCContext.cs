using Microsoft.EntityFrameworkCore;
using MVCFundamentals.Models;

namespace MVCFundamentals.Data
{
    public class MVCContext:DbContext
    {
        //DBcontext konfigurasyonu
        public MVCContext(DbContextOptions<MVCContext> options) : base(options)
        {

        } 

        //database e gonderilecek modeller
        public DbSet<Car> Cars { get; set; }
    }
}
