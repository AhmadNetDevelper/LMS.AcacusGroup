using AutoMapper;
using LMS.Application.Common.PaginationRecord;
using LMS.Application.Repositories;
using MediatR;

namespace LMS.Application.Features.Library.Query.GetBooksNotAdded
{
    public class GetAddedBookHandler : IRequestHandler<LibraryBookAddedRequest, PaginationRecord<LibraryBookAddedResponse>>
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IMapper _mapper;

        public GetAddedBookHandler(ILibraryRepository libraryRepository, IMapper mapper)
        {
            _libraryRepository = libraryRepository;
            _mapper = mapper;
        }

        public async Task<PaginationRecord<LibraryBookAddedResponse>> Handle(LibraryBookAddedRequest request, CancellationToken cancellationToken)
        {
            var paginationBook = await _libraryRepository.GetLibraryBooks(request.pageIndex, request.pageSize, cancellationToken);
            var responseBook = _mapper.Map<List<LibraryBookAddedResponse>>(paginationBook.DataRecord);

            return new PaginationRecord<LibraryBookAddedResponse>
            {
                DataRecord = responseBook,
                CountRecord = paginationBook.CountRecord
            };
        }
    }
}
