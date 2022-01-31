using AutoMapper;
using Ecommerce.Application.Common.Persistence;
using Ecommerce.Application.Products.Models;
using Ecommerce.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Products.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductModel>>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductModel>>
    {
        private readonly IRepository<Product, Guid> _repository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IRepository<Product, Guid> repository, IMapper mapper) =>
            (_repository, _mapper) = (repository, mapper);

        public async Task<List<ProductModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<ProductModel>>(products);
        }
    }
}
