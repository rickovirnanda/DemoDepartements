using Demo.Contracts;
using Demo.Models;
using Demo.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.CQRS.Commands.Departements
{
    public class CreateDepartementCommand:IRequest<SuccessResponse>
    {
        public CreateDepartemenVM Payload { get; set; }
    }

    public class CreateDepartementHandler : IRequestHandler<CreateDepartementCommand, SuccessResponse>
    {
        private readonly IDepartementRepository _repository;

        public CreateDepartementHandler(IDepartementRepository departementRepoRepository) =>
            _repository = departementRepoRepository;

        public Task<SuccessResponse> Handle(CreateDepartementCommand command, CancellationToken cancellationToken)
        {
            var result = new SuccessResponse();

            var departements = _repository.GetEntities(x => x.Name.ToLower().Contains(command.Payload.Name.ToLower()));

            if (departements.Count() != 0)
                result.Reason = $"Departement '{command.Payload.Name}' already existed.";
            else
            {
                var departement = new Departement
                {
                    Name = command.Payload.Name,
                    Location = command.Payload.Location
                };

                _repository.Create(departement);
                result.Success = true;
            }

            return Task.Run(()=>result);
        }
    }
}
