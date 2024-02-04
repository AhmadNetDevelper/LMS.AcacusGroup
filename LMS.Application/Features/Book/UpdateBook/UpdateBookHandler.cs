using AutoMapper;
using LMS.Application.Repositories;
using LMS.Domain.Entities;
using MediatR;

namespace LMS.Application.Features.BookFeatures.UpdateBook;
public class UpdateBookHandler : IRequestHandler<UpdateBookRequest, UpdateBookResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public UpdateBookHandler(IUnitOfWork unitOfWork, IBookRepository bookRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<UpdateBookResponse> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
    {
        var book = _mapper.Map<Book>(request);
        _bookRepository.Update(book);
        await _unitOfWork.Save(cancellationToken);

        return _mapper.Map<UpdateBookResponse>(book);
    }
}

