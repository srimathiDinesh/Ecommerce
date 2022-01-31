using AutoMapper;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Common.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Products.Commands
{
    public class CreateProductCommand : IRequest<Guid>
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Image")]
        public IFormFile Image { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IRepository<Product, Guid> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;

        public CreateProductCommandHandler(IRepository<Product, Guid> repository, IMapper mapper, IFileStorageService fileStorageService) => 
            (_repository, _unitOfWork, _mapper, _fileStorageService) = (repository, repository.UnitOfWork, mapper, fileStorageService);

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            string productImagePath = await _fileStorageService.UploadAsync(request.Image, "Products", cancellationToken);

            Product product = _mapper.Map<Product>(request);
            product.ImagePath = productImagePath;

            await _repository.AddAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
