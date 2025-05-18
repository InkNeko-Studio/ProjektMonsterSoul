using UnityEngine;

namespace Framework.ItemSystem
{
    [CreateAssetMenu(fileName = "Material", menuName = "Items/Material")]
    public class MaterialScriptable : ScriptableObject
    {
        public MaterialId materialId;
        public string materialName;
        public Sprite materialSprite;
    }
}