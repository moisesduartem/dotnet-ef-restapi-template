using MediatR;

namespace RestApi.Application.V1.Aggregates.Examples
{
    public class ExampleQuery : IRequest<ExampleDTO>
    {
        public string Id { get; set; }
    }
}
