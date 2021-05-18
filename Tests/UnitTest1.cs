using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bank.Core.Exceptions;
using Bank.Onion_with_tests;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Tests
{
    public class UnitTest1
    :
    IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _cliento;
        private readonly CustomWebApplicationFactory<Startup>
            _factory;

        public UnitTest1(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _cliento = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetAsync_IfServiceReturnsCorrectbank_ReturnOk()
        {
            // Arrange
            var postedBank = new global::Bank.Orchestrators.Bank.Bank()
            {
                Id = 1312,
                Location = "sdasdasda",
                CountOfWorkers = 12324,
                Head = "dsdad"
            };

            var addRequest = new HttpRequestMessage(HttpMethod.Post, "/Bank")
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                    postedBank), 
                    Encoding.UTF8, "application/json")
            };

            //Act
            var addResponse = await _cliento.SendAsync(addRequest);
            var bank = await getModelFromHttpResponce(addResponse);

            var getResponse = await _cliento.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"/Bank?id={bank.Id}"));
            bank = await getModelFromHttpResponce(getResponse);

            // Assert
            addResponse.EnsureSuccessStatusCode();
            getResponse.EnsureSuccessStatusCode();
            Assert.Equal(postedBank.Id, bank.Id);
            Assert.Equal(postedBank.Location, bank.Location);
            Assert.Equal(postedBank.Head, bank.Head);
            Assert.Equal(postedBank.CountOfWorkers, bank.CountOfWorkers);
        }

        [Fact]
        public async Task GetAsync_IfServiceThrowsExceptionWhenIdUndefined_ReturnOk()
        {
            // Arrange
            int undefinedId = 99999;

            var getRequest = new HttpRequestMessage(HttpMethod.Get, $"/Bank?bankId={undefinedId}");
            //Act

            var exception = await Assert.ThrowsAsync<System.InvalidOperationException>(async () => await _cliento.SendAsync(getRequest));

            // Assert
            Assert.NotNull(exception);

        }

        [Fact]
        public async Task PostAsync_IfServiceReturnsbank_ReturnOk()
        {
            // Arrange
            var postingBank = new global::Bank.Orchestrators.Bank.Bank()
            {
                Location = "sdasdasda",
                CountOfWorkers = 12324,
                Head = "dsdad"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/Bank")
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                        postingBank),
                    Encoding.UTF8, "application/json")
            };

            //Act
            var response = await _cliento.SendAsync(request);

            var byteResult = await response.Content.ReadAsByteArrayAsync();
            var stringResult = Encoding.UTF8.GetString(byteResult);
            var bank = JsonConvert.DeserializeObject<global::Bank.Core.Bank.Bank>(stringResult);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal(postingBank.Location, bank.Location);
            Assert.Equal(postingBank.Head, bank.Head);
            Assert.Equal(postingBank.CountOfWorkers, bank.CountOfWorkers);
        }

        [Fact]
        public async Task PatcAsync_IfLoginCorrect_ReturnOk()
        {
            // Arrange
            var addedBank = new global::Bank.Core.Bank.Bank()
            {
                Location = "sdasdasda",
                CountOfWorkers = 12324,
                Head = "dsdad"
            };
            var updatedUpdate = new global::Bank.Orchestrators.Bank.Bank()
            {
                Location = "sdasdasda",
                CountOfWorkers = 12324,
                Head = "dsdad"
            };
            var postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Bank")
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                        addedBank),
                    Encoding.UTF8,
                    "application/json")
            };

            //Act
            var postResponce = await _cliento.SendAsync(postRequest);
            var bank = await getModelFromHttpResponce(postResponce);

            var patchResponce = await _cliento.SendAsync(new HttpRequestMessage(HttpMethod.Patch, $"/Bank?bankId={bank.Id}&newLogin={updatedUpdate.CountOfWorkers}"));

            var getResponce = await _cliento.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"/Bank?bankId={bank.Id}"));
            bank = await getModelFromHttpResponce(getResponce);

            // Assert
            postResponce.EnsureSuccessStatusCode();
            patchResponce.EnsureSuccessStatusCode();
            getResponce.EnsureSuccessStatusCode();

            Assert.Equal(updatedUpdate.Location, updatedUpdate.Location);
            Assert.Equal(updatedUpdate.Head, updatedUpdate.Head);
            Assert.Equal(updatedUpdate.CountOfWorkers, updatedUpdate.CountOfWorkers);
        }

        [Fact]
        public async Task PostbankWithExistingLogin_IfProgramThrowException_ReturnOk()
        {
            // Arrange
            var bank = new global::Bank.Orchestrators.Bank.Bank()
            {
                Location = "sdasdasda",
                CountOfWorkers = 12324,
                Head = "dsdad"
            };
            var request = new HttpRequestMessage(HttpMethod.Post, "/Bank")
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                        new global::Bank.Orchestrators.Bank.Bank
                        {
                            Location = "sdasdasda",
                            CountOfWorkers = 12324,
                            Head = "dsdad"
                        }),
                    Encoding.UTF8,
                    "application/json")
            };

            var duplicateRequest = new HttpRequestMessage(HttpMethod.Post, "/Bank")
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                        new global::Bank.Orchestrators.Bank.Bank
                        {
                            Location = "sdasdasda",
                            CountOfWorkers = 12324,
                            Head = "dsdad"
                        }),
                    Encoding.UTF8,
                    "application/json")
            };

            //Act
            var response = await _cliento.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var exception = await Assert.ThrowsAsync<FailedInsertionException>(async () => await _cliento.SendAsync(duplicateRequest));

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task Deletebank_IfWorksAndSecondDeleteThrowsException_ReturnOk()
        {
            // Arrange
            var postedbank = new global::Bank.Orchestrators.Bank.Bank()
            {
                Location = "sdasdasda",
                CountOfWorkers = 12324,
                Head = "dsdad"
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Bank")
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                        postedbank),
                    Encoding.UTF8,
                    "application/json")
            };

            //Act
            var postResponse = await _cliento.SendAsync(postRequest);
            var bank = await getModelFromHttpResponce(postResponse);

            var deleteResponce = await _cliento.SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"/Bank?BankId={bank.Id}"));
  
            var exception = await Assert.ThrowsAsync<AlreadyDeletedException>(async () => await _cliento.SendAsync
            (new HttpRequestMessage(HttpMethod.Delete, $"/Bank?bankId={bank.Id}")));

            // Assert
            postResponse.EnsureSuccessStatusCode();
            deleteResponce.EnsureSuccessStatusCode();

            Assert.NotNull(exception);
        }

        [Fact]
        public async Task GetAndPatchDeletedbank_IfThrowsException_ReturnOk()
        {
            // Arrange
            var postedbank = new global::Bank.Orchestrators.Bank.Bank()
            {
                Location = "sdasdasda",
                CountOfWorkers = 12324,
                Head = "dsdad"
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Bank")
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                        postedbank),
                    Encoding.UTF8,
                    "application/json")
            };

            //Act
            var postResponse = await _cliento.SendAsync(postRequest);
            var bank = await getModelFromHttpResponce(postResponse);

            var deleteResponce = await _cliento.SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"/Bank?bankId={bank.Id}"));

            var onPostException = await Assert.ThrowsAsync<AlreadyDeletedException>(async () => await _cliento.SendAsync(
                new HttpRequestMessage(HttpMethod.Get, $"/bank?bankId={bank.Id}")));

            // Assert
            postResponse.EnsureSuccessStatusCode();
            deleteResponce.EnsureSuccessStatusCode();
            Assert.NotNull(onPostException);
            
        }

        async Task<global::Bank.Core.Bank.Bank> getModelFromHttpResponce(HttpResponseMessage responce)
        {
            var byteResult = await responce.Content.ReadAsByteArrayAsync();
            var stringResult = Encoding.UTF8.GetString(byteResult);
            var record = JsonConvert.DeserializeObject<global::Bank.Core.Bank.Bank>(stringResult);
            return record;
        }
    }
}