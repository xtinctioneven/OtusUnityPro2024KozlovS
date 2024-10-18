using System.Collections.Generic;
using NUnit.Framework;
// ReSharper disable UnusedVariable

namespace Crafting
{
    [TestFixture]
    public sealed class CraftingTests
    {
        private static readonly CraftingReceipt AXE_RECEIPT = new("Axe", 1,
            new CraftingIngredient("Wood", 2),
            new CraftingIngredient("Stone", 1));

        private static readonly CraftingReceipt SWORD_RECEIPT = new("Sword", 1,
            new CraftingIngredient("Wood", 1),
            new CraftingIngredient("Gold", 1),
            new CraftingIngredient("Stone", 1));

        [Test]
        public void AxeCrafting()
        {
            //Arrange:
            var inventory = new InventoryMock(new Dictionary<string, int>
            {
                {"Wood", 5},
                {"Stone", 5}
                //{Axe, 1}
            });
            var inventoryCrafter = new InventoryCrafter(inventory);

            //Act:
            bool success = inventoryCrafter.Craft(AXE_RECEIPT);

            //Assert:
            Assert.IsTrue(success);
            Assert.AreEqual(3, inventory.GetCount("Wood"));
            Assert.AreEqual(4, inventory.GetCount("Stone"));
            Assert.AreEqual(1, inventory.GetCount("Axe"));
        }

        [Test]
        public void SwordCrafting() //1, 1, 1
        {
            //Arrange:
            var inventory = new InventoryMock(new Dictionary<string, int>
            {
                {"Wood", 5},
                {"Stone", 5},
                {"Gold", 5},
            });

            //Act:
            var inventoryCrafter = new InventoryCrafter(inventory);

            //Act:
            bool success = inventoryCrafter.Craft(SWORD_RECEIPT);

            //Assert:
            Assert.IsTrue(success);
            Assert.AreEqual(4, inventory.GetCount("Wood"));
            Assert.AreEqual(4, inventory.GetCount("Stone"));
            Assert.AreEqual(4, inventory.GetCount("Gold"));
            Assert.AreEqual(1, inventory.GetCount("Sword"));
        }

        [Test]
        public void WhenInventoryIsNullThenThrowsExeption()
        {
            //Act:
            Assert.Catch<CraftingException>(() =>
            {
                InventoryCrafter crafter = new InventoryCrafter(null);
            }, "Inventory is null");
        }

        [Test]
        public void WhenResourcesNotEnoughThenFailed()
        {
            //Arrange:
            var inventory = new InventoryMock(new Dictionary<string, int>
            {
                {"Wood", 1},
                {"Stone", 1}
                //{Axe, 1}
            });

            //Act:
            var inventoryCrafter = new InventoryCrafter(inventory);

            //Act:
            bool success = inventoryCrafter.Craft(AXE_RECEIPT);
            
            //Assert:
            Assert.IsFalse(success);

            Assert.AreEqual(1, inventory.GetCount("Wood"));
            Assert.AreEqual(1, inventory.GetCount("Stone"));
            Assert.AreEqual(0, inventory.GetCount("Axe"));
        }

        [Test]
        public void WhenResourcesSameThatRequiredThenSuccess()
        {
            //Arrange:
            var inventory = new InventoryMock(new Dictionary<string, int>
            {
                {"Wood", 1},
                {"Stone", 1},
                {"Gold", 1},
            });
            
            var inventoryCrafter = new InventoryCrafter(inventory);

            //Act:
            bool success = inventoryCrafter.Craft(SWORD_RECEIPT);

            //Assert:
            Assert.IsTrue(success);
            Assert.AreEqual(0, inventory.GetCount("Wood"));
            Assert.AreEqual(0, inventory.GetCount("Stone"));
            Assert.AreEqual(0, inventory.GetCount("Gold"));
            Assert.AreEqual(1, inventory.GetCount("Sword"));
        }

        [Test]
        public void WhenResourcesIsAbsentThenCraftingFailed()
        {
            //Arrange:
            var inventory = new InventoryMock(new Dictionary<string, int>());
            var inventoryCrafter = new InventoryCrafter(inventory);

            //Act:
            bool success = inventoryCrafter.Craft(AXE_RECEIPT);

            //Assert:
            Assert.IsFalse(success);
            Assert.AreEqual(0, inventory.ItemCount);
            Assert.AreEqual(0 ,inventory.GetCount("Axe"));
            Assert.AreEqual(0 ,inventory.GetCount("Wood"));
            Assert.AreEqual(0, inventory.GetCount("Stone"));
        }

        [Test]
        public void WhenAxeInInvetoryExistsThenCraftingSuccess()
        {
            //Arrange:
            //Arrange:
            var inventory = new InventoryMock(new Dictionary<string, int>
            {
                {"Wood", 5},
                {"Stone", 5},
                {"Axe", 1}
            });
            var inventoryCrafter = new InventoryCrafter(inventory);

            //Act:
            
            bool success = inventoryCrafter.Craft(AXE_RECEIPT);

            //Assert:
            Assert.IsTrue(success);

            Assert.AreEqual(3, inventory.ItemCount);

            Assert.AreEqual(2, inventory.GetCount("Axe"));
            Assert.AreEqual(3, inventory.GetCount("Wood"));
            Assert.AreEqual(4, inventory.GetCount("Stone"));
        }
    }
}