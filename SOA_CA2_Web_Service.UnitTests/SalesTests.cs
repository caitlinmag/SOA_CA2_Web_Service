using APIService.Controllers;
using APIService.DTOs;
using APIService.Interfaces;
using APIService.Models;
using Microsoft.AspNetCore.Mvc;
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
            var testController = new DrinkSalesController(testService.Object);

            var saleDto = new SaleCreateDTO
            {
                DrinkItemId = "6561f5b682e0e02c61099324",
                Quantity = 3
            };

            // using It.isAny from - https://dev.to/imdj/unit-testing-aspnet-core-web-api-with-moq-and-xunit-controllers-services-nci?utm_source=chatgpt.com
            testService.Setup(s => s.CreateSale(It.IsAny<DrinksSales>()))
                .Returns(Task.CompletedTask);

            var result = await testController.PostSaleItem(saleDto);

            var createdSale = Assert.IsType<CreatedAtActionResult>(result.Result);

            var sale = Assert.IsType<SaleDTO>(createdSale.Value);

            Assert.Equal(3, sale.Quantity);
        }


        // Testing UpdateSale method, results = existing sale has been updated with new values
        [Fact]
        public async Task UpdateSaleTest()
        {
            var testService = new Mock<IDrinkSalesService>();
            var testController = new DrinkSalesController(testService.Object);

            var newSaleDetails = new SaleUpdateDTO
            {
                DrinkItemId = "6561f5b682e0e02c61099324",
                Quantity = 4
            };

            var existingSale = new DrinksSales
            {
                DrinksSalesId = "sale1", 
                DrinkItemId = "6561f5b682e0e02c61099324",
                Quantity = 10 // updating the quantity amount
            };

            testService.Setup(s => s.GetSaleById("sale1"))
                .ReturnsAsync(existingSale);

            testService.Setup(s => s.UpdateSale("sale1", It.IsAny<DrinksSales>()))
                .Returns(Task.CompletedTask);

            var result = await testController.PutSalesItem("sale1", newSaleDetails);
            Assert.IsType<NoContentResult>(result);
        }

        // Testing UpdateSale method if the sale id doesn't exist 
        [Fact]
        public async Task UpdateSaleNotFoundTest()
        {
            var testService = new Mock<IDrinkSalesService>();
            var testController = new DrinkSalesController(testService.Object);

            testService.Setup(s => s.GetSaleById("saledoesntexist"))
                .ReturnsAsync((DrinksSales)null);

            var result = await testController.PutSalesItem("saledoesntexist", new SaleUpdateDTO());
            Assert.IsType<NotFoundResult>(result);
        }
    }
    }

