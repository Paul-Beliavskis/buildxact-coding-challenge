namespace SuppliesPriceLister
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            // Create the builder with which components/services are registered.
            var builder = new ContainerBuilder();
            builder.AddMediatR(Assembly.GetExecutingAssembly());
            Container = builder.Build();

            // Create the scope, resolve your IDateWriter,
            // use it, then dispose of the scope.
            using (var scope = Container.BeginLifetimeScope())
            {
                var test = scope.Resolve<IMediator>();


            }
        }
    }
}
