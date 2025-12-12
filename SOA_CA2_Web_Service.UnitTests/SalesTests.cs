using APIService.Controllers;
using APIService.DTOs;
using APIService.Interfaces;
using APIService.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SOA_CA2_Web_Service.UnitTests
{
    public class SalesTests
    {
        // Testing CreateSale method , results = new sale is created 
        [Fact]
        public async Task CreateSaleTest()
        {
            var testService = new Mock<IDrinkSalesService>();

            var newSale = new SaleCreateDTO
            {
                DrinkItemId = "6561f5b682e0e02c61099324",
                Quantity = 3
            };


            testService.Setup(s => s.CreateSale(newSale))
                .ReturnsAsync(mockDrinks)
        }
        }
    }
}
