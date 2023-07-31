using InsuranceCartAPIEFCore.Models;
using InsuranceCartAPIEFCore.Repository;
using log4net.Config;
using log4net.Core;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCartAPIEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionDetailsController : ControllerBase
    {
        private readonly ITransactionDetail<TransactionDetail> transaction;
       
        public TransactionDetailsController(ITransactionDetail<TransactionDetail> _transaction)
        {
            transaction = _transaction;
           
        }
        private void LogError(string message)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            ILog _logger = LogManager.GetLogger(typeof(LoggerManager));
            _logger.Info(message);
        }
        [HttpGet]

        public ActionResult<List<TransactionDetail>> GetTransactionDetails()
        {
            try
            {
                LogError("Get All Transaction Details is called");
                List<TransactionDetail> t = transaction.GetAllTransaction();
                return Ok(t);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult AddTransaction(TransactionDetail t)
        {
            try
            {

                LogError("Transaction Added ");
                transaction.AddNewTransaction(t);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateTransaction(int id, TransactionDetail t)
        {
            try
            {
              LogError($"Transaction Edit #:{id},{t.ApplyId}");
               await transaction.UpdateTransaction(id, t);
               return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteTransactionDetails(int id)
        {
            try
            {


                LogError($"Transaction Delete #:{id}");
                await transaction.DeleteTransaction(id);
                return Ok();
            }
            catch(DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetTransactionByID")]
        public async Task<ActionResult> GetTransactionByID(int id)
        {
            LogError(+id + " is retrieved");
            TransactionDetail t= await transaction.GetTransactionById(id);
            return Ok(t);
        }
       
    }
}
