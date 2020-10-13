using Demo.Contracts;
using Demo.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.CQRS.Commands
{
    public class DeleteDepartementCommand : IRequest<SuccessResponse>
    {
        public long Id { get; set; }
    }

    public class DeleteDepartementHandler : IRequestHandler<DeleteDepartementCommand, SuccessResponse>
    {
        private readonly IDepartementRepository _departementRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteDepartementHandler(IDepartementRepository departementRepository, IEmployeeRepository employeeRepository)
        {
            _departementRepository = departementRepository;
            _employeeRepository = employeeRepository;
        }

        public Task<SuccessResponse> Handle(DeleteDepartementCommand command, CancellationToken cancellationToken)
        {
            var result = new SuccessResponse();

            var departement = _departementRepository.GetById(command.Id);

            if (departement == null)
                result.Reason = $"Departement with ID'{command.Id}' not found.";
            else
            {
                foreach (var employee in departement.Employees)
                    _employeeRepository.Delete(employee);

                _departementRepository.Delete(departement);

                result.Success = true;
            }

            return Task.Run(() => result);
        }
    }
}
