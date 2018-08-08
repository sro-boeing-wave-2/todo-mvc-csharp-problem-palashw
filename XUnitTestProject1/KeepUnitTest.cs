using System;
using Xunit;
using NSuperTest;
using GoogleKeepDB.Model;

namespace GoogleDBNSuperTest
{
    public class KeepUnitTest
    {
        public static Server server = new Server("https://localhost:44379/api/keep/");
        
        // test for get all
        [Fact]
        public void GetTest()
        {
            server.Get("")
                  .Expect(200)
                  .End();
        }

        // test for get by id
        [Fact]
        public void GetTestByID()
        {
            server.Get("1")
                  .Expect(200)
                  .End();
        }

        // test for get by title
        [Fact]
        public void GetTestByTitle()
        {
            server.Get("title/Sweets")
                  .Expect(200)
                  .End();
        }

        // test for get by label
        [Fact]
        public void GetTestByLabel()
        {
            server.Get("label/Art")
                  .Expect(200)
                  .End();
        }

        [Fact]
        public void GetTestByPinned()
        {
            server.Get("ispinned/true")
                  .Expect(200)
                  .End();
        }

        // test for post
        [Fact]
        public void Post()
        {
            var keep = new
            {
                title = "Pubs",
                plainText = "List of pubs to go",
                // checklist = { new Checklist { item = "p1" }, new Checklist { item = "p2" }, new Checklist { item = "p3" } },
                label = "Social",
                isPinned = true
            };

            server
              .Post("")
              .Send(keep)
              .Expect(201)
              .End();
        }

        // test for put
        [Fact]
        public void Put()
        {
            var keep1 = new
            {
                keepID = 7003,
                title = "Pubs2",
                plainText = "List of pubs to go",
                // checklist = { new Checklist { item = "p1" }, new Checklist { item = "p2" }, new Checklist { item = "p3" } },
                label = "Social",
                isPinned = true
            };

            server
              .Put("7003")
              .Send(keep1)
              .Expect(200)
              .End();
        }

        // test for delete
        [Fact]
        public void delete()
        {
            server
              .Delete("9004")
              .Expect(200)
              .End();
        }
    }
}
