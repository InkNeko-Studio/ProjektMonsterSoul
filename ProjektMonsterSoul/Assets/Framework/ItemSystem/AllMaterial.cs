using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Framework.ItemSystem
{
    [CreateAssetMenu(fileName = "AllMaterial", menuName = "Items/AllMaterial")]
    public class AllMaterial : ScriptableObject
    {
        private static AllMaterial _instance;

        private static AllMaterial GetInstance()
        {
            if (_instance == null)
                _instance = Resources.Load("AllMaterial") as AllMaterial;
            return _instance;
        }

        public List<MaterialScriptable> materials = new List<MaterialScriptable>();

        public static MaterialScriptable GetMaterial(MaterialId id)
        {
            return GetInstance().materials.Find(m => m.materialId == id);
        }
    }
}