namespace Crafting
{
    public readonly struct CraftingIngredient
    {
        public readonly string name;
        public readonly int count;

        public CraftingIngredient(string name, int count)
        {
            this.name = name;
            this.count = count;
        }
    }
}