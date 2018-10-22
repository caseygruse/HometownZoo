using Microsoft.VisualStudio.TestTools.UnitTesting;
using HometownZoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Data.Entity;

namespace HometownZoo.Models.Tests
{
    [TestClass()]
    public class AnimalServiceTests
    {
        private IQueryable<Animal> animals;

        [TestInitialize] //runs before each test
        public void BeforeTest()
        {
            animals = new List<Animal>()
            {
                new Animal(){animalId=1, name="casey", age=5, sex="male", species="shark"},
                new Animal(){animalId=2, name="bob", age=6, sex="male", species="cow"},
                new Animal(){animalId=5, name="kamm", age=7, sex="female", species="pig"}
            }.AsQueryable();
            
        }

        [TestMethod]
        public void GetAnimals_ShouldReturnAllAnimalsSortedByName()
        {
            //Set up a mock database and mock animals table

            //Create a mock of Animals 
            var mockAnimals = new Mock<DbSet<Animal>>();
            mockAnimals.As<IQueryable<Animal>>().Setup(a => a.Provider).Returns(animals.Provider);

            mockAnimals.As<IQueryable<Animal>>().Setup(a => a.Expression).Returns(animals.Expression);

            mockAnimals.As<IQueryable<Animal>>().Setup(a => a.GetEnumerator()).Returns(animals.GetEnumerator());

            //Create mock database
            var mockDb = new Mock<ApplicationDbContext>();
            mockDb.Setup(db => db.Animals).Returns(mockAnimals.Object);



            //ACT
            IEnumerable<Animal> allAnimals = AnimalService.GetAllAnimals(mockDb.Object);

            //Assert
            Assert.AreEqual(3, allAnimals.Count());
        }  
    }
}