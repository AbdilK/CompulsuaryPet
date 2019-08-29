using PetShopCompulsuary.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetShopCompulsuary.Infrastructure.Static.Data
{
    internal class FakeDB
    {
        internal static int petId;
        internal static IEnumerable<Pet> petList;

        internal FakeDB()
        {
            petList = Enumerable.Empty<Pet>();
            petId = 1;
            RandomizedPets();
        }

        internal MyEnum GetEnumType(int index)
        {
            switch (index)
            {
                case 0:
                    return MyEnum.Dog;
                case 1:
                    return MyEnum.Cat;
                case 2:
                    return MyEnum.Goat;
                default:
                    break;
            }
            return MyEnum.Unknown;
        }

        internal void RandomizedPets()
        {
            Random r = new Random();

            string[] fakeNames = { "Molly", "Maggie", "Max", "Charlie", "Buster" };

            var petList = FakeDB.petList.ToList();

            for (int i = 0; i < 10; i++)
            {
                Pet pet = new Pet
                {
                    PetID = petId++,
                    PetName = fakeNames[r.Next(0, 4)],
                    PetType = GetEnumType(r.Next(0, 3)),
                    PetPrice = r.NextDouble(),
                    PetPreviousOwner = "None",
                    PetBirthDate = new DateTime(2019, 1, 01),
                    PetSoldDate = DateTime.Today,
                };
                petList.Add(pet);
            }
            FakeDB.petList = petList;
        }



    }
}
