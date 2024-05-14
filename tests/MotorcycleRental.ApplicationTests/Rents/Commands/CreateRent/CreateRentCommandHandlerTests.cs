using Xunit;
using MotorcycleRental.Application.Rents.Commands.CreateRent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Application.Users;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Exceptions;
using MotorcycleRental.Domain.Repositories;
using MotorcycleRental.Infrastructure.Repositories;

namespace MotorcycleRental.Application.Rents.Commands.CreateRent.Tests
{
    public class CreateRentCommandHandlerTests
    {
        private readonly Mock<IRentsRepository> _mockRentsRepository = new Mock<IRentsRepository>();
        private readonly Mock<IRentPlansRepository> _mockRentPlansRepository = new Mock<IRentPlansRepository>();
        private readonly Mock<IBikersRepository> _mockBikersRepository = new Mock<IBikersRepository>();
        private readonly Mock<IMotorcyclesRepository> _mockMotorcyclesRepository = new Mock<IMotorcyclesRepository>();
        private readonly Mock<IUserContext> _mockUserContext = new Mock<IUserContext>();
        private readonly Mock<ILogger<CreateRentCommandHandler>> _mockLogger = new Mock<ILogger<CreateRentCommandHandler>>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly CreateRentCommandHandler _handler;

        public CreateRentCommandHandlerTests()
        {
            _handler = new CreateRentCommandHandler(
                _mockRentsRepository.Object,
                _mockRentPlansRepository.Object,
                _mockBikersRepository.Object,
                _mockMotorcyclesRepository.Object,
                _mockUserContext.Object,
                _mockLogger.Object,
                _mockMapper.Object
            );
        }

        [Fact]
        public async Task Handle_Success()
        {
            // Arrange
            var currentUser = new CurrentUser("1", "test@example.com", []);
            var command = new CreateRentCommand { MotorcycleId = 1, RentPlanId = 1 };
            var biker = new Biker { Id = 1, UserId = currentUser.Id };
            var motorcycle = new Motorcycle { Id = 1 };
            var rentPlan = new RentPlan { Id = 1, Days = 5 };
            var rent = new Rent { Id = 1 };

            _mockUserContext.Setup(x => x.GetCurrentUser()).Returns(currentUser);
            _mockRentPlansRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(rentPlan);
            _mockBikersRepository.Setup(x => x.GetByUserIdAsync(currentUser.Id)).ReturnsAsync(biker);
            _mockMotorcyclesRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(motorcycle);
            _mockRentsRepository.Setup(x => x.GetActiveRentByBiker(biker.Id)).ReturnsAsync(value: null);
            _mockMapper.Setup(m => m.Map<Rent>(command)).Returns(rent);
            _mockRentsRepository.Setup(x => x.Create(rent)).ReturnsAsync(rent.Id);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Xunit.Assert.Equal(rent.Id, result);
        }

        [Fact]
        public async Task Handle_ThrowsNotFoundException_WhenRentPlanNotFound()
        {
            // Arrange
            var currentUser = new CurrentUser("1", "test@example.com", []);
            var command = new CreateRentCommand { MotorcycleId = 1, RentPlanId = 1 };

            _mockUserContext.Setup(x => x.GetCurrentUser()).Returns(currentUser);
            _mockRentPlansRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(value: null);

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ThrowsNotFoundException_WhenBikerNotFound()
        {
            // Arrange
            var currentUser = new CurrentUser("1", "test@example.com", []);
            var command = new CreateRentCommand { MotorcycleId = 1, RentPlanId = 1 };
            var rentPlan = new RentPlan { Id = 1 };

            _mockUserContext.Setup(x => x.GetCurrentUser()).Returns(currentUser);
            _mockRentPlansRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(rentPlan);
            _mockBikersRepository.Setup(x => x.GetByUserIdAsync(currentUser.Id)).ReturnsAsync(value: null);  // Biker not found

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ThrowsNotFoundException_WhenMotorcycleNotFound()
        {
            // Arrange
            var currentUser = new CurrentUser("1", "test@example.com", []);
            var command = new CreateRentCommand { MotorcycleId = 1, RentPlanId = 1 };
            var rentPlan = new RentPlan { Id = 1 };
            var biker = new Biker { Id = 1, UserId = currentUser.Id };

            _mockUserContext.Setup(x => x.GetCurrentUser()).Returns(currentUser);
            _mockRentPlansRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(rentPlan);
            _mockBikersRepository.Setup(x => x.GetByUserIdAsync(currentUser.Id)).ReturnsAsync(biker);
            _mockMotorcyclesRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(value: null);  // Motorcycle not found

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ThrowsBadRequestException_WhenActiveRentExists()
        {
            // Arrange
            var currentUser = new CurrentUser("1", "test@example.com", []);
            var command = new CreateRentCommand { MotorcycleId = 1, RentPlanId = 1 };
            var rentPlan = new RentPlan { Id = 1 };
            var biker = new Biker { Id = 1, UserId = currentUser.Id };
            var motorcycle = new Motorcycle { Id = 1 };
            var activeRent = new Rent { Id = 99 };  // Simulate existing active rent

            _mockUserContext.Setup(x => x.GetCurrentUser()).Returns(currentUser);
            _mockRentPlansRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(rentPlan);
            _mockBikersRepository.Setup(x => x.GetByUserIdAsync(currentUser.Id)).ReturnsAsync(biker);
            _mockMotorcyclesRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(motorcycle);
            _mockRentsRepository.Setup(x => x.GetActiveRentByBiker(biker.Id)).ReturnsAsync(activeRent);

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}