// using System.Collections.Generic;
//
// namespace Crafting
// {
//     public sealed class Inventory
//     {
//         public int ItemCount => this.items.Count;
//
//         private readonly Dictionary<string, int> items;
//
//         public InventoryMock(Dictionary<string, int> items)
//         {
//             this.items = items;
//         }
//
//         public int GetCount(string name)
//         {
//             this.items.TryGetValue(name, out int count);
//             return count;
//         }
//
//         public bool Remove(string name, int removeCount)
//         {
//             if (this.GetCount(name) < removeCount)
//             {
//                 return false;
//             }
//
//             items[name] -= removeCount;
//             return true;
//         }
//
//         public void Add(string name, int count)
//         {
//             items.TryGetValue(name, out int resultCount);
//             items[name] = resultCount + count;
//         }
//     }
// }