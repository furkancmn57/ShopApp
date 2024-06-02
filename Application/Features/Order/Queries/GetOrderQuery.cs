using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
using Application.Common.Pagination;
using Application.Features.Order.Models;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Queries
{
    public class GetOrderQuery : IRequest<Pagination<GetOrdersResponse>>
    {
        public GetOrderQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public class Handler : IRequestHandler<GetOrderQuery, Pagination<GetOrdersResponse>>
        {
            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<Pagination<GetOrdersResponse>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
            {
                var orders = await _orderRepository.GetAsync(request.Page, request.PageSize, cancellationToken);

                return orders;
            }
        }
    }
}
