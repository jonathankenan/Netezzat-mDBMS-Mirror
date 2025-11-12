namespace mDBMS.QueryProcessor.Contracts
{
    public interface IConcurrencyControlManager
    {
        int BeginTransaction();

        ConcurrencyResponse ValidateObject(TransactionAction action);

        void EndTransaction(int transactionId, TransactionStatus status);
    }
}
