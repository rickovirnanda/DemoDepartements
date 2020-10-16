using Demo;
using Demo.CQRS.Commands.Departements;
using Demo.CQRS.Queries;
using DemoService.CQRS.Commands.Employees;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DemoDataServiceUnitTest.Mocks
{
    public class MockMediator
    {
        private DemoContext _context;
        private MockHandler _handler;
        public MockMediator(DemoContext context)
        {
            _context = context;
            _handler = new MockHandler(_context);
        }

        public Mock<IMediator> Init()
        {
            // init mediator
            var mediator = new Mock<IMediator>();

            #region command
            mediator.Setup(m => m.Send(It.IsAny<CreateDepartementCommand>(), It.IsAny<CancellationToken>()))
                .Returns<CreateDepartementCommand, CancellationToken>((x, y) => _handler.CreateDepartementHandler.Handle(x,y));

            mediator.Setup(m => m.Send(It.IsAny<CreateEmployeeCommand>(), It.IsAny<CancellationToken>()))
                .Returns<CreateEmployeeCommand, CancellationToken>((x, y) => _handler.CreateEmployeeHandler.Handle(x, y));
            #endregion

            #region query
            mediator.Setup(m => m.Send(It.IsAny<GetDepartementByIdQuery>(), It.IsAny<CancellationToken>()))
                .Returns<GetDepartementByIdQuery, CancellationToken>((x, y) => _handler.GetDepartementByIdHandler.Handle(x,y));
            #endregion

            return mediator;
        }
    }
}
