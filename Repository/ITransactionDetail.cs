using InsuranceCartAPIEFCore.Models;

namespace InsuranceCartAPIEFCore.Repository
{
    public interface ITransactionDetail<TransactionDetail>
    {
        List<TransactionDetail> GetAllTransaction();
        string AddNewTransaction(TransactionDetail t);
        Task UpdateTransaction(int id, TransactionDetail t);
        Task<TransactionDetail> DeleteTransaction(int id);
        Task<TransactionDetail> GetTransactionById(int id);
      
    }
}
