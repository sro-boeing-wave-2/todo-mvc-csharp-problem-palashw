//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.TestHost;
//using Newtonsoft.Json;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Linq;
//using Xunit;
//using System.Net.Http.Headers;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection.Extensions;
//using System.Net;
//using GoogleKeepDB;
//using GoogleKeepDB.Model;
//using MongoDB.Driver;

//namespace GoogleDBInMemoryTest
//{
//    public class KeepControllerIntegrationTests
//    {
//        private MongoClient _client;
//        private MongoServer _server;

//        public KeepControllerIntegrationTests()
//        {
//            _client = new MongoClient("mongodb://localhost:27017");
//            _server = _client.GetServer();


//            List<Keep> TestKeep = new List<Keep>
//            {
//                new Keep()
//                {
//                    title = "First",
//                    plainText = "First sentence",
//                    isPinned = true,
//                    checklist = new List<Checklist>() { new Checklist { item = "hello" }, new Checklist { item = "brother" } },
//                    label = "random"
//                }
//            };
//            _context.Keep.AddRange(TestKeep);
//            _context.SaveChanges();
//        }

//        Keep testKeepPost = new Keep()
//        {
//            keepID = 2,
//            title = "First1",
//            plainText = "First1 sentence",
//            isPinned = true,
//            checklist = new List<Checklist>() { new Checklist { item = "hello1" }, new Checklist { item = "brother1" } },
//            label = "random1"
//        };

//        Keep testNotePut = new Keep()
//        {
//            keepID = 1,
//            title = "puttitle",
//            plainText = "First1 sentence",
//            isPinned = true,
//            checklist = new List<Checklist>() { new Checklist { item = "hello1" }, new Checklist { item = "brother1" } },
//            label = "random1"
//        };

//        Keep testNoteDelete = new Keep()
//        {
//            title = "deleted",
//            plainText = "First1 sentence",
//            isPinned = true,
//            checklist = new List<Checklist>() { new Checklist { item = "hello1" }, new Checklist { item = "brother1" } },
//            label = "random1"
//        };

//        [Fact]
//        public async Task TestPost()
//        {
//            var content = JsonConvert.SerializeObject(testKeepPost);
//            var stringcontent = new StringContent(content, Encoding.UTF8, "application/json");
//            var response = await _client.PostAsync("/api/keep", stringcontent);
//            var responseString = await response.Content.ReadAsStringAsync();
//            var note = JsonConvert.DeserializeObject<Keep>(responseString);
//            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//        }

//        [Fact]
//        public async Task TestGetAll()
//        {
//            var response = await _client.GetAsync("/api/keep");
//            var responseString = await response.Content.ReadAsStringAsync();
//            var notes = JsonConvert.DeserializeObject<List<Keep>>(responseString);
//            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//        }

//        [Fact]
//        public async Task TestGetByID()
//        {
//            var response = await _client.GetAsync("api/keep/1");
//            var responseString = await response.Content.ReadAsStringAsync();
//            var notes = JsonConvert.DeserializeObject<Keep>(responseString);
//            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//            Assert.Equal("First", notes.title);
//        }

//        [Fact]
//        public async Task TestGetByLabel()
//        {
//            var response = await _client.GetAsync("api/keep/label/random");
//            var responseString = await response.Content.ReadAsStringAsync();
//            var notes = JsonConvert.DeserializeObject<List<Keep>>(responseString);
//            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//        }

//        [Fact]
//        public async Task TestGetByTitle()
//        {
//            var response = await _client.GetAsync("api/keep/title/First");
//            var responseString = await response.Content.ReadAsStringAsync();
//            var notes = JsonConvert.DeserializeObject<List<Keep>>(responseString);
//            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//        }

//        [Fact]
//        public async Task TestGetByPinned()
//        {
//            var response = await _client.GetAsync("api/keep/ispinned/true");
//            var responseString = await response.Content.ReadAsStringAsync();
//            var notes = JsonConvert.DeserializeObject<List<Keep>>(responseString);
//            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//        }

//        [Fact]
//        public async Task TestPut()
//        {
//            var contentChange = JsonConvert.SerializeObject(testNotePut);
//            var stringContentChange = new StringContent(contentChange, Encoding.UTF8, "application/json");
//            var responseChanged = await _client.PutAsync("/api/keep/1", stringContentChange);
//            responseChanged.EnsureSuccessStatusCode();
//            var responseString = await responseChanged.Content.ReadAsStringAsync();
//            var note = JsonConvert.DeserializeObject<Keep>(responseString);
//            Assert.Equal(HttpStatusCode.OK, responseChanged.StatusCode);
//        }

//        [Fact]
//        public async Task TestDelete()
//        {
//            var response = await _client.DeleteAsync("api/keep/1");
//            var responsecode = response.StatusCode;
//            Assert.Equal(HttpStatusCode.OK, responsecode);
//        }

//    }
//}