using TransferBatchProcecingModels.DTOs;

namespace TransferBatchProcessingServices.Mappers.Interfaces
{
    public interface IMapperDBtoDTO
    {
        TransferCommissionsDto MapCommissions(string key, decimal totalCommission);
    }
}
