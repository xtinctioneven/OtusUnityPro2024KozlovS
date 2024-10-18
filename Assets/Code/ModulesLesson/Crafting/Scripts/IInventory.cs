namespace Crafting
{
    public interface IInventory
    {
        int ItemCount { get; }
        
        int GetCount(string name);
        bool Remove(string name, int removeCount);
        void Add(string name, int count);
    }
}