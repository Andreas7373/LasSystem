using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBerakningService
    {
        Task BeraknaAlla(Kund kund);
        Task BeraknaPerson(Kund kund, Person person);
    }
}
