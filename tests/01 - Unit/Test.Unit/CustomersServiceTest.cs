using System.Net;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Presentation.Server.Controllers;

using Service.Interfaces;

using Xunit;

using BuildingBlocks.MockData;

namespace Test.Unit
{
    public class CustomersServiceTest
    {
        [Fact]
        public async Task CreateCustomerValid_ReturnsSuccessAsync()
        {
            //Arrange
            var customerService = new Mock<ICustomerService>();
            var newCustomer = CusatomersMockData.NewCustomer();
            var sut = new CustomersContoller(customerService.Object);

            //Act
            var actionResult = await sut.AddAsync(newCustomer, It.IsAny<CancellationToken>());

            //Assert
            actionResult.Should().BeOfType<CreatedResult>();
            var result = actionResult as CreatedResult;
            result.StatusCode.Should().Be((int)HttpStatusCode.Created);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            //Arrange
            var customerService = new Mock<ICustomerService>();
            customerService.Setup(users => users.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(CusatomersMockData.GetCustomers()
            );
            var sut = new CustomersContoller(customerService.Object);

            //Act
            var actionResult = await sut.GetAllAsync(It.IsAny<CancellationToken>());

            //Assert
            actionResult.Result.Should().BeOfType<OkObjectResult>();
            var result = actionResult.Result as OkObjectResult;
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturn204NoContentStatus()
        {
            //Arrange
            var customerService = new Mock<ICustomerService>();
            customerService.Setup(users =>
                users.GetAllAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(CusatomersMockData.GetEmptyCustomers()
            );
            var sut = new CustomersContoller(customerService.Object);

            //Act
            var actionResult = await sut.GetAllAsync(It.IsAny<CancellationToken>());

            //Assert
            actionResult.Result.Should().BeOfType<NoContentResult>();
            var result = actionResult.Result as NoContentResult;
            result.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            customerService.Verify(_ => _.GetAllAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task AddAsync_ShouldCall_ICustomerService_AddAsync_AtleastOnce()
        {
            //Arrange
            var customerService = new Mock<ICustomerService>();
            var newCustomer = CusatomersMockData.NewCustomer();
            var sut = new CustomersContoller(customerService.Object);

            //Act
            var result = await sut.AddAsync(newCustomer, It.IsAny<CancellationToken>());

            //Assert
            customerService.Verify(_ => _.AddAsync(newCustomer, It.IsAny<CancellationToken>(), true));
        }
    }
}
