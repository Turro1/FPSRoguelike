using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public int verticalCellsCount = 9;
        public int horizontalCellsCount = 9;
        public List<InventoryCell> inventoryCells = new List<InventoryCell>();
        public GameObject itemPrefab;
        public Transform content;
        public Image mouseItemIcon;
        public static InventoryManager Instance;

        public InventoryCell enteredCell;
        public InventoryCell draggingCell;
        public bool isDraggingItem;

        private void Awake()
        {
            Instance = this;
            FillInventory();
        }

        private void Update()
        {
            if (isDraggingItem && draggingCell.currentItemScriptableObject)
            {
                mouseItemIcon.sprite = draggingCell.currentItemScriptableObject.Icon;
                mouseItemIcon.color = Color.white;
                mouseItemIcon.rectTransform.position = Input.mousePosition;
            }
            else
            {
                mouseItemIcon.color = Color.clear;
            }
        }

        private void FillInventory()
        {
            for (int i = 0; i < verticalCellsCount * horizontalCellsCount; i++)
            {
                var cell = Instantiate(itemPrefab, content).GetComponent<InventoryCell>();
                inventoryCells.Add(cell);
            }
        }

        public Tuple<bool, int> GetFreeCell()
        {
            for (var i = 0; i < inventoryCells.Count; i++)
            {
                if (inventoryCells[i].currentItemScriptableObject == null)
                {
                    return Tuple.Create(true, i);
                }
            }

            return Tuple.Create(false, 0);
        }

        public void AddItem(ItemScriptableObject itemScriptableObject, int cellId)
        {
            inventoryCells[cellId].currentItemScriptableObject = itemScriptableObject;
        }

        public void UpdateCells()
        {
            for (var i = 0; i < inventoryCells.Count; i++)
            {
                inventoryCells[i].UpdateCell();
            }
        }
    }
}