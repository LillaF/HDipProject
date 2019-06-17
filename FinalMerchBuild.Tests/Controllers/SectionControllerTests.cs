using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using FinalMerchBuild.Controllers;
using FinalMerchBuild.DAL;
using FinalMerchBuild.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Collections.Specialized.BitVector32;

namespace FinalMerchBuild.Tests.Controllers
{
    [TestClass]
    public class SectionControllerTests
    {


        [TestMethod]
        public void DeleteFailsIfNullID()
        {
            var controller = new SectionController();
            var expected = (int)System.Net.HttpStatusCode.BadRequest;

            var badresult = controller.Delete(null) as HttpStatusCodeResult;

            Assert.AreEqual(expected, badresult.StatusCode);
        }


        [TestMethod()]
        public void CanValidateSection()
        {
            FinalMerchBuild.Models.Section newSection = new FinalMerchBuild.Models.Section
            {
                SectionName = "Test Section Name is way  50 characters",
                Height = 3,
                Width = 6,
                Depth = 30
            };
            var context = new ValidationContext(newSection, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(newSection, context, result, true);

            Assert.IsTrue(valid);
        }


        [TestMethod()]
        public void DoesNotValidateSectionNameTooLong()
        {
            // Arrange
            FinalMerchBuild.Models.Section newSection = new FinalMerchBuild.Models.Section
            {
                SectionName = "Test Section than 50 characters",
                Height = 3,
                Width = 6,
                Depth = 20
            };
            var context = new ValidationContext(newSection, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(newSection, context, result, true);

            Assert.IsFalse(valid);
            Assert.AreEqual(result.First().ErrorMessage, "Section Name is too long");
        }

    }


}
