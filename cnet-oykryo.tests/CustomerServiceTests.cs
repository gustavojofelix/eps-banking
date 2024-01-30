using cnet_oykryo.domain.Entities;
using cnet_oykryo.Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cnet_oykryo.tests
{
    public class CustomerServiceTests
    {
        //[Fact]
        //public void GetCustomerByName_ValidName_ReturnsCustomer()
        //{
        //    // Arrange
        //    var customerRepositoryMock = new Mock<ICustomerRepository>();
        //    customerRepositoryMock.Setup(repo => repo.GetCustomerByName("John")).Returns(new Customer { Name = "John" });

        //    var customerService = new CustomerService(customerRepositoryMock.Object);

        //    // Act
        //    var result = customerService.GetCustomerByName("John");

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal("John", result.Name);
        //}
    }
}
