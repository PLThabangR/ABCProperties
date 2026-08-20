using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Properties.Command.DeleteProperty
{
    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, bool>
    {
        private readonly IPropertyService? propertyService;

        public DeletePropertyCommandHandler(IPropertyService? propertyService)
        {
            this.propertyService = propertyService;
        }

        public async Task<bool> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            //check if the exists
            var propertyInDb =await  propertyService.DeleteAsync(request.Id);

            if (propertyInDb >0)
            {
                return true;
            }

            return false;
            
        }
    }
}
