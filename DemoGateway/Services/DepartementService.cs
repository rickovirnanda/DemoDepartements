using DemoGateway.Contracts;
using DemoGateway.Data;
using DemoGateway.ViewModel;
using DemoGateway.ViewModels;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
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
            _departement = new ProtoDepartement.ProtoDepartementClient(client.DemoChannel);
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
            var result = _departement.DeleteDepartement(new GetByIdRequest { Id = id });

            return new SuccessResponse
            {
                Success = result.Success,
                Reason = result.Reason
            };
        }

        public DepartementDetailVM GetDepartementById(long id)
        {
            try
            {
                var result = _departement.GetDepartementById(new GetByIdRequest { Id = id });

                var departement = new DepartementDetailVM
                {
                    Id = (int)result.Id,
                    Name = result.Name,
                    Location = result.Location
                };

                departement.Employees.AddRange(
                    result.Employees.Select(
                        x => new EmployeeVM
                        {
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            JoinDate = x.JoinDate.ToDateTime()
                        }));

                return departement;
            }
            catch(RpcException ex)
            {
                throw ex;
            }
        }

        public List<DepartementVM> GetDepartements(int page, int itemsPerPage)
        {
            var result = _departement.GetDepartement(new GetDepartementMessage { Page = page, ItemsPerPage = itemsPerPage });

            var departements = result.Departements.Select(x => new DepartementVM
            {
                Id = (int)x.Id,
                Name = x.Name,
                Location = x.Location
            }).ToList();

            return departements;
        }

        public SuccessResponse UpdateDepartement(DepartementVM newDepartement)
        {
            throw new NotImplementedException();
        }
    }
}
