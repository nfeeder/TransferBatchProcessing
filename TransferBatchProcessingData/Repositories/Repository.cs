using TransferBatchProcecingModels.Domain;
using TransferBatchProcessingData.Data.Interface;
using TransferBatchProcessingData.Repositories.Interfaces;

namespace TransferBatchProcessingData.Repositories
{
    public class Repository : IRepository
    {
        private readonly IDataProvider _dataProvider;

        public Repository(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public Task<List<Transfer>> GetTransfersAsync(string filePath)
        {
            return _dataProvider.GetTransfersAsync(filePath);
        }
    }
}
