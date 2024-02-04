using LMS.Application.Common.PaginationRecord;
using LMS.Application.Repositories;
using LMS.Domain.Entities;
using LMS.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LMS.Persistence.Repositories
{
    public class LibraryRepository : BaseRepository<Library>, ILibraryRepository
    {
        protected readonly DataContext Context;
        public LibraryRepository(DataContext context) : base(context)
        {
            Context = context;
        }

        public async Task<PaginationRecord<Book>> GetLibraryBooks(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var libraryBooks = await Context.Books.Where(x => x.LibraryId != null && 
                                                         x.IsAvailable == true)
                                                .Skip((pageIndex - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToListAsync(cancellationToken);

            return new PaginationRecord<Book>
            {
                DataRecord = libraryBooks,
                CountRecord = await Context.Books.Where(x => x.LibraryId != null).CountAsync(cancellationToken),
            };
        }

        public async Task<PaginationRecord<Book>> GetLibraryBooksNotAdded(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var libraryBooks = await Context.Books.Where(x => x.LibraryId == null &&
                                                         x.IsAvailable == true)
                                                .Skip((pageIndex - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToListAsync(cancellationToken);

            return new PaginationRecord<Book>
            {
                DataRecord = libraryBooks,
                CountRecord = await Context.Books.Where(x => x.LibraryId == null).CountAsync(cancellationToken),
            };
        }
    }
}
