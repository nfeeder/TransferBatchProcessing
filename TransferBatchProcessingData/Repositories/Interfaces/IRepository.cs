using TransferBatchProcecingModels.Domain;

namespace TransferBatchProcessingData.Repositories.Interfaces
{
    public interface IRepository
    {
        Task<List<Transfer>> GetTransfersAsync(string filePath);
    }
}
