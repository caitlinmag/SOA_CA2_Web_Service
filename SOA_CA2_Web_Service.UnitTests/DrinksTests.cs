using APIService;
using APIService.Controllers;
using APIService.DTOs;
using APIService.Interfaces;
using APIService.Models;
using APIService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using MongoDB.Driver;
using Moq;
using System;
using Xunit;

namespace SOA_CA2_Web_Service.UnitTests
{
    public class DrinksTests
    {
        // Testing GetAllDrinks method, result = returns list of all drinks 
        [Fact]
        public async Task GetAllDrinksTest()
        {
            var testService = new Mock<IDrinksService>();

            var mockDrinks = new List<DrinkItem> { 
                new DrinkItem {DrinkItemId = "6561f5b682e0e02c61099324", DrinkName = "Americano"},
                new DrinkItem {DrinkItemId = "6561f5b682e0e02c61099325", DrinkName = "Latte"},
            };

            testService.Setup(s => s.GetAllDrinks())
                .ReturnsAsync(mockDrinks);

            var testController = new DrinkItemsController(testService.Object);

            var result = await testController.GetDrinks();

            var getResult = Assert.IsType<List<DrinkDTO>>(result.Value);

            Assert.Equal(2, getResult.Count); // checking how many drinks in result
            Assert.Equal("Americano", getResult.First().DrinkName);
        }

        // Testing GetDrinkByID method, result = returns drink id and its values 
        [Fact]
        public async Task GetDrinkByIDTest()
        {
            var testService = new Mock<IDrinksService>();

            string testId = "6561f5b682e0e02c61099324";

            var drink = new DrinkItem { 
                DrinkItemId = testId, DrinkName = "Americano" , SupplierId = "7561f5b682e0e02c61099324"
            };

            var supplier = new Supplier {
                SupplierId = "7561f5b682e0e02c61099324", SupplierName = "Costa", Location = "Dublin" 
            };

            // using get drink read DTO, as it takes drink and supplier values 
            testService.Setup(s => s.GetDrinkRead(testId))
               .ReturnsAsync((drink, supplier));

            var testController = new DrinkItemsController(testService.Object);

            var result = await testController.GetDrinkItem(testId);

            var testDrink = Assert.IsType<DrinkReadDTO>(result.Value);

            // checking if the test results match the drink item values
            Assert.Equal(testId, testDrink.DrinkItemId);
            Assert.Equal("Americano", testDrink.DrinkName);
            Assert.Equal("Costa", testDrink.SupplierName);
            Assert.Equal("Dublin", testDrink.Location);
        }
    }
}
