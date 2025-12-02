using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPersonRepository
    {

        Task<Person?> GetByPersonIdAsync(Guid kundId, Guid personId);
        Task<Person?> GetByPersonnummerAsync(Guid kundId, string personnummer);
        Task<List<Guid>> GetAllPersonsId(Guid kundId);
        Task<bool> BulkInsertPersonerAsync(List<Person> personer, bool takeRelated = false);
        Task<bool> BulkUpdatePersonerAsync(List<Person> personer, bool takeRelated = false);
        Task<List<Person>> SelectPersonerAsync(Guid kundId, List<Guid> personIds, bool takeRelated = false);
        Task<List<Person>> SelectPersonerWithWinLasDataAsync(Guid kundId, List<Guid> personIds);
        Task<bool> BulkDeletePersonerAsync(Guid kundId, List<Guid> personIds);

    }
}
