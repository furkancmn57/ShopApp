using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.Address.Commands.Validatators;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Address.Commands
{
    public class CreateAddressCommand : IRequest<AddressAggregate>
    {
        public CreateAddressCommand(int userId, string addressTitle, string address)
        {
            UserId = userId;
            AddressTitle = addressTitle;
            Address = address;
        }

        public int UserId { get; set; }
        public string AddressTitle { get; set; }
        public string Address { get; set; }

        public class Handler : IRequestHandler<CreateAddressCommand, AddressAggregate>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task<AddressAggregate> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
            {
                var user =  await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

                if (user is null)
                {
                    throw new NotFoundExcepiton("User Bulunamadı.");
                }

                var validator = new CreateAddressCommandValidator();
                var validationResult = validator.Validate(request);

                if (validationResult.IsValid == false)
                {
                    throw new ValidationException("Adres eklerken hata oluştu.", validationResult.ToDictionary());
                }

                var address = AddressAggregate.Create(request.AddressTitle, request.Address, user);

                await _context.Addresses.AddAsync(address, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return address;
            }
        }
    }
}
