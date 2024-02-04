using AutoMapper;
using LMS.Application.Repositories;
using MediatR;

namespace LMS.Application.Features.Library.Command.RemoveBookFromLibrary
{
    public class RemoveBookFromLibraryHandler : IRequestHandler<RemoveBookFromLibraryRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILibraryRepository _libraryRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public RemoveBookFromLibraryHandler(ILibraryRepository libraryRepository, IBookRepository bookRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _libraryRepository = libraryRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(RemoveBookFromLibraryRequest request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.Get(request.bookId, cancellationToken);

            if (book != null)
            {
                book.LibraryId = null;
                book.IsAvailable = false;

                _bookRepository.Update(book);
                await _unitOfWork.Save(cancellationToken);
            }

            return await Task.FromResult(true);
        }

    }
}
