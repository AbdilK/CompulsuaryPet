using Microsoft.Extensions.DependencyInjection;
using PetShopCompulsuary.Core.Application.Services.ApplicationService;
using PetShopCompulsuary.Core.Application.Services.ApplicationService.DomainService;
using PetShopCompulsuary.Core.Application.Services.DomainService;
using PetShopCompulsuary.Infrastructure.Static.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopCompulsuary.Petshop
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IPetRepository, PetRepository>();

            serviceCollection.AddScoped<IPetService, PetService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var petService = serviceProvider.GetService<IPetService>();

            var printer = new Printer(petService);
        }
    }
}
