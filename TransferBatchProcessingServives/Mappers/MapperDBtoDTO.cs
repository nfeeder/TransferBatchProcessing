using TransferBatchProcecingModels.DTOs;
using TransferBatchProcessingServices.Mappers.Interfaces;

namespace TransferBatchProcessingServices.Mappers
{
    public class MapperDBtoDTO : IMapperDBtoDTO
    {
        public TransferCommissionsDto MapCommissions(string transfer, decimal commission)
        {
            return new TransferCommissionsDto()
            {
                AccountId = transfer,
                Commission = formatCommission(commission)
            };
        }

        private string formatCommission(decimal commission)
        {
            if (commission == Math.Truncate(commission))
            {
                return commission.ToString("0");
            }
            else return commission.ToString();

        }
    }
}
