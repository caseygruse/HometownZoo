using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Data.Entity;
using System;

namespace HometownZoo.Models.Tests
{
    [TestClass()]
    public class AnimalServiceTests
    {
        private IQueryable<Animal> animals;

        /// <summary>
        /// TestInitialize runs before every method in this class.
        /// creates a db set (animals)
        /// </summary>
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

        /// <summary>
        /// // Set up a mock database and mock animals table
        /// </summary>
        /// <returns></returns>
        private Mock<DbSet<Animal>> SetUpMockDb()
        {
            var mockAnimals = new Mock<DbSet<Animal>>();
            mockAnimals.As<IQueryable<Animal>>().Setup(a => a.Provider).Returns(animals.Provider);

            mockAnimals.As<IQueryable<Animal>>().Setup(a => a.Expression).Returns(animals.Expression);

            mockAnimals.As<IQueryable<Animal>>().Setup(a => a.GetEnumerator()).Returns(animals.GetEnumerator());
            return mockAnimals;
        }


        /// <summary>
        /// tests getAllAnimals
        /// uses moq db set.
        /// </summary>
        [TestMethod]
        public void GetAnimals_ShouldReturnAllAnimalsSortedByName()
        {
            //Create a mock of Animals 
            Mock<DbSet<Animal>> mockAnimals = SetUpMockDb();

            //Create mock database
            var mockDb = GetMockDB(mockAnimals);

            //ACT
            IEnumerable<Animal> allAnimals = AnimalService.GetAllAnimals(mockDb.Object);

            //Assert all animals are returned
            Assert.AreEqual(3, allAnimals.Count());

            //Assert animals are sorted by name (Ascending)
            Assert.AreEqual("bob", allAnimals.ElementAt(0).name);
        }

        [TestMethod]
        public void AddAnimal_NewAnimalShouldCallAddAndSaveChanges()
        {
            //Arrange
            Mock<DbSet<Animal>> mockAnimals = SetUpMockDb();
            Mock<ApplicationDbContext> mockDb = GetMockDB(mockAnimals);

            Animal a = new Animal()
            {
                animalId = 6,
                age = 4,
                name = "elly",
                sex = "female",
                species = "elephant"
            };
            //Act
            AnimalService.AddAnimal(mockDb.Object, a);

            //Assert
            mockAnimals.Verify(m => m.Add(a), Times.Once);

            mockDb.Verify(m => m.SaveChanges());

        }

        /// <summary>
        /// returns mock Db
        /// </summary>
        /// <param name="mockAnimals"></param>
        /// <returns></returns>
        private static Mock<ApplicationDbContext> GetMockDB(Mock<DbSet<Animal>> mockAnimals)
        {
            var mockDb = new Mock<ApplicationDbContext>();
            mockDb.Setup(db => db.Animals).Returns(mockAnimals.Object);
            return mockDb;
        }

        [TestMethod]
        public void AddAnimal_NullAnimal_ShouldThrowNullArguemntException()
        {
            //Arrange
            Animal a = null;

            var mockDb = GetMockDB(SetUpMockDb());
            //Assert => Act
            Assert.ThrowsException<ArgumentNullException>(() => AnimalService.AddAnimal(mockDb.Object , a));
        }
    }
}