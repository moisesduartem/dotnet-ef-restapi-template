using AutoMapper;
using RestApi.Application.V1.Aggregates.Examples;

namespace RestApi.Infra.Profiles
{
    public class ExampleProfile : Profile
    {
        public ExampleProfile()
        {
            CreateMap<Example, ExampleDTO>();
        }
    }
}
