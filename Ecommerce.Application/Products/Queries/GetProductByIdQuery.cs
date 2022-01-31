using AutoMapper;
using Ecommerce.Application.Products.Models;
using Ecommerce.Application.Common.Persistence;
using Ecommerce.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ProductModel>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id) => Id = id;
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductModel>
    {
        private readonly IRepository<Product, Guid> _repository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IRepository<Product, Guid> repository, IMapper mapper) =>
            (_repository, _mapper) = (repository, mapper);

        public async Task<ProductModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product product = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<ProductModel>(product);
        }
    }
}
