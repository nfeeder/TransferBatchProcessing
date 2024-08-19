

using Microsoft.Extensions.DependencyInjection;
using TransferBatchProcessingData.Data;
using TransferBatchProcessingData.Data.Interface;
using TransferBatchProcessingData.Repositories;
using TransferBatchProcessingData.Repositories.Interfaces;
using TransferBatchProcessingServices.Mappers;
using TransferBatchProcessingServices.Mappers.Interfaces;
using TransferBatchProcessingServices.Services;
using TransferBatchProcessingServices.Services.Interfaces;


namespace TransferBatchProcessing
{
    class Program
    {
        static async Task Main(string[] args)
        {

            //service collection
            var serviceCollection = new ServiceCollection();

            //config services
            serviceCollection.AddScoped<ICommissions, CommissionsService>();
            serviceCollection.AddScoped<IDataProvider, DataProvider>();
            serviceCollection.AddScoped<IRepository, Repository>();
            serviceCollection.AddScoped<IMapperDBtoDTO, MapperDBtoDTO>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var getCommissions = serviceProvider.GetRequiredService<ICommissions>();

         
            var fileName = Console.ReadLine();
            string folderPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "File");
            string filePath = Path.Combine(folderPath, fileName);

            if (string.IsNullOrEmpty(fileName) || !File.Exists(filePath))
            {
                Console.WriteLine("Invalid file path. Please make sure the file exists and try again.");
                return;
            }

            var results = await getCommissions.CalculateCommissionsAsync(filePath);

            foreach (var result in results)
            {
                Console.WriteLine($"{result.AccountId},{result.Commission}");
            }
        }
    }
}