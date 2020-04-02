using System.Linq;
using ProAgil.Repository.Interface;
using ProAgil.Repository;
using ProAgil.Domain;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProAgil.Repository.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        protected ProAgilContext Context { get; set; }

        public ProAgilRepository(ProAgilContext context)
        {
            Context = context;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add<T>(T entity) where T : class
        {
            Context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            Context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            Context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await Context.SaveChangesAsync()) > 0;
        }



        public async Task<Evento[]> GetAllEventoByTemaAsync(string tema, bool includePalestrante)
        {
            IQueryable<Evento> query = Context.Eventos
               .Include(x => x.Lotes)
               .Include(x => x.RedeSociais);

            if (includePalestrante)
            {
                query = query
                    .Include(x => x.PalestranteEventos)
                    .ThenInclude(x => x.Palestante);
            }

            query.OrderByDescending(x => x.DataEvento)
                .Where(x=> x.Tema.Contains(tema));

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrante = false)
        {
            IQueryable<Evento> query = Context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedeSociais);

            if (includePalestrante)
            {
                query = query
                    .Include(x => x.PalestranteEventos)
                    .ThenInclude(x => x.Palestante);
            }

            query.OrderByDescending(x => x.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int id, bool includePalestrante)
        {
            IQueryable<Evento> query = Context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedeSociais);

            if (includePalestrante)
            {
                query = query
                    .Include(x => x.PalestranteEventos)
                    .ThenInclude(x => x.Palestante);
            }

            query.OrderByDescending(x => x.DataEvento)
                .Where(x => x.Id == id);

            return await query.FirstOrDefaultAsync();
        }



        public async Task<Palestrante[]> GetAlPalestranteByNomeAsync(string nome, bool includeEvento)
        {
            IQueryable<Palestrante> query = Context.Palestrantes
                .Include(x => x.RedesSociais);

            if (includeEvento)
            {
                query = query
                    .Include(x => x.PalestranteEventos)
                    .ThenInclude(x => x.Evento);
            }

            query.OrderByDescending(x => x.Id)
                .Where(x => x.Nome.Contains(nome));

            return await query.ToArrayAsync();
        }


        public async Task<Palestrante> GetPalestranteByIdAsync(int id, bool includeEvento = false)
        {
            IQueryable<Palestrante> query = Context.Palestrantes
                .Include(x => x.RedesSociais);

            if (includeEvento)
            {
                query = query
                    .Include(x => x.PalestranteEventos)
                    .ThenInclude(x => x.Evento);
            }

            query.OrderByDescending(x => x.Id)
                .Where(x => x.Id == id);

            return await query.FirstOrDefaultAsync();
        }

    }
}