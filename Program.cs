using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public delegate void RecipeCalorieNotification(string recipeName, double totalCalories);

class Ingredient
{
    public string Name { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; }
    public double Calories { get; set; }
    public string FoodGroup { get; set; }
}

class Recipe
{
    public string RecipeID { get; set; }
    public List<Ingredient> Ingredients { get; }
    public List<string> Steps { get; }
    public RecipeCalorieNotification NotifyCalorieExceedance { get; set; }

    public Recipe(string recipeID)
    {
        RecipeID = recipeID;
        Ingredients = new List<Ingredient>();
        Steps = new List<string>();
    }

    public void AddIngredient(string name, double quantity, string unit, double calories, string foodGroup)
    {
        Ingredients.Add(new Ingredient { Name = name, Quantity = quantity, Unit = unit, Calories = calories, FoodGroup = foodGroup });
    }

    public void AddStep(string description)
    {
        Steps.Add(description);
    }

    public void DisplayRecipe()
    {
        Console.WriteLine($"Recipe: {RecipeID}");
        Console.WriteLine("Ingredients:");
        foreach (var ingredient in Ingredients)
        {
            Console.WriteLine($"- {ingredient.Quantity} {GetAbbreviatedUnit(ingredient.Unit)} {ingredient.Name}");
        }
        Console.WriteLine("Steps:");
        for (int i = 0; i < Steps.Count; i++)
        {
            Console.WriteLine($"Step {i + 1}: {Steps[i]}");
        }
    }

    public void ScaleRecipe(double factor)
    {
        foreach (var ingredient in Ingredients)
        {
            ingredient.Quantity *= factor;
        }
    }

    public void ResetQuantities()
    {
        ScaleRecipe(1.0);
    }

    public void ClearRecipe()
    {
        Ingredients.Clear();
        Steps.Clear();
    }

    public double GetTotalQuantityOfIngredient(string ingredientName)
    {
        double totalQuantity = 0;
        foreach (var ingredient in Ingredients)
        {
            if (ingredient.Name.Equals(ingredientName, StringComparison.OrdinalIgnoreCase))
            {
                totalQuantity += ingredient.Quantity;
            }
        }
        return totalQuantity;
    }

    public double CalculateTotalCalories()
    {
        double totalCalories = 0;
        foreach (var ingredient in Ingredients)
        {
            totalCalories += ingredient.Calories;
        }
        return totalCalories;
    }

    private string GetAbbreviatedUnit(string unit)
    {
        switch (unit.ToLower())
        {
            case "grams":
                return "g";
            case "kilograms":
                return "kg";
            case "milliliters":
                return "ml";
            case "liters":
                return "l";
            case "teaspoon":
                return "tsp";
            case "cups":
                return "cup";
            case "whole":
                return "";
            default:
                return unit;
        }
    }

    public void CheckAndNotifyCalorieExceedance()
    {
        double totalCalories = CalculateTotalCalories();
        if (totalCalories > 300 && NotifyCalorieExceedance != null)
        {
            NotifyCalorieExceedance(RecipeID, totalCalories);
        }
    }
}

class RecipeManager
{
    private List<Recipe> recipes;

    public RecipeManager()
    {
        recipes = new List<Recipe>();
    }

    public void AddRecipe(Recipe recipe)
    {
        recipes.Add(recipe);
    }

    public void DisplayAllRecipes()
    {
        Console.WriteLine("Recipes:");
        foreach (var recipe in recipes)
        {
            Console.WriteLine($"- {recipe.RecipeID}");
        }
    }

    public void DisplayRecipeByName(string recipeName)
    {
        Recipe recipe = recipes.Find(r => r.RecipeID.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
        if (recipe != null)
        {
            recipe.DisplayRecipe();
        }
        else
        {
            Console.WriteLine($"Recipe '{recipeName}' not found.");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the Recipe Application!");

        RecipeManager recipeManager = new RecipeManager();

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add Recipe");
            Console.WriteLine("2. Display All Recipes");
            Console.WriteLine("3. Display Recipe by Name");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddRecipe(recipeManager);
                    break;
                case "2":
                    recipeManager.DisplayAllRecipes();
                    break;
                case "3":
                    DisplayRecipeByName(recipeManager);
                    break;
                case "4":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number from 1 to 4.");
                    break;
            }
        }
    }

