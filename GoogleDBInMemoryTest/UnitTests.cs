//using GoogleKeepDB.Controllers;
//using System;
//using Xunit;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Mvc;
//using GoogleKeepDB.Model;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace GoogleDBInMemoryTest
//{
//    public class KeepUnitTesting
//    {


//        public KeepsController GetController()
//        {
//            var optionsbuilder = new DbContextOptionsBuilder<KeepContext>();
//            optionsbuilder.UseInMemoryDatabase<KeepContext>(Guid.NewGuid().ToString());
//            KeepContext context = new KeepContext(optionsbuilder.Options);
//            CreateTestData(optionsbuilder.Options);
//            return new KeepsController(context);

//        }

//        public void CreateTestData(DbContextOptions<KeepContext> options)
//        {
//            using (var context = new KeepContext(options))
//            {
//                List<Keep> keepList = new List<Keep>
//                {
//                    new Keep()
//                    {   keepID = 1,
//                        title = "First",
//                        plainText = "First sentence",
//                        isPinned = true,
//                        checklist = new List<Checklist>() { new Checklist { item = "hello" }, new Checklist { item = "brother" } },
//                        label = "random" },
//                    new Keep()
//                    {
//                        keepID = 2,
//                        title = "Second",
//                        plainText = "second sentence",
//                        isPinned = false,
//                        checklist = new List<Checklist>() { new Checklist { item = "hey" }, new Checklist { item = "father" } },
//                        label = "random2"
//                    }
//                };
//                context.Keep.AddRange(keepList);
//                context.SaveChanges();
//            };
//        }

//        [Fact]
//        public async Task TestGetAll()
//        {
//            var _controller = GetController();
//            var result = await _controller.GetKeep();
//            // var objectresult = result as OkObjectResult;
//            Assert.Equal(2, result.Count());
//        }

//        [Fact]
//        public async Task TestGetByID()
//        {
//            var _controller = GetController();
//            var result = await _controller.GetKeepByID(1);
//            var objectresult = result as OkObjectResult;
//            var keep = objectresult.Value as Keep;
//            Assert.Equal("First", keep.title);
//        }

//        [Fact]
//        public void TestGetbylabel()
//        {
//            var _controller = GetController();
//            var result = _controller.GetKeepByLabel("random");
//            var objectresult = result.Result as OkObjectResult;
//            List<Keep> a = objectresult.Value as List<Keep>;
//            foreach (Keep p in a)
//            {
//                Assert.Equal("random", p.label);
//            }

//        }

//        [Fact]
//        public void TestGetbytitle()
//        {
//            var _controller = GetController();
//            var result = _controller.GetKeepByTitle("First");
//            var objectresult = result.Result as OkObjectResult;
//            List<Keep> a = objectresult.Value as List<Keep>;
//            foreach (Keep p in a)
//            {
//                Assert.Equal("First", p.title);
//            }

//        }

//        [Fact]
//        public void TestGetbyPinned()
//        {
//            var _controller = GetController();
//            var result = _controller.GetKeepByIsPinned(false);
//            var objectresult = result.Result as OkObjectResult;
//            List<Keep> a = objectresult.Value as List<Keep>;
//            foreach (Keep p in a)
//            {
//                Assert.Equal("Second", p.title);
//            }
//        }

//        [Fact]
//        public void TestPost()
//        {
//            Keep keep = new Keep()
//            {
//                keepID = 3,
//                title = "Third",
//                plainText = "Third sentence",
//                isPinned = true,
//                checklist = new List<Checklist>() { new Checklist { item = "oye" }, new Checklist { item = "marja" } },
//                label = "random3"
//            };

//            var _controller = GetController();
//            var result = _controller.PostKeep(keep);
//            var objectresult = result.Result as OkObjectResult;
//            Keep a = objectresult.Value as Keep;
//            Assert.Equal("Third", a.title);
//        }

//        [Fact]
//        public async Task TestPut()
//        {
//            Keep keep = new Keep()
//            {
//                keepID = 1,
//                title = "First1",
//                plainText = "first1 sentence",
//                isPinned = true,
//                checklist = new List<Checklist>() { new Checklist { item = "oye" }, new Checklist { item = "marja" } },
//                label = "random1"
//            };

//            var _controller = GetController();
//            var result = await _controller.PutKeepByID(1, keep);
//            var objectresult = result as OkObjectResult;
//            var a = objectresult.Value as Keep;
//            Assert.Equal("First1", a.title);
//        }

//        [Fact]
//        public void TestDelete()
//        {
//            var _controller = GetController();
//            var result = _controller.DeleteKeep(2);
//            Assert.True(result.IsCompletedSuccessfully);
//        }
//    }
//}
