using PetShopCompulsuary.Core.Application.Services.ApplicationService;
using PetShopCompulsuary.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetShopCompulsuary.Petshop
{
    internal class Printer
    {
        private IPetService petService;

        internal Printer(IPetService petService)
        {
            this.petService = petService;

            WelcomeMessage();
            UserSelection();
        }

        public void WelcomeMessage()
        {
            PrintLine("Welcome To Abdils PetShop \n");
            PrintLine("Please select one of the following option(s) \n");
            OptionMessage();


        }

        public void UserSelection()
        {
            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 8 || selection > 1)
            {
                switch (selection)
                {
                    case 1:
                        PrntAllPets();
                        break;
                    case 2:
                        PrntPetsByType();
                        break;
                    case 3:
                        PrntOrderedByPrice();
                        break;
                    case 4:
                        PrntTopFiveCheapestPets();
                        break;
                    case 5:
                        CreatePet();
                        break;
                    case 6:
                        UpdatePet();
                        break;
                    case 7:
                        DeletePet();
                        break;
                    default:
                        PrintLine("\nInvalid number, try again \n");
                        break;
                }
                OptionMessage();
            }
        }



        public void PrntTopFiveCheapestPets()
        {

            foreach (var pet in petService.GetTopFiveCheapestPets())
            {
                PrntPetInfo(pet);
            }
        }

        public void PrntOrderedByPrice()
        {
            foreach (var pet in petService.GetPetsSortedPrice())
            {
                PrntPetInfo(pet);
            }
        }

        public void CreatePet()
        {
            Pet newPet = new Pet
            {
                PetName = AskQuestion("Name: "),
                PetColor = AskQuestion("Color: "),
                PetPrice = SetPetPrice(),
                PetType = GetPetTypeEnum(AskQuestion("Pet-type: ")),
                PetSoldDate = DateTime.Today,
                PetPreviousOwner = "None"
            };

            PrintLine("Enter the pet's birth date (yy/mm/dd)");
            DateTime dateTime;
            while (!DateTime.TryParse(Console.ReadLine(), out dateTime) || dateTime > DateTime.Today)
            {
                PrintLine("You did not select a valid birth date");
            }

            newPet.PetBirthDate = dateTime;
            petService.CreatePet(newPet);
        }


        public void DeletePet()
        {
            var id = Convert.ToInt32(AskQuestion("Please type the ID of the pet you want to delete"));
            bool petDeleted = petService.DeletePet(id);
            if (petDeleted)
            {
                PrintLine($"The desired pet has been successfully deleted with the ID: {id}");
            }
            else
            {
                PrintLine("Invalid ID | Please try again");
            }
        }

        public void UpdatePet()
        {
            PrintLine("Type the ID of the pet you want to update.");
            int selectedPetId;
            while (!int.TryParse(Console.ReadLine(), out selectedPetId))
            {
                PrintLine("Only type in a number");
            }

            Pet petToUpdate = petService.GetPetByID(selectedPetId);
            if (petToUpdate != null)
            {
                Pet newPet = new Pet
                {
                    PetID = petToUpdate.PetID,
                    PetName = AskQuestion("Name: "),
                    PetColor = AskQuestion("Color: "),
                    PetPrice = SetPetPrice(),
                    PetSoldDate = DateTime.Today,
                    PetPreviousOwner = AskQuestion("Previous owner: ")
                };
                petService.UpdatePet(newPet);
            }
            else
            {
                PrintLine("You selected an invalid ID, returning you to main-menu");
            }

        }

        public double SetPetPrice()
        {
            double price;
            PrintLine("Price: ");
            while (!double.TryParse(Console.ReadLine(), out price))
            {
                PrintLine("Only numbers allowed");
            }
            return price;
        }

        public void PrntAllPets()
        {
            foreach (var pet in petService.GetAllPets().ToList())
            {
                PrntPetInfo(pet);
            }
        }

        public void PrntPetsByType()
        {

            PrintLine("Which type of pet do you wish to search for?");
            PrntEnumsToString();

            List<Pet> petListType = petService.GetAllPetByType(GetPetTypeEnum(Console.ReadLine()));

            if (petListType.Count != 0)
            {
                foreach (var pet in petListType)
                {
                    PrntPetInfo(pet);
                };
            }
            else
            {
                PrintLine("No pets were found");
            }

        }

        public void PrntPetInfo(Pet pet)
        {
            PrintLine($"Pet-ID: {pet.PetID} Name: {pet.PetName} BirthDate: {pet.PetBirthDate.ToString("dd.MM.yyy")}" +
            $" Price: {pet.PetPrice} \n\nPet-Type: {pet.PetType} Color: {pet.PetColor} Sold-Date:" +
            $" {pet.PetSoldDate.ToString("dd.MM.yyy")} PreviousOwner: {pet.PetPreviousOwner} \n ----------------------------" +
            $"----------------------------------------------------- \n");
        }

        public void PrntEnumsToString()
        {
            var values = Enum.GetValues(typeof(MyEnum));
            foreach (var value in values)
            {
                PrintLine(value.ToString() + " ");
            }
            PrintLine("\n");
        }

        public MyEnum GetPetTypeEnum(string type)
        {
            string petType = type.ToLower();
            switch (type)
            {
                case "cat":
                    return MyEnum.Cat;
                case "dog":
                    return MyEnum.Dog;
                case "goat":
                    return MyEnum.Goat;
            }
            return MyEnum.Unknown;
        }

        public string AskQuestion(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        public void PrintLine(string line)
        {
            Console.WriteLine(line);
        }

        public void OptionMessage()
        {
            var optionCounter = 1;
            PrintLine($"{optionCounter++}: Show all pets");
            PrintLine($"{optionCounter++}: Search pets by specfic pet-type");
            PrintLine($"{optionCounter++}: Sort pets by price");
            PrintLine($"{optionCounter++}: Get the 5 cheapest pets");
            PrintLine($"{optionCounter++}: Create new pet");
            PrintLine($"{optionCounter++}: Update pet");
            PrintLine($"{optionCounter++}: Delete pet");

        }
    }
}
