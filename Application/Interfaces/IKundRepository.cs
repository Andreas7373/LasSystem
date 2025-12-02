using Domain.Entities;

namespace Application.Interfaces
{
    public interface IKundRepository
    {
        // CREATE
        Task<Kund> CreateAsync(Kund kund);

        // READ ALL
        Task<List<Kund>> GetAllAsync();

        // READ BY ID
        Task<Kund> GetByIdAsync(Guid id);

        // UPDATE
        Task<bool> UpdateAsync(Kund updated);

        // DELETE
        Task<bool> DeleteAsync(Guid id);
    }
}
