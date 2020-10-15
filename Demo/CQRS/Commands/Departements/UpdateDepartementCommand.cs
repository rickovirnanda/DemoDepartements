using Demo.Contracts;
using Demo.Models;
using Demo.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoService.CQRS.Commands.Departements
{
    public class UpdateDepartementCommand:IRequest<SuccessResponse>
    {
        public DepartementVM Payload { get; set; }
    }

    public class UpdateDepartementHandler : IRequestHandler<UpdateDepartementCommand, SuccessResponse>
    {
        private readonly IDepartementRepository _repository;
        public UpdateDepartementHandler(IDepartementRepository departementRepoRepository) =>
            _repository = departementRepoRepository;
        public Task<SuccessResponse> Handle(UpdateDepartementCommand command, CancellationToken cancellationToken)
        {
            var result = new SuccessResponse();

            var departement = _repository.GetById(command.Payload.Id);

            if (departement == null)
                result.Reason = $"Departement with ID'{command.Payload.Id}' not found.";
            else
            {
                departement.Name = command.Payload.Name;
                departement.Location = command.Payload.Location;

                _repository.Update(departement);
                result.Success = true;
            }

            return Task.Run(() => result);
        }
    }
}
