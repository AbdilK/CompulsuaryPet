﻿using PetShopCompulsuary.Core.Application.Services.DomainService;
using PetShopCompulsuary.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShopCompulsuary.Core.Application.Services.ApplicationService.DomainService
{
    public class PetService : IPetService
    {
        private IPetRepository petRepository;

        public PetService(IPetRepository petRepository)
        {
            this.petRepository = petRepository;

        }

        public List<Pet> GetAllPetByType(MyEnum enumType)
        {
            List<Pet> petSpecificType = GetAllPets().Where(pet => pet.PetType == enumType).ToList();
            return petSpecificType;
        }

        public List<Pet> GetPetsSortedPrice()
        {
            List<Pet> petSpecificType = GetAllPets().OrderBy(pet => pet.PetPrice).ToList();
            return petSpecificType;
        }

        public List<Pet> GetTopFiveCheapestPets()
        {
            int numberOfPets = 0;
            if (GetAllPets().ToList().Count >= 5)
            {
                numberOfPets = 5;
            }
            else
            {
                numberOfPets = GetAllPets().Count();
            }
            List<Pet> FiveCheapestPets = GetPetsSortedPrice().Take(numberOfPets).ToList();
            return FiveCheapestPets;
        }

        public void CreatePet(Pet pet)
        {
            petRepository.CreatePet(pet);
        }

        public bool DeletePet(int petId)
        {
            return petRepository.DeletePet(petId);
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return petRepository.GetAllPets();
        }

        public Pet GetPetByID(int petId)
        {
            return petRepository.GetPetByID(petId);
        }

        public void UpdatePet(Pet pet)
        {
            petRepository.UpdatePet(pet);
        }
    }
}
