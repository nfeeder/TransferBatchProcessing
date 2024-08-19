using TransferBatchProcecingModels.Domain;
using TransferBatchProcessingData.Data.Interface;


namespace TransferBatchProcessingData.Data
{
    public class DataProvider : IDataProvider
    {
        public async Task<List<Transfer>> GetTransfersAsync(string filePath)
        {         
            try
            {
                var transfers = new List<Transfer>();

                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        var transfer = new Transfer(){ AccountId = parts[0], TransferId = parts[1], TotalTransferAmount = decimal.Parse(parts[2]) };
                        transfers.Add(transfer);
                    }
                }

                return transfers;
            }
            catch (Exception e)
            {
                throw new Exception(filePath, e);
            }       
        }
    }
}

