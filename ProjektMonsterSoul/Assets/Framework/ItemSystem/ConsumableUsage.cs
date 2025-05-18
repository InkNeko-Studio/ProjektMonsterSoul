using Framework.SaveSystem;
using UnityEngine;

namespace Framework.ItemSystem
{
    public static class ConsumableUsage
    {
        public static void Use(ConsumableId id)
        {
            switch (id)
            {
                case ConsumableId.RedPotion: UseRedPotion(); break;
                case ConsumableId.GreenPotion: UseGreenPotion(); break;
            }
        }

        private static void UseRedPotion()
        {
            SaveController.CurrentSave.playerData.life = Mathf.Clamp(SaveController.CurrentSave.playerData.life + 50, 0, SaveController.CurrentSave.playerData.maxLife);
        }

        private static void UseGreenPotion()
        {
            SaveController.CurrentSave.playerData.life = Mathf.Clamp(SaveController.CurrentSave.playerData.life + 100, 0, SaveController.CurrentSave.playerData.maxLife);
        }
    }
}