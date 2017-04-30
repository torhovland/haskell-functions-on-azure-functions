using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FizzBuzzClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = new Program().Run();
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        async Task Run()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var azureQueue = new AzureQueue(configuration["connectionStrings:haskellstorage"]);

            for (int i = 1; i <= 20; i++)
            {
                await azureQueue.WriteAsync("fizz-buzz-requests", i.ToString());
                Console.WriteLine($"Submitted number {i}.");
            }

            while (true)
            {
                foreach (var message in await azureQueue.ReadAsync("fizz-buzz-responses"))
                    Console.WriteLine($"Received: {message}");                
            }
        }
    }
}