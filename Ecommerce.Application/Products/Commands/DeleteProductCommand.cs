using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Common.Persistence;
using Ecommerce.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Products.Commands
{
    public class DeleteProductCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public DeleteProductCommand(Guid id) => Id = id;
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Guid>
    {
        private readonly IRepository<Product, Guid> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        public DeleteProductCommandHandler(IRepository<Product, Guid> repository, IFileStorageService fileStorageService) =>
            (_repository, _unitOfWork, _fileStorageService) = (repository, repository.UnitOfWork, fileStorageService);

        public async Task<Guid> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _repository.GetByIdAsync(request.Id, cancellationToken);
            _ = product ?? throw new Exception("Product Not Found");

            _repository.Remove(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _fileStorageService.Remove(product.ImagePath, "Products");

            return request.Id;
        }
    }
}
