using System.Transactions;

namespace OrderHandlerMicroservice.Repositories.Interfaces;

public interface IBaseRepository
{
    public TransactionScope CreateTransactionScope(IsolationLevel level = IsolationLevel.ReadCommitted);
}