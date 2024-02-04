using LMS.Application.Repositories;
using LMS.Domain.Entities;
using LMS.Persistence.Context;

namespace LMS.Persistence.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(DataContext context) : base(context)
        {
        }
    }
}
