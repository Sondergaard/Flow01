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

            services.AddSingleton<Dispatcher>();
            services.AddMediatR(asm);

            var sp = services.BuildServiceProvider();

            var request = new ChangeSupplier();
            request.CustomerNo = 5;

            var dispatcher = sp.GetRequiredService<Dispatcher>();

            var response2 = await dispatcher.DispatchAsync(request);

        }
    }



    class ChangeSupplierHandler : Flow1<ChangeSupplier>
    {
        public override Task<bool> ValidateAsync(ChangeSupplier request, CancellationToken cancellationToken)
        {
            return base.ValidateAsync(request, cancellationToken);
        }
    }


}