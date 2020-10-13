using DemoGateway.ViewModel;
using DemoGateway.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoGateway.Data
{
    public interface IDepartementService
    {
        SuccessResponse AddDepartement(CreateDepartementVM newDepartement);

        List<DepartementVM> GetDepartements(int page, int itemsPerPage);

        DepartementDetailVM GetDepartementById(long id);

        SuccessResponse DeleteDepartement(long id);
    }
}
