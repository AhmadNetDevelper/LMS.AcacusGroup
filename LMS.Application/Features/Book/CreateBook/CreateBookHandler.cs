using AutoMapper;
using LMS.Application.Repositories;
using LMS.Domain.Entities;
using MediatR;

namespace LMS.Application.Features.BookFeatures.CreateBook;

public class CreateBookHandler : IRequestHandler<CreateBookRequest, CreateBookResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public CreateBookHandler(IUnitOfWork unitOfWork, IBookRepository bookRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<CreateBookResponse> Handle(CreateBookRequest request, CancellationToken cancellationToken)
    {
        var book = _mapper.Map<Book>(request);
        _bookRepository.Insert(book);
        await _unitOfWork.Save(cancellationToken);

        return _mapper.Map<CreateBookResponse>(book);
    }

}
