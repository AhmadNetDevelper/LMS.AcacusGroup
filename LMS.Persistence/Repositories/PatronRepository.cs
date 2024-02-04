using LMS.Application.Repositories;
using LMS.Domain.Entities;
using LMS.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LMS.Persistence.Repositories
{
    public class PatronRepository : BaseRepository<Patron>, IPatronRepository
    {
        protected readonly DataContext Context;

        public PatronRepository(DataContext context) : base(context)
        {
            Context = context;
        }

        public Task<Patron> GetByUserId(long userId, CancellationToken cancellationToken)
        {
            return Context.Patrons.Where(x => x.UserId == userId).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
