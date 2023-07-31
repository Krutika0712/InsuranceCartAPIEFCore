using Castle.Core.Resource;
using InsuranceCartAPIEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCartAPIEFCore.Repository
{
    public class TransactionRepo : ITransactionDetail<TransactionDetail>
    {
        private readonly InsManagementContext db;
        public TransactionRepo(InsManagementContext _db)
        {
            db = _db;
        }

        public string AddNewTransaction(TransactionDetail t)
        {
            string message;
            if (t != null)
            {
                db.TransactionDetails.Add(t);
                db.SaveChanges();
                message = "Record Added";
                return message;
            }
            else
            {
                message = "Error";
                return message;
            }
        }

        //public async Task<TransactionDetail> AddNewTransaction(TransactionDetail t)
        //{
        //    db.TransactionDetails.Add(t);
        //    await db.SaveChangesAsync();
        //    return t;

        //}

        public async Task<TransactionDetail> DeleteTransaction(int id)
        {
            TransactionDetail t = db.TransactionDetails.Where(x => x.ApplyId == id).SingleOrDefault();
            if (t != null)
            {
                db.TransactionDetails.Remove(t);
                await db.SaveChangesAsync();
            }
             
            return t;
        }

        public List<TransactionDetail> GetAllTransaction()
        {
          
             return db.TransactionDetails.ToList();
            
           
        }


        public async Task<TransactionDetail> GetTransactionById(int id)
        {
            TransactionDetail t = await db.TransactionDetails.FirstOrDefaultAsync(x => x.ApplyId == id);
            return t;
        }

        public async Task UpdateTransaction(int id, TransactionDetail t)
        {
            db.TransactionDetails.Update(t);
            await db.SaveChangesAsync();

        }
    }
}
