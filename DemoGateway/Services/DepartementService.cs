using DemoGateway.Contracts;
using DemoGateway.Data;
using DemoGateway.ViewModel;
using DemoGateway.ViewModels;
using ServiceProto.Departement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoGateway.Services
{
    public class DepartementService:IDepartementService
    {
        public readonly ProtoDepartement.ProtoDepartementClient _departement;
        public DepartementService(IGrpcClient client)
        {
            _departement = new ProtoDepartement.ProtoDepartementClient(client.DepartementChannel);
        }

        public SuccessResponse AddDepartement(CreateDepartementVM newDepartement)
        {
            var result =_departement.AddDepartement(new AddDepartementMessage { Name = newDepartement.Name, Location = newDepartement.Location });
            return new SuccessResponse
            {
                Success = result.Success,
                Reason = result.Reason
            };
        }

        public SuccessResponse DeleteDepartement(long id)
        {
            throw new NotImplementedException();
        }

        public DepartementDetailVM GetDepartementById(long id)
        {
            throw new NotImplementedException();
        }

        public List<DepartementVM> GetDepartements(int page, int itemsPerPage)
        {
            throw new NotImplementedException();
        }
    }
}
