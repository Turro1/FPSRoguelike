using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "InventoryItem", menuName = "ScriptableObjects/InventoryItem", order = 1)]
    public class ItemScriptableObject : ScriptableObject
    {
        public string Name = "Item";
        public Sprite Icon;
        public GameObject Prefab;
    }
}