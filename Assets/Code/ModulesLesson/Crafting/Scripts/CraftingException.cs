using System;

namespace Crafting
{
    public sealed class CraftingException : Exception
    {
        public CraftingException(string message) : base(message)
        {
        }
    }
}