    static void AddRecipe(RecipeManager recipeManager)
    {
        Console.WriteLine("Adding Recipe:");

        string recipeName;
        do
        {
            Console.Write("Enter recipe name (letters only): ");
            recipeName = Console.ReadLine().Trim();
        } while (!IsValidRecipeName(recipeName));

        Recipe recipe = new Recipe(recipeName);

        int numIngredients;
        do
        {
            Console.Write("Enter the number of ingredients: ");
        } while (!int.TryParse(Console.ReadLine(), out numIngredients) || numIngredients <= 0);

        for (int i = 0; i < numIngredients; i++)
        {
            Console.Write($"Ingredient {i + 1} name: ");
            string ingredientName = Console.ReadLine();

            double ingredientQuantity;
            do
            {
                Console.Write("Quantity: ");
            } while (!double.TryParse(Console.ReadLine(), out ingredientQuantity) || ingredientQuantity <= 0);

            string ingredientUnit;
            do
            {
                Console.WriteLine("Available units:");
                DisplayUnits();
                Console.Write("Select unit: ");
                ingredientUnit = Console.ReadLine();
            } while (!IsValidUnit(ingredientUnit));

            double ingredientCalories;
            do
            {
                Console.Write("Calories: ");
            } while (!double.TryParse(Console.ReadLine(), out ingredientCalories) || ingredientCalories <= 0);

            string foodGroup;
            do
            {
                Console.WriteLine("Available food groups:");
                Console.WriteLine("1. Dairy");
                Console.WriteLine("2. Protein");
                Console.WriteLine("3. Fruits");
                Console.WriteLine("4. Grains");
                Console.WriteLine("5. Vegetables");
                Console.Write("Select food group (enter number): ");
                string foodGroupChoice = Console.ReadLine();

                switch (foodGroupChoice)
                {
                    case "1":
                        foodGroup = "Dairy";
                        break;
                    case "2":
                        foodGroup = "Protein";
                        break;
                    case "3":
                        foodGroup = "Fruits";
                        break;
                    case "4":
                        foodGroup = "Grains";
                        break;
                    case "5":
                        foodGroup = "Vegetables";
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number from 1 to 5.");
                        foodGroup = null;
                        break;
                }
            } while (foodGroup == null);

            recipe.AddIngredient(ingredientName, ingredientQuantity, ingredientUnit, ingredientCalories, foodGroup);
        }

        int numSteps;
        do
        {
            Console.Write("Enter the number of steps: ");
        } while (!int.TryParse(Console.ReadLine(), out numSteps) || numSteps <= 0);

        for (int i = 0; i < numSteps; i++)
        {
            Console.Write($"Step {i + 1}: ");
            string stepDescription = Console.ReadLine();
            recipe.AddStep(stepDescription);
        }

        recipeManager.AddRecipe(recipe);

        double totalCalories = recipe.CalculateTotalCalories();
        Console.WriteLine($"Total Calories: {totalCalories}");
        if (totalCalories > 300)
        {
            Console.WriteLine("Warning: Total calories exceed 300!");
        }
    }

    static void DisplayUnits()
    {
        Console.WriteLine("- g");
        Console.WriteLine("- kg");
        Console.WriteLine("- ml");
        Console.WriteLine("- l");
        Console.WriteLine("- tsp");
        Console.WriteLine("- cups");
        Console.WriteLine("- whole");
    }

    static bool IsValidUnit(string unit)
    {
        List<string> validUnits = new List<string> { "g", "kg", "ml", "l", "tsp", "cups", "whole" };
        return validUnits.Contains(unit.ToLower());
    }

    static void DisplayRecipeByName(RecipeManager recipeManager)
    {
        Console.Write("Enter the name of the recipe to display: ");
        string selectedRecipeName = Console.ReadLine();
        recipeManager.DisplayRecipeByName(selectedRecipeName);
    }

    static bool IsValidRecipeName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @"^[a-zA-Z]+$");
    }
}

