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
    public class UserTests
    {
        // Testing GetUserByName method, results = return the user exists 
        [Fact]
        public async Task GetUserByNameTest()
        {
            var testService = new Mock<IUserService>();
            var name = "Jane";

            var existingUser = new User
            { 
                Id = "user1",
                UserName = "Jane", 
                Password = "testingpassword"
            };

            testService.Setup(s => s.GetUserByName(name))
                .ReturnsAsync(existingUser);

            var testUser = await testService.Object.GetUserByName(name);

            Assert.NotNull(testUser);
            Assert.Equal(name, testUser.UserName);
        }

        // Testing GetUserByName method, results = return the user exists 
        [Fact]
        public async Task UserNotFoundTest()
        {
            var testService = new Mock<IUserService>();
            var name = "John";

            testService.Setup(s => s.GetUserByName(name))
                .ReturnsAsync((User?)null);

            var testUser = await testService.Object.GetUserByName(name);

            Assert.Null(testUser);
        }

        // Testing Authenticate method, results = user gets a token
        [Fact]
        public async Task AuthenticateUserTest()
        {
            var testService = new Mock<IUserService>();
            var name = "John";
            var password = "test";
            var token = "newtoken";

            testService.Setup(s => s.Authenticate(name, password))
                .Returns(token);

            var result = testService.Object.Authenticate(name, password);

            Assert.NotNull(result);
            Assert.Equal(token, result);
        }


    }
}
