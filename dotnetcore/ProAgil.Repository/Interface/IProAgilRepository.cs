using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository.Interface
{
    public interface IProAgilRepository
    {
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveChangesAsync();

         //EVENTOS
        Task<Evento[]> GetAllEventoByTemaAsync(string tema, bool includePalestrante);
        Task<Evento[]> GetAllEventoAsync(bool includePalestrante);
        Task<Evento> GetEventoByIdAsync(int id, bool includePalestrante);

        //PALESTRANTE
        Task<Palestrante[]> GetAlPalestranteByNomeAsync(string nome, bool includeEvento);
        Task<Palestrante> GetPalestranteByIdAsync(int id, bool includeEvento);
    }
}