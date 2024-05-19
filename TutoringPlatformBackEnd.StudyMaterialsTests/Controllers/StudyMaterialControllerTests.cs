using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using TutoringPlatformBackEnd.StudyMaterial.Model;
using TutoringPlatformBackEnd.StudyMaterial.Services;
using Moq;
using MongoDB.Bson;

namespace TutoringPlatformBackEnd.StudyMaterial.Controllers.Tests
{
    [TestClass()]
    public class StudyMaterialControllerTests
    {
        [TestMethod]
        public async Task GetAllStudyMaterials_ReturnsOkResultWithStudyMaterials()
        {
            // Arrange
            var mockService = new Mock<IStudyMaterialService>();
            mockService.Setup(service => service.GetAllStudyMaterialsAsync())
                       .ReturnsAsync(new List<StudyMaterialModel>());

            var controller = new StudyMaterialController(mockService.Object);

            // Act
            var result = await controller.GetAllStudyMaterials();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.IsNotNull((result.Result as OkObjectResult)?.Value);
            CollectionAssert.AreEqual(new List<StudyMaterialModel>(), (result.Result as OkObjectResult)?.Value as List<StudyMaterialModel>);
        }

        [TestMethod]
        public async Task GetStudyMaterialById_ExistingId_ReturnsOkResultWithStudyMaterial()
        {
            // Arrange
            var mockService = new Mock<IStudyMaterialService>();
            var expectedStudyMaterial = new StudyMaterialModel { Id = ObjectId.GenerateNewId(), Title = "test Material" };

            mockService.Setup(service => service.GetStudyMaterialByIdAsync("1"))
                       .ReturnsAsync(expectedStudyMaterial);

            var controller = new StudyMaterialController(mockService.Object);

            // Act
            var result = await controller.GetStudyMaterialById("1");

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(expectedStudyMaterial, (result.Result as OkObjectResult)?.Value);
        }

        [TestMethod]
        public async Task GetStudyMaterialById_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var mockService = new Mock<IStudyMaterialService>();
            mockService.Setup(service => service.GetStudyMaterialByIdAsync("100"))
                       .ReturnsAsync((StudyMaterialModel)null);

            var controller = new StudyMaterialController(mockService.Object);

            // Act
            var result = await controller.GetStudyMaterialById("100");

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetStudyMaterialsByTutorId_ReturnsOkResultWithStudyMaterials()
        {
            // Arrange
            var mockService = new Mock<IStudyMaterialService>();
            mockService.Setup(service => service.GetStudyMaterialsByTutorIdAsync("tutorId"))
                       .ReturnsAsync(new List<StudyMaterialModel>());

            var controller = new StudyMaterialController(mockService.Object);

            // Act
            var result = await controller.GetStudyMaterialsByTutorId("tutorId");

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.IsNotNull((result.Result as OkObjectResult)?.Value);
            CollectionAssert.AreEqual(new List<StudyMaterialModel>(), (result.Result as OkObjectResult)?.Value as List<StudyMaterialModel>);
        }
        [TestMethod]
        public async Task CreateStudyMaterial_ValidInput_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var mockService = new Mock<IStudyMaterialService>();
            var inputStudyMaterial = new StudyMaterialModel
            {
                Title = "Sample Material",
                EducationLevel = "High School",
                Tags = new List<string> { "Math", "Science" },
                ContentURL = "http://example.com/content.pdf",
                CoverImageURL = "http://example.com/cover.jpg"
            };
            var expectedStudyMaterial = new StudyMaterialModel
            {
                Id = ObjectId.GenerateNewId(), // Ensure the expected study material has an Id.
                Title = "Sample Material",
                EducationLevel = "High School",
                Tags = new List<string> { "Math", "Science" },
                ContentURL = "http://example.com/content.pdf",
                CoverImageURL = "http://example.com/cover.jpg"
            };

            mockService.Setup(service => service.CreateStudyMaterialAsync(It.IsAny<StudyMaterialModel>()))
                       .ReturnsAsync((StudyMaterialModel sm) =>
                       {
                           sm.Id = expectedStudyMaterial.Id; // Simulate setting the Id during creation.
                           return sm;
                       });

            var controller = new StudyMaterialController(mockService.Object);

            // Act
            var result = await controller.CreateStudyMaterial(inputStudyMaterial);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual(nameof(StudyMaterialController.GetStudyMaterialById), createdAtActionResult.ActionName);
            var returnedStudyMaterial = createdAtActionResult.Value as StudyMaterialModel;
            Assert.IsNotNull(returnedStudyMaterial);
            Assert.AreEqual(expectedStudyMaterial.Id, returnedStudyMaterial.Id);
            Assert.AreEqual(expectedStudyMaterial.Title, returnedStudyMaterial.Title);
            Assert.AreEqual(expectedStudyMaterial.EducationLevel, returnedStudyMaterial.EducationLevel);
            CollectionAssert.AreEqual(expectedStudyMaterial.Tags, returnedStudyMaterial.Tags); // Compare collections by contents
            Assert.AreEqual(expectedStudyMaterial.ContentURL, returnedStudyMaterial.ContentURL);
            Assert.AreEqual(expectedStudyMaterial.CoverImageURL, returnedStudyMaterial.CoverImageURL);
        }

        [TestMethod]
        public async Task UpdateStudyMaterial_ValidInput_ReturnsNoContentResult()
        {
            // Arrange
            var mockService = new Mock<IStudyMaterialService>();
            var inputId = "1";
            var inputStudyMaterial = new StudyMaterialModel { Title = "Updated Material" };

            var controller = new StudyMaterialController(mockService.Object);

            // Act
            var result = await controller.UpdateStudyMaterial(inputId, inputStudyMaterial);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteStudyMaterial_ValidId_ReturnsNoContentResult()
        {
            // Arrange
            var mockService = new Mock<IStudyMaterialService>();
            var inputId = "1";

            var controller = new StudyMaterialController(mockService.Object);

            // Act
            var result = await controller.DeleteStudyMaterial(inputId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
    }
}