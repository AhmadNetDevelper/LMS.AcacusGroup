using AutoMapper;
using LMS.Application.Repositories;
using MediatR;

namespace LMS.Application.Features.Library.Command.AddBookToLibrary
{
    public class AddBookToLibraryHandler : IRequestHandler<AddBookNotAddedRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILibraryRepository _libraryRepository;
        private readonly IBookRepository _bookRepository;

        private readonly IMapper _mapper;

        public AddBookToLibraryHandler(ILibraryRepository libraryRepository, IBookRepository bookRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _libraryRepository = libraryRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddBookNotAddedRequest request, CancellationToken cancellationToken)
        {
            var library = await  _libraryRepository.Get(1, cancellationToken);//get default library
            var book = await _bookRepository.Get(request.bookId, cancellationToken);

            if (book != null && library != null)
            {
                book.LibraryId = library.Id;
                book.IsAvailable = true;

                _bookRepository.Update(book);
                await _unitOfWork.Save(cancellationToken);
            }

            return await Task.FromResult(true);
        }
    }
}

