using Application.Interfaces;
using Domain.Entities;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PersonRepository(LasSystemDbContext context) : IPersonRepository
    {
        private readonly LasSystemDbContext _context = context;

        public async Task<Person?> GetByPersonIdAsync(Guid kundId, Guid personId)
        {
            return await _context.Personer
                .AsNoTracking()
                .Include(p => p.Anstallningar)
                .Include(p => p.WinLasData)
                .FirstOrDefaultAsync(p => p.KundId == kundId && p.Id == personId);
        }

        public async Task<Person?> GetByPersonnummerAsync(Guid kundId, string personnummer)
        {
           return  await _context.Personer
                .AsNoTracking()
                .Include(p => p.Anstallningar)
                .Include(p => p.WinLasData)
                .FirstOrDefaultAsync(p => p.KundId == kundId && p.Personnummer == personnummer);

  
        }
        public async Task<List<Guid>> GetAllPersonsId(Guid kundId)
        {
            return await _context.Personer
                .Where(p => p.KundId == kundId)
                .Select(p => p.Id)
                .ToListAsync();
        }

        public async Task<bool> BulkInsertPersonerAsync(List<Person> personer, bool takeRelated = false)
        {
            try
            {

                var bulkConfig = new BulkConfig
                {
                    IncludeGraph = takeRelated 
                };

                await _context.BulkInsertAsync(personer, bulkConfig);

                return true; 
            }
            catch (Exception ex)
            {

                return false; 
            }
        }

        public async Task<bool> BulkUpdatePersonerAsync(List<Person> personer, bool takeRelated = false)
        {
            try
            {
                var bulkConfig = new BulkConfig
                {
                    IncludeGraph = takeRelated
                };

                await _context.BulkUpdateAsync(personer, bulkConfig);
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public async Task<List<Person>> SelectPersonerWithWinLasDataAsync(Guid kundId, List<Guid> personIds)
        {
            IQueryable<Person> query = _context.Personer
            .Where(p => personIds.Contains(p.Id) && p.KundId == kundId);
            query = query.Include(p => p.WinLasData);

            return await query.ToListAsync();

        }
        public async Task<List<Person>> SelectPersonerAsync(Guid kundId, List<Guid> personIds, bool takeRelated = false)
        {
            IQueryable<Person> query = _context.Personer
            .Where(p => personIds.Contains(p.Id) && p.KundId == kundId);

  
                query = query.Include(p => p.Anstallningar);

            return await query.ToListAsync();
            //var personer = personIds.Select(id => new Person { Id = id, KundId = kundId }).ToList();
            //var bulkConfig = new BulkConfig
            //{
            //    IncludeGraph = takeRelated
            //};


            //await _context.BulkReadAsync(personer, bulkConfig);

            //return personer;
        }

        public async Task<bool> BulkDeletePersonerAsync(Guid kundId, List<Guid> personIds)
        {
            try
            {
                var personer = personIds.Select(id => new Person { Id = id, KundId = kundId }).ToList();

                await _context.BulkDeleteAsync(personer, new BulkConfig
                {
                    PreserveInsertOrder = false,
                    SetOutputIdentity = false
                });
                return true;
            }
            catch { return false; }
        }
    }
}

