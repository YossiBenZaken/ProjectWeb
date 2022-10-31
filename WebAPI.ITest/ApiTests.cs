using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Json;
using WebAPI.Models;

namespace WebAPI.ITest
{
    [TestClass]
    public class ApiTests
    {
        private HttpClient _httpClient;
        public ApiTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }
        [TestMethod]
        public async Task GetAllSnackbars_ReturnsAllTheSnackbars()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Snackbar>>("/api/Snackbars");
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Count() == 24);
        }
        [TestMethod]
        public async Task GetSnackbar_ReturnOneSnackbar()
        {
            var response = await _httpClient.GetFromJsonAsync<Snackbar>("/api/Snackbars/1");
            Assert.IsNotNull(response);
            Assert.IsTrue(response.id == 1);
        }
        [TestMethod]
        public async Task GetSnackbar_ReturnNotFound()
        {
            var response = await _httpClient.GetAsync("/api/Snackbars/100");
            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }
    }
}