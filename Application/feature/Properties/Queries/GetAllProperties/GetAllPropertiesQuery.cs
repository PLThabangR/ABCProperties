using Application.Models.Responds;
using MediatR;


namespace Application.feature.Properties.Queries.GetAllProperties
{
    public record GetAllPropertiesQuery : IRequest<List<PropertyResponse>>;
}
