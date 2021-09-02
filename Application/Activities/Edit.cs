using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Activity.Id);

                //Si el usuario no ha metido el Title -es null- entonces será null... y si es null quiero que se 
                //quede con el title que ya tenía porque presuponemos que el User no quería modificarlo
                //activity.Title = request.Activity.Title ?? activity.Title;


                //El primer parámetro será el objeto fuente, desde donde empezará a mappear; el segundo será hasta donde queremos mappear
                //En resumen, mappearemos cada propiedad de request.Activity en nuestra activity que obtenemos de la BBDD
                _mapper.Map(request.Activity, activity);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}