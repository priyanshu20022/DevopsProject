using Jenkins.API.Controllers;
using Jenkins.API.Context;
using Jenkins.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Jenkins.Test
{
    public class ProjectControllerTests
    {
        private DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetAllProducts_ReturnsOkResultWithListOfProjects()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new AppDbContext(options))
            {
                context.Projects.Add(new Project { Id = 1, Name = "Project 1", LastModified = DateTime.Now, CreatedOn = DateTime.Now, OwnerId = 1, Shared = false });
                context.Projects.Add(new Project { Id = 2, Name = "Project 2", LastModified = DateTime.Now, CreatedOn = DateTime.Now, OwnerId = 2, Shared = true });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var controller = new ProjectController(context);

                // Act
                var result = await controller.GetAllProducts();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var projects = Assert.IsType<List<Project>>(okResult.Value);
                Assert.Equal(2, projects.Count);
            }
        }

        [Fact]
        public async Task AddProject_ReturnsOkResultWithMessage()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new AppDbContext(options))
            {
                var controller = new ProjectController(context);
                var projectToAdd = new Project { Name = "New Project", LastModified = DateTime.Now, CreatedOn = DateTime.Now, OwnerId = 1, Shared = false };

                // Act
                var result = await controller.AddProject(projectToAdd);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var resultValue = okResult.Value as object; // Cast to object
                Assert.NotNull(resultValue); // Ensure the result value is not null

                // Use reflection to retrieve the 'Message' property value
                var messageProperty = resultValue.GetType().GetProperty("Message");
                if (messageProperty == null)
                {
                    throw new Exception("Returned object does not contain a 'Message' property.");
                }
                var message = messageProperty.GetValue(resultValue) as string;

                // Assert the content of the 'Message' property
                Assert.NotNull(message); // Ensure the message is not null
                Assert.Equal("Project added successfully!", message); // Assert the message content
            }
        }




    }
}
