using GoogleKeepDB.Controllers;
using GoogleKeepDB.Models;
using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GoogleKeepDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace GoogleDBInMemoryTest
{
    public class KeepIntegrationTesting
    {
        private readonly KeepsController _controller;

        public KeepIntegrationTesting()
        {
            var optionsbuilder = new DbContextOptionsBuilder<KeepContext>();
            optionsbuilder.UseInMemoryDatabase("InMemoryDB");
            KeepContext context = new KeepContext(optionsbuilder.Options);
            _controller = new KeepsController(context);
            CreateTestData(context);

        }

        public void CreateTestData(KeepContext context)
        {
            List<Keep> keep1 = new List<Keep>()
            {
                new Keep
                {   title = "First",
                    plainText = "First sentence",
                    isPinned = true,
                    checklist = new List<Checklist>() { new Checklist { item = "hello" }, new Checklist { item = "brother" } },
                    label = "random" },
                new Keep
                {
                    title = "Second",
                    plainText = "second sentence",
                    isPinned = false,
                    checklist = new List<Checklist>() { new Checklist { item = "hey" }, new Checklist { item = "father" } },
                    label = "random2"
                } };
            context.AddRange(keep1);
            context.SaveChanges();

        }

        [Fact]
        public void TestGetAll()
        {
            var result = _controller.GetKeep();
            // var objectresult = result as OkObjectResult;
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void TestGetByID()
        {
            var result = _controller.GetKeepByID(2);
            var objectresult = result.Result as OkObjectResult;
            var keep = objectresult.Value as Keep;
            Assert.Equal("Second",  keep.title);
        }

        [Fact]
        public void TestGetbylabel()
        {
            var result = _controller.GetKeepByLabel("random");
            var objectresult = result.Result as OkObjectResult;
            List<Keep> a = objectresult.Value as List<Keep>;
            foreach(Keep p in a)
            {
                Assert.Equal("random", p.label);
            }
            
        }

        [Fact]
        public void TestGetbytitle()
        {
            var result = _controller.GetKeepByTitle("First");
            var objectresult = result.Result as OkObjectResult;
            List<Keep> a = objectresult.Value as List<Keep>;
            foreach (Keep p in a)
            {
                Assert.Equal("First", p.title);
            }

        }

        [Fact]
        public void TestGetbyPinned()
        {
            var result = _controller.GetKeepByIsPinned(false);
            var objectresult = result.Result as OkObjectResult;
            List<Keep> a = objectresult.Value as List<Keep>;
            foreach (Keep p in a)
            {
                Assert.Equal("Second", p.title);
            }
        }

        [Fact]
        public void Post()
        {
            Keep keep = new Keep()
            {
                title = "Third",
                plainText = "Third sentence",
                isPinned = true,
                checklist = new List<Checklist>() { new Checklist { item = "oye" }, new Checklist { item = "marja" } },
                label = "random3"
            };

            var result = _controller.PostKeep(keep);
            var objectresult = result.Result as OkObjectResult;
            Keep a = objectresult.Value as Keep;
                Assert.Equal("Third", a.title);
        }

        [Fact]
        public void Put()
        {
            Keep keep = new Keep()
            {
                keepID = 2,
                title = "First1",
                plainText = "first1 sentence",
                isPinned = true,
                checklist = new List<Checklist>() { new Checklist { item = "oye" }, new Checklist { item = "marja" } },
                label = "random1"
            };

            var result = _controller.PutKeepByID(2, keep);
            var objectresult = result.Result as OkObjectResult;
            var a = objectresult.Value as Keep;
            Assert.Equal("First1", a.title);
        }
    }
}
