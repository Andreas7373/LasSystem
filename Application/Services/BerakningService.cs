using Application.Interfaces;
using Domain.Entities;
using Domain.Services;

namespace Application.Services
{
    public class BerakningService(IPersonRepository personRepository) : IBerakningService
    {
        private readonly IPersonRepository _personRepository = personRepository;

        public async Task BeraknaAlla(Kund kund)
        {
          
            var personIds = await _personRepository.GetAllPersonsId(kund.Id);
            var count = personIds.Count;
            int pageSize = 100;
            int page = 0;
            do
            {
                var batchIds = personIds.Skip(page * pageSize).Take(pageSize).ToList();
                var personer = await _personRepository.SelectPersonerAsync(kund.Id, batchIds, true);
        
                foreach (Person p in personer)
                {
                    LASCalculator.BeraknaLAS(kund, p);
                }

                await _personRepository.BulkUpdatePersonerAsync(personer);
                page++;
            } while (page * pageSize < count);
        }

        public async Task BeraknaPerson(Kund kund,Person person)
        {
            LASCalculator.BeraknaLAS(kund, person);
        }
    }
}
