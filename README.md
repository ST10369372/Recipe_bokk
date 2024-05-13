# Recipe_bokk

This C# code is a console application for managing recipes. Let's go through the code and understand its functionality:

### Classes:

1. **Ingredient**: Represents an ingredient in a recipe. It has properties like Name, Quantity, Unit, Calories, and FoodGroup.

2. **Recipe**: Represents a recipe. It contains a RecipeID, a list of Ingredients, a list of Steps, and a delegate for notifying calorie exceedance. It has methods for adding ingredients, adding steps, displaying the recipe, scaling the recipe, resetting ingredient quantities, clearing the recipe, calculating total calories, and checking for calorie exceedance.

3. **RecipeManager**: Manages a list of recipes. It provides methods for adding a recipe, displaying all recipes, and displaying a recipe by its name.

### Main Method (`Program.Main()`):

1. **Initialization**: It initializes a `RecipeManager` object and displays a welcome message.

2. **Menu Loop**: It presents a menu to the user with options to add a recipe, display all recipes, display a recipe by name, or exit the program.

3. **Option Handling**: Based on the user's input, it calls appropriate methods to perform the desired action.

### `AddRecipe()` Method:

1. **Prompting for Recipe Information**: It prompts the user to input information about the recipe, such as name, ingredients, and steps.

2. **Input Validation**: It ensures that the user provides valid inputs, such as non-empty recipe name, positive ingredient quantities, valid units, etc.

3. **Creating and Adding Recipe**: It creates a new `Recipe` object with the provided information and adds it to the `RecipeManager`.

4. **Displaying Total Calories**: It calculates and displays the total calories of the recipe, warning the user if it exceeds 300 calories.

### Supporting Methods:

- **DisplayUnits()**: Displays the available units for ingredients.
- **IsValidUnit()**: Checks if a unit provided by the user is valid.
- **DisplayRecipeByName()**: Prompts the user to input a recipe name and displays the recipe with that name.
- **IsValidRecipeName()**: Validates the format of the recipe name.

### How it Runs:

When you run the program, it displays a menu. You can choose to add a recipe, display all recipes, display a specific recipe, or exit the program. If you choose to add a recipe, it guides you through providing information about the recipe, such as ingredients and steps. After adding a recipe, it calculates and displays its total calories and warns if it exceeds 300 calories. You can then continue to interact with the program by selecting options from the menu until you choose to exit.
