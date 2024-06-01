using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
using Application.Features.Address.Commands;
using Application.Features.Address.Constans;
using Application.Features.Address.Validatators;
using Application.Features.User.Constants;
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
    public class CreateAddressCommand : IRequest
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

        public class Handler : IRequestHandler<CreateAddressCommand>
        {
            private readonly IAddressRepository _addressRepository;
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository, IAddressRepository addressRepository)
            {
                _userRepository = userRepository;
                _addressRepository = addressRepository;
            }

            public async Task Handle(CreateAddressCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

                if (user.Addresses.Count <= 3)
                {
                    throw new BusinessException(AddressConstants.AddressLimitError);
                }

                var validator = new CreateAddressCommandValidator();
                var validationResult = validator.Validate(request);

                if (validationResult.IsValid == false)
                {
                    throw new ValidationException(AddressConstants.AddressAddError, validationResult.ToDictionary());
                }

                var address = new AddressAggregate
                {
                    AddressTitle = request.AddressTitle,
                    Address = request.Address,
                    User = user
                };


                await _addressRepository.AddAsync(address,cancellationToken);
            }
        }
    }
}
