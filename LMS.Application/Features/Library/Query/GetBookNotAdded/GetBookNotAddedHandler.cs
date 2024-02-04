using AutoMapper;
using LMS.Application.Common.PaginationRecord;
using LMS.Application.Features.Library.Query.GetBooksNotAdded;
using LMS.Application.Repositories;
using MediatR;

namespace LMS.Application.Features.Library.Query.GetBookNotAdded
{
    public class GetBookNotAddedHandler : IRequestHandler<BookNotAddedRequest, PaginationRecord<LibraryBookAddedResponse>>
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IMapper _mapper;

        public GetBookNotAddedHandler(ILibraryRepository libraryRepository, IMapper mapper)
        {
            _libraryRepository = libraryRepository;
            _mapper = mapper;
        }

        public async Task<PaginationRecord<LibraryBookAddedResponse>> Handle(BookNotAddedRequest request, CancellationToken cancellationToken)
        {
            var paginationBook = await _libraryRepository.GetLibraryBooksNotAdded(request.pageIndex, request.pageSize, cancellationToken);
            var responseBook = _mapper.Map<List<LibraryBookAddedResponse>>(paginationBook.DataRecord);

            return new PaginationRecord<LibraryBookAddedResponse>
            {
                DataRecord = responseBook,
                CountRecord = paginationBook.CountRecord
            };
        }
    }
}
