
using Demo;
using Demo.CQRS.Commands.Departements;
using Demo.CQRS.Queries;
using DemoService.CQRS.Commands.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoDataServiceUnitTest.Mocks
{
    public class MockHandler
    {
        private readonly MockRepository _repository;
        public readonly CreateDepartementHandler CreateDepartementHandler;
        public readonly GetDepartementByIdHandler GetDepartementByIdHandler;

        public readonly CreateEmployeeHandler CreateEmployeeHandler;

        public MockHandler(DemoContext context)
        {
            _repository = new MockRepository(context);

            CreateDepartementHandler = new CreateDepartementHandler(_repository.DepartementRepository);
            GetDepartementByIdHandler = new GetDepartementByIdHandler(_repository.DepartementRepository);

            CreateEmployeeHandler = new CreateEmployeeHandler(_repository.EmployeeRepository);
        }
    }
}
