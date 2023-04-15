﻿using MediatR;
using Odering.Core.Repositories.Command;
using Odering.Core.Repositories.Query;

namespace Ordering.Application.Commands.Customers.Delete
{
    // Customer create command with string response
    public class DeleteCustomerCommand : IRequest<String>
    {
        public Guid Id { get; private set; }

        public DeleteCustomerCommand(Guid Id)
        {
            this.Id = Id;
        }
    }

    // Customer delete command handler with string response as output
    public class DeleteCustomerCommmandHandler : IRequestHandler<DeleteCustomerCommand, String>
    {
        private readonly ICustomerCommandRepository _customerCommandRepository;
        private readonly ICustomerQueryRepository _customerQueryRepository;
        public DeleteCustomerCommmandHandler(ICustomerCommandRepository customerRepository, ICustomerQueryRepository customerQueryRepository)
        {
            _customerCommandRepository = customerRepository;
            _customerQueryRepository = customerQueryRepository;
        }

        public async Task<string> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customerEntity = await _customerQueryRepository.GetByIdAsync(request.Id, cancellationToken);

                await _customerCommandRepository.DeleteAsync(customerEntity, cancellationToken);
            }
            catch (Exception exp)
            {
                throw (new ApplicationException(exp.Message));
            }

            return "Customer information has been deleted!";
        }
    }
}
