namespace Crafting
{
    public readonly struct CraftingReceipt
    {
        public readonly string resultName;
        public readonly int resultCount;
        public readonly CraftingIngredient[] ingredients;

        public int IngredientCount => this.ingredients.Length;

        public CraftingIngredient GetIngredient(int index)
        {
            return this.ingredients[index];
        }
        
        public CraftingReceipt(string resultName, int resultCount, params CraftingIngredient[] ingredients)
        {
            this.resultName = resultName;
            this.resultCount = resultCount;
            this.ingredients = ingredients;
        }
    }
}