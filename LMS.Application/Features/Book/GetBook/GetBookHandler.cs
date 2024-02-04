using AutoMapper;
using LMS.Application.Common.PaginationRecord;
using LMS.Application.Repositories;
using MediatR;

namespace LMS.Application.Features.BookFeatures.GetBook
{
    public class GetBookHandler : IRequestHandler<GetBookRequest, PaginationRecord<BookResponse>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBookHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<PaginationRecord<BookResponse>> Handle(GetBookRequest request, CancellationToken cancellationToken)
        {
            var paginationBook = await _bookRepository.GetAll(request.pageIndex, request.pageSize, cancellationToken);
            var responseBook = _mapper.Map<List<BookResponse>>(paginationBook.DataRecord);

            return new PaginationRecord<BookResponse>
            {
                DataRecord = (IEnumerable<BookResponse>)responseBook,
                CountRecord = paginationBook.CountRecord
            };
        }
    }
}
