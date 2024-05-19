using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringPlatformBackEnd.StudyMaterial.Services;

namespace TutoringPlatformBackEnd.StudyMaterialsTests.Integration
{
    [TestClass]
    public class StudyMaterialIntegrationTest
    {
        private HttpClient _httpClient;

        [TestInitialize]
        public void Setup(IHttpClientFactory httpClientFactory)
        {
            var webHost = new WebHostBuilder()
                .UseEnvironment("Development")
                .ConfigureServices(services =>
                {
                    services.AddControllers();
                    services.AddScoped<IStudyMaterialService, StudyMaterialService>();
                }).Build();

            _httpClient = httpClientFactory.CreateClient("TestClient");
        }

    }
}
