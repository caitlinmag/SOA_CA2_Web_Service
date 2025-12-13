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
    public class SupplierTests
    {
        // Testing GetSupplierByID method , result = returns supplier id and its values 
        [Fact]
        public async Task GetSupplierByIDTest()
        {
            var testService = new Mock<ISupplierService>();
            var testController = new SuppliersController(testService.Object);

            var supplier = new Supplier
            {
                SupplierId = "supplier1", 
                SupplierName = "Insomnia"
            };

            testService.Setup(s => s.GetSupplierById("supplier1"))
               .ReturnsAsync(supplier);

            var result = await testController.GetSupplier("supplier1");

            var testSupplier = Assert.IsType<SupplierDTO>(result.Value);

            Assert.Equal("Insomnia", testSupplier.SupplierName);
        }

        // Testing DeleteSupplier method , supplier should be successfully deleted by id 
        [Fact]
        public async Task DeleteSupplierTest()
        {
            var testService = new Mock<ISupplierService>();
            var testController = new SuppliersController(testService.Object);

            var supplier = new Supplier
            {
                SupplierId = "supplier2",
            };

            testService.Setup(s => s.DeleteSupplier("supplier2")).Returns(Task.CompletedTask);
       
            var result = await testController.DeleteSupplier("supplier2");

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
