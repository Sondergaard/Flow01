using System;
using System.Threading;
using System.Threading.Tasks;
using ClassLibrary1;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var asm = typeof(Program).Assembly;
            var services = new ServiceCollection();

            services.AddSingleton<RequestDispatcher>();
            services.AddSingleton<CommandDispatcher>();
            services.AddMediatR(asm);

            var sp = services.BuildServiceProvider();

            var request = new ChangeSupplier {CustomerNo = 5};

            var requestDispatcher = sp.GetRequiredService<RequestDispatcher>();
            var commandDispatcher = sp.GetRequiredService<CommandDispatcher>();

            await commandDispatcher.DispatchAsync(new MoveIn() {CustomerNo = 3});
            var response2 = await requestDispatcher.DispatchAsync(request);

        }
    }

    class MoveInHandler : CommandHandler<MoveIn>
    {
        protected override async Task<bool> ValidateAsync(MoveIn actionData, CancellationToken cancellationToken)
        {
            await System.Console.Out.WriteLineAsync("Hello");
            return true;
        }


    }

    class ChangeSupplierHandler : ClassLibrary1.RequestHandler<ChangeSupplier>
    {


        protected override Task<bool> ValidateAsync(ChangeSupplier request, CancellationToken cancellationToken)
        {
            return base.ValidateAsync(request, cancellationToken);
        }
    }


}