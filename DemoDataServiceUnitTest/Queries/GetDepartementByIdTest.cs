using Demo;
using DemoDataServiceUnitTest.Mocks;
using DemoDataServiceUnitTest.Commands;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Demo.CQRS.Queries;
using Xunit;
using Demo.ViewModels;
using Grpc.Core;

namespace DemoDataServiceUnitTest.Queries
{
    public class GetDepartementByIdTest
    {
        private readonly DemoContext _context;
        private readonly Mock<IMediator> _mediator;
        private readonly CreateDepartementCommandTest _createDepartementCommand;

        public GetDepartementByIdTest(DemoContext context = null)
        {
            _context = context != null ? context : new MockDbContext().Create();
            _mediator = new MockMediator(_context).Init();

            _createDepartementCommand = new CreateDepartementCommandTest(_context);
        }

        [Fact]
        public async Task Success_GetDepartementByIdQuery()
        {
            await _createDepartementCommand.Success_CreateDepartement();

            var query = new GetDepartementByIdQuery { Id = 1 };
            var result = await _mediator.Object.Send(query, default);

            Assert.IsType<DepartementDetailVM>(result);
            Assert.Equal("Test success name", result.Name);
        }

        [Fact]
        public async Task Failed_GetDepartementByIdQuery()
        {
            // Arrange
            var query = new GetDepartementByIdQuery { Id = 999 };

            //
            await Assert.ThrowsAsync<RpcException>(() => _mediator.Object.Send(query, default));
        }
    }
}
