using Application.Models.Responds;
using MediatR;
using System;


namespace Application.feature.Properties.Queries.GetById
{
    public record GetPropertyByIdQuery(int id) : IRequest<PropertyResponse>;
    
    
}
