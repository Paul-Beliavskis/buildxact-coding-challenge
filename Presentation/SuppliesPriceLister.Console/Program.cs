using System;
using System.Threading.Tasks;
using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using SuppliesPriceLister.Application.HandlerRequests;

namespace SuppliesPriceLister
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            // Create the builder with which components/services are registered.
            var builder = new ContainerBuilder();
            builder.AddMediatR(typeof(GetBuildingSuppliesRequest).Assembly);
            Container = builder.Build();

            // Create the scope, resolve your IDateWriter,
            // use it, then dispose of the scope.
            using (var scope = Container.BeginLifetimeScope())
            {
                var requestHandler = scope.Resolve<MediatR.IMediator>();

                var request = new GetBuildingSuppliesRequest
                {
                    SortOrder = Application.Enums.SortOrderEnum.Desc
                };

                //var response = await requestHandler.Send(request);


                //foreach (var supply in response.Supplies)
                //{
                //    Console.WriteLine($"Supply Id: {supply.SupplyId}, Supply name: {supply.ItemName}, Supply price: {supply.Price}");
                //}

                Console.Read();
            }
        }
    }
}
