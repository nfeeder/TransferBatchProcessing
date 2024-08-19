using TransferBatchProcecingModels.DTOs;

namespace TransferBatchProcessingServices.Services.Interfaces
{
    public interface ICommissions
    {
        Task<IEnumerable<TransferCommissionsDto>> CalculateCommissionsAsync(string filePath);
    }
}
