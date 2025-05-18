using System;
using Framework.ItemSystem;

namespace Framework.SaveSystem.Data
{
    [Serializable]
    public class ConsumableData
    {
        public ConsumableId consumableId;
        public int quantity;
    }
}