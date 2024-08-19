namespace TransferBatchProcecingModels.Domain
{
    public class Transfer
    {
        public string AccountId { get; set; }
        public string TransferId { get; set; }
        public decimal TotalTransferAmount { get; set; }
    }
}
