using BuildingBlocks.MockData;

using Data.EF.DatabaseContext;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using Moq;

using Service.Concretes;

using Xunit;

namespace Test.Integration;

public class CustomersControllerTest : IDisposable
{
    protected readonly AppDbContext _context;

    public CustomersControllerTest()
    {
        var cnn = new DatabaseConfiguration().GetDataConnectionString();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: cnn)
            .Options
        ;

        _context = new AppDbContext(options);

        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task GetAllAsync_ReturnCustomerCollection()
    {
        //Arrange
        _context.Customers.AddRange(CusatomersMockData.GetCustomers());
        _context.SaveChanges();
        var sut = new CustomerService(_context);

        //Act
        var result = await sut.GetAllAsync();

        //Assert
        result.Should().HaveCount(CusatomersMockData.GetCustomers().Count + 1);
    }

    [Fact]
    public async Task AddAsync_AddNewCustomer()
    {
        //Arrange
        var newCustomer = CusatomersMockData.NewCustomer();
        _context.Customers.AddRange(CusatomersMockData.GetCustomers());
        _context.SaveChanges();

        var sut = new CustomerService(_context);

        //Act
        await sut.AddAsync(newCustomer, It.IsAny<CancellationToken>(), true);

        //Assert
        var expectedRecordCount = CusatomersMockData.GetCustomers().Count + 2;
        var dataCount = _context.Customers.Count();
        dataCount.Should().Be(expectedRecordCount);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
