using MediatR;
using Odering.Core.Entities;

namespace Ordering.Application.Queries.Customers
{
    // Customer GetCustomerByIdQuery with Customer response
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public Guid Id { get; private set; }

        public GetCustomerByIdQuery(Guid Id)
        {
            this.Id = Id;
        }

    }

    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        private readonly IMediator _mediator;

        public GetCustomerByIdHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customers = await _mediator.Send(new GetAllCustomerQuery());
            var selectedCustomer = customers.FirstOrDefault(x => x.Id == request.Id);
            return selectedCustomer;
        }
    }
}
