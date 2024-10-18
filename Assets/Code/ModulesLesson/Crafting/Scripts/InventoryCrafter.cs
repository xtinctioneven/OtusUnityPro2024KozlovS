namespace Crafting
{
    public sealed class InventoryCrafter
    {
        private readonly IInventory inventory;
        
        public InventoryCrafter(IInventory inventory)
        {
            this.inventory = inventory ?? throw new CraftingException("Inventory is null");
        }

        public bool Craft(in CraftingReceipt receipt)
        {
            CraftingIngredient[] ingredients = receipt.ingredients;
            int ingredientCount = ingredients.Length;
            
            for (int i = 0; i < ingredientCount; i++)
            {
                CraftingIngredient ingredient = ingredients[i];
                if (inventory.GetCount(ingredient.name) < ingredient.count)
                {
                    return false;
                }
            }

            for (int i = 0; i < ingredientCount; i++)
            {
                
                CraftingIngredient ingredient = ingredients[i];
                inventory.Remove(ingredient.name, ingredient.count);
                // inventory[ingredient.name] -= ingredient.count;
            }

            string resultName = receipt.resultName;
            inventory.Add(resultName, receipt.resultCount);
            return true;
        }
    }
}

//
// inventory.TryGetValue("Wood", out int wood);
// inventory.TryGetValue("Stone", out int stone);
//
// if (wood >= 2 && stone >= 1)
// {
//     inventory["Wood"] -= 2;
//     inventory["Stone"] -= 1;
//
//     inventory.TryGetValue("Axe", out int axe);
//     axe += 1;
//     inventory["Axe"] = axe;
//     return true;
// }
//
// return false;