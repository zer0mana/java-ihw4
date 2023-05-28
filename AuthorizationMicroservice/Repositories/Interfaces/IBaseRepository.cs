using System.Transactions;

namespace AuthorizationMicroservice.Repositories.Interfaces;

public interface IBaseRepository
{
    public TransactionScope CreateTransactionScope(IsolationLevel level = IsolationLevel.ReadCommitted);
}