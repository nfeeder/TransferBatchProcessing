using TransferBatchProcessingServices.Services.Interfaces;
using TransferBatchProcessingData.Repositories.Interfaces;
using TransferBatchProcecingModels.DTOs;
using TransferBatchProcessingServices.Mappers.Interfaces;


namespace TransferBatchProcessingServices.Services
{
    public class CommissionsService : ICommissions
    {
        private readonly IRepository _repository;
        private readonly IMapperDBtoDTO _mapperDBtoDTO;

        public CommissionsService(IRepository repository, IMapperDBtoDTO mapperDBtoDTO)
        {
            _repository = repository;
            _mapperDBtoDTO = mapperDBtoDTO;
        }

        public async Task<IEnumerable<TransferCommissionsDto>> CalculateCommissionsAsync(string filePath)
        {

            var transfers = await _repository.GetTransfersAsync(filePath);
            var maxTransfer = transfers.OrderByDescending(m => m?.TotalTransferAmount).FirstOrDefault()?.TransferId;

            var groupedTransfers = transfers.GroupBy(t => t.AccountId);


            return groupedTransfers.Select(group =>
            {

                var totalCommission = group.Where(t => t.TransferId != maxTransfer)
                                           .Sum(t => t.TotalTransferAmount * 0.10m);

                var commissions = _mapperDBtoDTO.MapCommissions(group.Key, totalCommission);

                return commissions;
            });
        }

    }
}
