using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // var newActivity = new Activity
                // {
                //     Id = Guid.NewGuid(),
                //     Title = request.Activity.Title,
                //     Date = request.Activity.Date,
                //     Description = request.Activity.Description,
                //     Category = request.Activity.Category,
                //     City = request.Activity.City,
                //     Venue = request.Activity.Venue
                // };

                _context.Activities.Add(request.Activity);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}