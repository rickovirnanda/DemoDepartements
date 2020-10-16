using Demo;
using Demo.CQRS.Commands.Departements;
using Demo.Models;
using Demo.ViewModels;
using DemoDataServiceUnitTest.Mocks;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DemoDataServiceUnitTest.Commands
{
    public class CreateDepartementCommandTest
    {
        private readonly DemoContext _context;
        private readonly Mock<IMediator> _mediator;
        public CreateDepartementCommandTest(DemoContext context=null)
        {
            _context = context != null ? context: new MockDbContext().Create();
            _mediator = new MockMediator(_context).Init();
        }

        [Fact]
        public async Task Success_CreateDepartement()
        {
            var command = new CreateDepartementCommand
            {
                Payload = new CreateDepartemenVM { 
                    Name="Test success name",
                    Location="Test Success Location"
                }
            };

            var result = await _mediator.Object.Send(command, default);
            Assert.IsType<SuccessResponse>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Failed_CreateDepartement()
        {
            await Success_CreateDepartement();

            var command = new CreateDepartementCommand
            {
                Payload = new CreateDepartemenVM
                {
                    Name = "Test success name",
                    Location = "Test Success Location"
                }
            };

            var result = await _mediator.Object.Send(command, default);
            Assert.IsType<SuccessResponse>(result);
            Assert.False(result.Success);
        }
    }
}
