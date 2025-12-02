using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class KundRepository : IKundRepository
    {
        private readonly LasSystemDbContext _context;

        public KundRepository(LasSystemDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task<Kund> CreateAsync(Kund kund)
        {
            kund.Id = Guid.NewGuid();
            await _context.Kunder.AddAsync(kund);
            await _context.SaveChangesAsync();
            return kund;
        }

        // READ ALL
        public async Task<List<Kund>> GetAllAsync()
        {
            return await _context.Kunder.ToListAsync();
        }

        // READ BY ID
        public async Task<Kund> GetByIdAsync(Guid id)
        {
            return await _context.Kunder.FirstOrDefaultAsync(k => k.Id == id);
        }

        // UPDATE
        public async Task<bool> UpdateAsync(Kund updated)
        {
            var existing = await _context.Kunder.FindAsync(updated.Id);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(updated);
            await _context.SaveChangesAsync();
            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(Guid id)
        {
            var kund = await _context.Kunder.FindAsync(id);
            if (kund == null) return false;

            _context.Kunder.Remove(kund);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
