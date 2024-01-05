using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vb.Data.Entity;
using Vb.Data;
using Microsoft.EntityFrameworkCore;

namespace VbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTransactionController : ControllerBase
    {
        private readonly VbDbContext dbContext;

        public AccountTransactionController(VbDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<AccountTransaction>> Get()
        {
            return await dbContext.Set<AccountTransaction>()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<AccountTransaction> Get(int id)
        {
            var accountTransaction = await dbContext.Set<AccountTransaction>()
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            return accountTransaction;
        }

        [HttpPost]
        public async Task Post([FromBody] AccountTransaction accountTransaction)
        {
            await dbContext.Set<AccountTransaction>().AddAsync(accountTransaction);
            await dbContext.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] AccountTransaction accountTransaction)
        {
            var fromdb = await dbContext.Set<AccountTransaction>().Where(x => x.Id == id).FirstOrDefaultAsync();
            fromdb.Amount = accountTransaction.Amount;
            fromdb.Description = accountTransaction.Description;
            fromdb.TransactionDate = accountTransaction.TransactionDate;
            fromdb.TransferType = accountTransaction.TransferType;
            fromdb.ReferenceNumber = accountTransaction.ReferenceNumber;

            await dbContext.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var fromdb = await dbContext.Set<AccountTransaction>().Where(x => x.Id == id).FirstOrDefaultAsync();
            fromdb.IsActive = false;
            await dbContext.SaveChangesAsync();
        }

    }
}
