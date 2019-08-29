using PetShopCompulsuary.Core.Application.Services.DomainService;
using PetShopCompulsuary.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShopCompulsuary.Infrastructure.Static.Data
{
    public class PetRepository : IPetRepository
    {
        FakeDB DB;

        public PetRepository()
        {
            DB = new FakeDB();
        }

        public void CreatePet(Pet pet)
        {
            pet.PetID = FakeDB.petId++;
            var petList = FakeDB.petList.ToList();
            petList.Add(pet);
            FakeDB.petList = petList;
        }

        public bool DeletePet(int id)
        {
            Pet pets = GetPetByID(id);
            if (pets != null)
            {
                var petList = FakeDB.petList.ToList();
                petList.Remove(pets);
                FakeDB.petList = petList;
                return true;
            }
            return false;
        }

        public void UpdatePet(Pet pets)
        {
            Pet PetToUpdate = GetPetByID(pets.PetID);
            PetToUpdate.PetName = pets.PetName;
            PetToUpdate.PetPreviousOwner = pets.PetPreviousOwner;
            PetToUpdate.PetPrice = pets.PetPrice;
            PetToUpdate.PetSoldDate = pets.PetSoldDate;
            PetToUpdate.PetColor = pets.PetColor;
        }

        public Pet GetPetByID(int petId)
        {
            foreach (var pet in FakeDB.petList.ToList())
            {
                if (pet.PetID == petId)
                {
                    return pet;
                }
            }
            return null;
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return FakeDB.petList;
        }
    }

}
