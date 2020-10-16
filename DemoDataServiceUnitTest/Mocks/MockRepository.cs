using Demo;
using Demo.Contracts;
using Demo.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoDataServiceUnitTest.Mocks
{
    public class MockRepository
    {
        public readonly IDepartementRepository DepartementRepository;
        public readonly IEmployeeRepository EmployeeRepository;

        public MockRepository(DemoContext context)
        {
            DepartementRepository = new DepartementRepository(context);
            EmployeeRepository = new EmployeeRepository(context);
        }
    }
}
