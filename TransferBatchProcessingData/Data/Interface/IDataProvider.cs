using TransferBatchProcecingModels.Domain;

namespace TransferBatchProcessingData.Data.Interface
{
    public interface IDataProvider
    {
        Task<List<Transfer>> GetTransfersAsync(string filePath);
    }
}
