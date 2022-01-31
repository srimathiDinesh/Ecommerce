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
    public class UpdateProductCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public IFormFile Image { get; set; }

        public string ExistingImage { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
    {
        private readonly IRepository<Product, Guid> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IRepository<Product, Guid> repository, IFileStorageService fileStorageService, IMapper mapper) =>
            (_repository, _unitOfWork, _fileStorageService, _mapper) = (repository, repository.UnitOfWork, fileStorageService, mapper);

        public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _repository.GetByIdAsync(request.Id, cancellationToken);
            _ = product ?? throw new Exception("Product Not Found");

            string productImagePath = request.Image is not null
                ? await _fileStorageService.UploadAsync(request.Image, "Products", cancellationToken)
                : product.ImagePath;

            _mapper.Map(request, product);
            product.ImagePath = productImagePath;

            _repository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (request.Image is not null)
                _fileStorageService.Remove(request.ExistingImage, "Products");

            return request.Id;
        }
    }
}
