using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vb.Data.Entity;
using Vb.Data;
using Microsoft.EntityFrameworkCore;

namespace VbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly VbDbContext dbContext;

        public ContactController(VbDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<Contact>> Get()
        {
            return await dbContext.Set<Contact>()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Contact> Get(int id)
        {
            var contact = await dbContext.Set<Contact>()
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            return contact;
        }

        [HttpPost]
        public async Task Post([FromBody] Contact contact)
        {
            await dbContext.Set<Contact>().AddAsync(contact);
            await dbContext.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Contact contact)
        {
            var fromdb = await dbContext.Set<Contact>().Where(x => x.Id == id).FirstOrDefaultAsync();
            fromdb.ContactType = contact.ContactType;
            fromdb.Information = contact.Information;

            await dbContext.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var fromdb = await dbContext.Set<Contact>().Where(x => x.Id == id).FirstOrDefaultAsync();
            fromdb.IsActive = false;
            await dbContext.SaveChangesAsync();
        }

    }
}
