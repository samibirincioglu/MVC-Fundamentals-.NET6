using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCFundamentals.Data;
using MVCFundamentals.Models;
using System;
using System.Linq;

namespace MVCFundamentals.Data
{
    //Uygulama baslatildiginda database'e hazir test verileri gondermek

    public static class SeedData
    {
        //initialization stratejisi kullanmak ve program.cs de scope a almak gerekiyor
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new MVCContext(
                serviceProvider.GetRequiredService<DbContextOptions<MVCContext>>()))
            {
                //DB de urun varsa seedi ekleme
                if (context.Cars.Any())
                {
                    return;
                }
                
                context.Cars.AddRange(
                    new Car
                    {
                        ModelName = "Honda",
                        ProductionDate = DateTime.Parse("01-01-2000"),
                        Color = "Green",
                        Price = 25000

                    },
                    new Car
                     {
                         ModelName = "Mazda",
                         ProductionDate = DateTime.Parse("01-01-2003"),
                         Color = "Black",
                         Price = 23000

                     },
                    new Car
                      {
                          ModelName = "Bmw",
                          ProductionDate = DateTime.Parse("01-01-2010"),
                          Color = "White",
                          Price = 75000

                      },
                    new Car
                       {
                           ModelName = "TOGG",
                           ProductionDate = DateTime.Parse("01-01-2024"),
                           Color = "Red",
                           Price = 100000

                       }

                    );
                context.SaveChanges();
            }
        }

    }
}
