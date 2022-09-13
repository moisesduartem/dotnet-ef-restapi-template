using AutoMapper;
using MediatR;

namespace RestApi.Application.V1.Aggregates.Examples
{
    public class ExampleQueryHandler : IRequestHandler<ExampleQuery, ExampleDTO>
    {
        private readonly IMapper _mapper;

        public ExampleQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<ExampleDTO> Handle(ExampleQuery request, CancellationToken cancellationToken)
        {
            var entity = new Example
            {
                Id = Guid.NewGuid(),
                Name = "Paul"
            };

            var dto = _mapper.Map<ExampleDTO>(entity);

            return Task.FromResult(dto);
        }
    }
}
