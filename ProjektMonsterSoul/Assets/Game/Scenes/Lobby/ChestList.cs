using System;
using System.Collections;
using Framework.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scenes.Lobby
{
    public class ChestList : MonoBehaviour
    {
        public RectTransform content;
        public ChestListItem prefab;
        
        private void OnEnable()
        {
            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }
            foreach (var item in SaveController.CurrentSave.playerData.items)
            {
                var itemInst = Instantiate(prefab, content);
                itemInst.SetData(item);
            }

            StartCoroutine(FixContent());
        }

        private IEnumerator FixContent()
        {
            yield return null;
            LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        }
    }
}
