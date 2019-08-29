﻿using PetShopCompulsuary.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopCompulsuary.Core.Application.Services.ApplicationService
{
    public interface IPetService
    {
        IEnumerable<Pet> GetAllPets();
        bool DeletePet(int petId);
        void CreatePet(Pet pet);
        void UpdatePet(Pet pet);
        Pet GetPetByID(int petId);
        List<Pet> GetAllPetByType(MyEnum enumType);
        List<Pet> GetPetsSortedPrice();
        List<Pet> GetTopFiveCheapestPets();
    }
}
