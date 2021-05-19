using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bank.Onion_with_tests;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Tests
{
    public class ClientTest :
    IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup>
            _factory;

        public ClientTest(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task PostAsync_IfRecordInserted_ReturnOk()
        {
            // Arrange
            int bankId = 1;
            global::Bank.Orchestrators.Client.Clients clients = new global::Bank.Orchestrators.Client.Clients()
            {
                Name = "fefwefwf",
                SecondName = "dqwdq",
                Sum = 13123
            };
            var request = new HttpRequestMessage(HttpMethod.Post, $"/Bank/{bankId}/Bank")
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                        clients),
                    Encoding.UTF8,
                    "application/json")
            };

            //Act
            var responce = await _client.SendAsync(request);

            // Assert
            responce.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, responce.StatusCode);

        }

        [Fact]
        public async Task clientsListGetAsync_IfCorrectListReturned_ReturnOk()
        {
            // Arrange
            int bankId = 1;
            int sum = 1000;
            string name = "fwefwe";
            string secondName = "ewfwefwef";
            global::Bank.Orchestrators.Client.Clients clients1 = new global::Bank.Orchestrators.Client.Clients()
            {
                Name = name,
                SecondName = secondName,
                Sum = sum
            };
            
            var firstPostRequest = new HttpRequestMessage(HttpMethod.Post, $"/Bank/{bankId}/Bank")
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                        clients1),
                    Encoding.UTF8,
                    "application/json")
            };
            var getRequest = new HttpRequestMessage(HttpMethod.Get, $"/Bank/{bankId}/Bank?");
            //Act
            var firstPostResponce = await _client.SendAsync(firstPostRequest);

            var getResponce =  await _client.SendAsync(getRequest);
            var clients = await getModelListFromHttpResponce(getResponce);

            // Assert
            firstPostResponce.EnsureSuccessStatusCode();
            getResponce.EnsureSuccessStatusCode();
            Assert.Equal(2, clients.Count);
            Assert.Equal(clients1.Sum, clients1.Sum);
        }
        [Fact]
        public async Task clientsDeleteAsync_IfRecordDeleted_ReturnOk()
        {
            // Arrange
            int bankId = 3;
            global::Bank.Orchestrators.Client.Clients clients = new global::Bank.Orchestrators.Client.Clients()
            {
                Name = "edqwqw",
                SecondName = "secondName",
                Sum = 123
            };
            
            var firstPostRequest = new HttpRequestMessage(HttpMethod.Post, $"/Bank/{bankId}/Bank?")
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                        clients),
                   Encoding.UTF8,
                   "application/json")
            };

            //Act
            var firstPostResponce = await _client.SendAsync(firstPostRequest);
            var record = await getModelFromHttpResponce(firstPostResponce);

            var deleteResponce = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"/Bank/Client?clientId={clients.Id}"));
            var exception = await Assert.ThrowsAsync<System.InvalidOperationException>
                (async () => await _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"/Bank/Client?clientId={clients.Id}")));

            // Assert
            firstPostResponce.EnsureSuccessStatusCode();
            deleteResponce.EnsureSuccessStatusCode();
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task RecordUpdateAsync_IfRecordUpdated_ReturnOk()
        {
            // Arrange
            int bankId = 1;
            global::Bank.Orchestrators.Client.Clients clients1 = new global::Bank.Orchestrators.Client.Clients()
            {
                Name = "edqwqw",
                SecondName = "secondName",
                Sum = 123
            };
            global::Bank.Orchestrators.Client.Clients clients2 = new global::Bank.Orchestrators.Client.Clients()
            {
                Name = "edqwffdsfsqw",
                SecondName = "sefdsfsdcondName",
                Sum = 1223
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, $"/Bank/{bankId}/Bank?")
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                      clients1),
                   Encoding.UTF8,
                   "application/json")
            };

            var getRequest = new HttpRequestMessage(HttpMethod.Get, $"/Bank/{bankId}/Client?");

            //Act
            var postResponce = await _client.SendAsync(postRequest);
            var record = await getModelFromHttpResponce(postResponce);

            var updatedResponce = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Put, $"/Bank/Client?bankId={clients1.Id}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                      clients2),
                   Encoding.UTF8,
                   "application/json")
            });

            var getResponce = await _client.SendAsync(getRequest);
            var recordList = await getModelListFromHttpResponce(getResponce);

            // Assert
            postResponce.EnsureSuccessStatusCode();
            updatedResponce.EnsureSuccessStatusCode();
            Assert.Equal(clients1.Id, recordList[0].Id);
            Assert.Equal(clients2.Sum, recordList[0].Sum);
        }
       
        [Fact]
        public async Task RecordPatchAsync_IfRecordPatched_ReturnOk()
        {
            // Arrange
            int userId = 1;
            int addedAmount = 10000;

            global::Bank.Orchestrators.Client.Clients clients = new global::Bank.Orchestrators.Client.Clients()
            {
                Name = "edqwffdsfsqw",
                SecondName = "sefdsfsdcondName",
                Sum = 1223
            };
            var postRequest = new HttpRequestMessage(HttpMethod.Post, $"/User/{userId}/Record")
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                      clients),
                   Encoding.UTF8,
                   "application/json")
            };

            //Act
            var postResponce = await _client.SendAsync(postRequest);
            var record = await getModelFromHttpResponce(postResponce);

            var patchResponce = await _client.SendAsync(
                new HttpRequestMessage(HttpMethod.Patch, $"/User/Record?newAmount={addedAmount}&recordId={record.Id}"));

            var getResponce = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"/User/Record?recordId={record.Id}"));
            record = await getModelFromHttpResponce(getResponce);

            // Assert
            patchResponce.EnsureSuccessStatusCode();
            postResponce.EnsureSuccessStatusCode();
            Assert.Equal(addedAmount + clients.Sum, clients.Sum);
        }
        [Fact]
        public async Task RecordPatchAsync_IfThrowsOverflowException_ReturnOk()
        {
            // Arrange
            int bankId = 1;

            global::Bank.Orchestrators.Client.Clients clients = new global::Bank.Orchestrators.Client.Clients()
            {
                Name = "edqwffdsfsqw",
                SecondName = "sefdsfsdcondName",
                Sum = 1223
            };
            var postRequest = new HttpRequestMessage(HttpMethod.Post, $"/User/{bankId}/Record")
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                      clients),
                   Encoding.UTF8,
                   "application/json")
            };

            int newAmount = int.MaxValue;
            int recordId = 1;

            var updateRequest = new HttpRequestMessage(HttpMethod.Patch, $"/User/Record?newAmount={newAmount}&recordId={recordId}");

            //Act
            var postResponce = await _client.SendAsync(postRequest);
            var exception = await Assert.ThrowsAsync<System.OverflowException>(async () => await _client.SendAsync(updateRequest));

            // Assert
            postResponce.EnsureSuccessStatusCode();
            Assert.NotNull(exception);
        }
        [Fact]
        public async Task RecordDeleteListAsync_IfWorks_ReturnOk()
        {
            // Arrange
            int categoryId = 1;
            int userId = 1;
            int amount = 1000;
            DateTime firstInsertedDate = new DateTime(2001, 10, 20);
            DateTime secondInsertedDate = new DateTime(2001, 10, 23);
            DateTime endDate = new DateTime(2001, 10, 26);
            global::Bank.Orchestrators.Client.Clients firstclients = new global::Bank.Orchestrators.Client.Clients()
            {
                Name = "edqwffdsfsqw",
                SecondName = "sefdsfsdcondName",
                Sum = 1223
            };
            global::Bank.Orchestrators.Client.Clients secondClient = new global::Bank.Orchestrators.Client.Clients()
            {
                Name = "edqwffdsfsqw",
                SecondName = "sefdsfsdcondName",
                Sum = 1223
            };

            var PostRequest = new HttpRequestMessage(HttpMethod.Post, $"/Bank/{userId}/Client")
            {
                Content = new StringContent(JsonConvert.SerializeObject(
                        firstclients),
                    Encoding.UTF8,
                    "application/json")
            };

            var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, $"/Bank/{userId}/Client");
            var getRequest = new HttpRequestMessage(HttpMethod.Get, $"/Bank/{userId}/Client");

            //Act
            var PostResponce = await _client.SendAsync(PostRequest);
            var DeleteResponce = await _client.SendAsync(deleteRequest);
            var GetResponce = await _client.SendAsync(getRequest);

            var clients = await getModelListFromHttpResponce(GetResponce);
            // Assert
            PostResponce.EnsureSuccessStatusCode();
            DeleteResponce.EnsureSuccessStatusCode();
            GetResponce.EnsureSuccessStatusCode();
            Assert.Empty(clients);
        }

        async Task<global::Bank.Orchestrators.Client.Clients> getModelFromHttpResponce(HttpResponseMessage responce)
        {
            var byteResult = await responce.Content.ReadAsByteArrayAsync();
            var stringResult = Encoding.UTF8.GetString(byteResult);
            var record = JsonConvert.DeserializeObject<global::Bank.Orchestrators.Client.Clients>(stringResult);
            return record;
        }

        async Task<List<global::Bank.Orchestrators.Client.Clients>> getModelListFromHttpResponce(HttpResponseMessage responce)
        {
            var byteResult = await responce.Content.ReadAsByteArrayAsync();
            var stringResult = Encoding.UTF8.GetString(byteResult);
            var clients = JsonConvert.DeserializeObject<List<Bank.Orchestrators.Client.Clients>>(stringResult);
            return clients;
        }
    }
}