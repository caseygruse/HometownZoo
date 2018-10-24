using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HometownZoo.Models
{
    public static class AnimalService
    {
        /// <summary>
        /// returns all animals from the database 
        /// sorted by name in ascending order.
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static IEnumerable<Animal> GetAllAnimals(ApplicationDbContext db)
        {
            //query syntax
            return (from a in db.Animals orderby a.name select a).ToList();
            // method syntax
            //return db.Animals.ToList()
            //    .OrderBy(a => a.name)
            //    .ToList();
        }

        public static void AddAnimal(ApplicationDbContext db, Animal a)
        {
            if(a is null)
            {
                throw new ArgumentNullException($"the animal {nameof(a)} can not be null");
            }

            //TODO: Ensure duplicate names are disallowed.
            db.Animals.Add(a);

            db.SaveChanges();   
        }
    }
}