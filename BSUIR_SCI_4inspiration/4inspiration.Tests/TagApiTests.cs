using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AppCore;
using Domain;
//using NUnit.Framework;
using MvcApplication.Models.WebAPIModels;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;


namespace _4inspiration.Tests
{
    //[TestFixture]
    [TestClass]
    public class WebApiTests
    {
        [TestMethod]
        public void GetAllTags()
        {
            // Arrange 
            var controller = new MvcApplication.Controllers.TagWebAPIController();
            // Act
            var all_tags_we_got = controller.GetAllTags();
            // Assert
            Assert.IsNotNull(all_tags_we_got, "Result is null");
            Assert.IsNotInstanceOfType(all_tags_we_got, typeof(JsonResult), "Wrong type");
        }

        [TestMethod]
        public void GetTagByID()
        {
            // Arrange 
            var controller = new MvcApplication.Controllers.TagWebAPIController();
            // Act
            var tag_we_got = controller.GetTagByID(3);
            // Assert
            Assert.IsNotNull(tag_we_got, "Result is null");
            Assert.IsNotInstanceOfType(tag_we_got, typeof(JsonResult), "Wrong type");
        }

        [TestMethod]
        public void DontCreateTagThatExists()
        {
            // Arrange 
            var controller = new MvcApplication.Controllers.TagWebAPIController() 
            {
                Request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:54134/api/tags/")
            };
            var tag2add = new TagModel() { ID = 0, Name = "fsfs"};
            // Act
            var response = controller.PostTag(tag2add);
            // Assert
            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
        }

        [TestMethod]
        public void DontCreateTagWithEmptyName()
        {
            // Arrange 
            var controller = new MvcApplication.Controllers.TagWebAPIController()
            {
                Request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:54134/api/tags/")
            };
            var tag2add = new TagModel();
            controller.ModelState.AddModelError("", "empty name");
            // Act
            var response = controller.PostTag(tag2add);
            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void CreateTag()
        {
            var httpConfiguration = new HttpConfiguration();
            MvcApplication.WebApiConfig.Register(httpConfiguration);
            var httpRouteData = new HttpRouteData(httpConfiguration.Routes["TagApi"],
                new HttpRouteValueDictionary { { "controller", "id" } });
            // Arrange 
            var controller = new MvcApplication.Controllers.TagWebAPIController()
            {
                Request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:54134/api/tags/")
                {
                    Properties =
                    {
                        { HttpPropertyKeys.HttpConfigurationKey, httpConfiguration },
                        { HttpPropertyKeys.HttpRouteDataKey, httpRouteData }
                    }
                }
            };
            var tag2add = new TagModel() { ID = 0, Name = "testaaaa" };
            // Act
            var response = controller.PostTag(tag2add);
            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode,"There is a such one in the db");
        }

        [TestMethod]
        public void DeleteTagByID()
        {
            var httpConfiguration = new HttpConfiguration();
            MvcApplication.WebApiConfig.Register(httpConfiguration);
            var httpRouteData = new HttpRouteData(httpConfiguration.Routes["TagApi"],
                new HttpRouteValueDictionary { { "controller", "id" } });
            // Arrange 
            var controller = new MvcApplication.Controllers.TagWebAPIController()
            {
                Request = new HttpRequestMessage(HttpMethod.Delete, "http://localhost:54134/api/tags/")
                {
                    Properties =
                    {
                        { HttpPropertyKeys.HttpConfigurationKey, httpConfiguration },
                        { HttpPropertyKeys.HttpRouteDataKey, httpRouteData }
                    }
                }
            };
            // Act
            var response = controller.DeleteTag(10);
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "No item with such id");
          
        }

        [TestMethod]
        public void PutTagByID()
        {
            var httpConfiguration = new HttpConfiguration();
            MvcApplication.WebApiConfig.Register(httpConfiguration);
            var httpRouteData = new HttpRouteData(httpConfiguration.Routes["TagApi"],
                new HttpRouteValueDictionary { { "controller", "id" } });
            // Arrange 
            var controller = new MvcApplication.Controllers.TagWebAPIController()
            {
                Request = new HttpRequestMessage(HttpMethod.Put, "http://localhost:54134/api/tags/")
                {
                    Properties =
                    {
                        { HttpPropertyKeys.HttpConfigurationKey, httpConfiguration },
                        { HttpPropertyKeys.HttpRouteDataKey, httpRouteData }
                    }
                }
            };
            var tag2update = new TagModel() { ID = 0, Name = "updated"};
            // Act
            var response = controller.PutTag(5, tag2update);
            // Assert

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "No item with such id");
        }
    }
}
