using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HometownZoo;
using HometownZoo.Controllers;

namespace HometownZoo.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_ReturnsNonNullViewResult()
        {
            //Arrange       // create the controller to test
            HomeController home = new HomeController();
            //Act        // 
            ViewResult result = home.Index() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About_ReturnsNonNullViewResult()
        {
            //Arrange
            HomeController home = new HomeController();
            //Act        // if this ever dosnt work than Assert will be null.
            ViewResult result = home.About() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About_ViewBagShouldHaveNonNullViewBagMessage()
        {
            //Arrange
            HomeController home = new HomeController();
            //Act
            ViewResult result = home.About() as ViewResult;
            //Assert
            Assert.IsNotNull(result.ViewBag.Message);

            //checks for exact message.
            //Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact_ReturnsNonNullViewResult()
        {
            HomeController home = new HomeController();

            ViewResult result = home.Contact() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
