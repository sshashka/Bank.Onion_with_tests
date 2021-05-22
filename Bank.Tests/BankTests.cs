using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bank.Data;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Bank.Tests
{
    [TestFixture]
    public class BankIntegrationTests
    {
        private HttpClient _client;
        private CustomWebApplicationFactory _factory;
        private const string RequestUrl = "api/Bank/";

        [SetUp]
        public void SetUp()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }
        [Test]
        public async Task banksController_GetById_ReturnsbankModel()
        {
            var httpResponse = await _client.GetAsync(RequestUrl + 1);
            
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var bank = JsonConvert.DeserializeObject<Orchestrators.Banks.Bank>(stringResponse);
            
            Assert.AreEqual(1, bank.Id);
            Assert.AreEqual(12313, bank.Count);
            Assert.AreEqual("A song of ice and fire", bank.Head);
            Assert.AreEqual("1996", bank.Location);
        }
        [Test]
        public async Task BanksController_Add_AddsBankToDatabase()
        {
            var bank = new Orchestrators.Banks.Bank(){Head = "Charles Dickens", Location = "Two Cities", Count = 213};
            var content = new StringContent(JsonConvert.SerializeObject(bank), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUrl + 1, content);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var bankInResponse = JsonConvert.DeserializeObject<Orchestrators.Banks.Bank>(stringResponse);

            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<BankContext>();
                var databaseBank = await context.Banks.FindAsync(bankInResponse.Id);
                Assert.AreEqual(databaseBank.Id, bankInResponse.Id);
                Assert.AreEqual(databaseBank.Head, bankInResponse.Head);
                Assert.AreEqual(databaseBank.Location, bankInResponse.Location);
            }
        }
        [Test]
        public async Task banksController_Update_UpdatesBankInDatabase()
        {
            var bank = new Orchestrators.Banks.Bank{Id = 1, Count = 1843};
            var content = new StringContent(await JsonConvert.SerializeObjectAsync(bank), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PatchAsync($"/api/Bank/{bank.Id}", content);

            httpResponse.EnsureSuccessStatusCode();
            
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<BankContext>();
                var databasebank = await context.Banks.FindAsync(bank.Id);
                Assert.AreEqual(bank.Id, databasebank.Id);
            }
        }
        [Test]
        public async Task BooksController_DeleteById_DeletesBookFromDatabase()
        {
            var bankId = 1;
            var httpResponse = await _client.DeleteAsync(RequestUrl + bankId);

            httpResponse.EnsureSuccessStatusCode();
            
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<BankContext>();
                
                Assert.AreEqual(0, context.Banks.Count());
            }
        }
    }
    
    
}