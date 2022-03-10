using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using PaymentAPI.Models;

namespace Payment_DetailsAPI.Models
{
    public class PrespDB
    {
        // remove connection string and migrations folder
        public static void PresPopulation(IApplicationBuilder app)
        {
            using(var serviceScope=app.ApplicationServices.CreateScope())
            {
                SendData(serviceScope.ServiceProvider.GetService<PaymentDetailContext>());
            }
        }
        private static void SendData(PaymentDetailContext context)
        {
            Console.WriteLine("Applying Migration...");
            context.Database.Migrate();//to create migrations
            if(context.paymentDetails.Any())
            {
                context.paymentDetails.AddRange(new PaymentDetails() { 
                    CardNumber="1234567890123456",
                    CardOwnerName="John Doe",
                    SecurityCode="123",
                    ExpirationDate="02/24"
                });

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Already have data...no seeding");
            }
        }
    }
}
