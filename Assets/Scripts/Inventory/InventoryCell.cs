using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization; // Подключаем пространство имен для TextMeshPro

namespace Inventory
{
    public partial class InventoryCell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [FormerlySerializedAs("currentItem")] public ItemScriptableObject currentItemScriptableObject;
        public Image image;
        public bool isDragging;
        public bool isEntered;
        [SerializeField] private TextMeshProUGUI itemLabel; // Ссылка на UI текст для названия предмета

        private void Start()
        {
            image = GetComponentsInChildren<Image>()[1];
            UpdateCell();
            // Убедитесь, что текст скрыт при старте
            if (itemLabel != null)
            {
                itemLabel.gameObject.SetActive(false);
            }
        }

        public void UpdateCell()
        {
            if (currentItemScriptableObject && currentItemScriptableObject.Icon)
            {
                image.sprite = currentItemScriptableObject.Icon;
                image.color = Color.white;
            }
            else
            {
                image.sprite = null;
                image.color = Color.clear;
            }
        }

        public void RemoveItem()
        {
            SpawnObject(currentItemScriptableObject.Prefab);
            currentItemScriptableObject = null;
            UpdateCell();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G) && isEntered)
            {
                RemoveItem();
            }
        }

        private void SpawnObject(GameObject prefabToSpawn)
        {
            Instantiate(prefabToSpawn, Vector3.zero, Quaternion.identity);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;
            InventoryManager.Instance.isDraggingItem = true;
            InventoryManager.Instance.draggingCell = this;
            // Скрыть label при начале перетаскивания
            if (itemLabel != null)
            {
                itemLabel.gameObject.SetActive(false);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (isDragging && InventoryManager.Instance.enteredCell)
            {
                var item = InventoryManager.Instance.enteredCell.currentItemScriptableObject;
                InventoryManager.Instance.enteredCell.currentItemScriptableObject = currentItemScriptableObject;
                currentItemScriptableObject = item;
                InventoryManager.Instance.isDraggingItem = false;
                InventoryManager.Instance.draggingCell = null;
                InventoryManager.Instance.enteredCell = null;
                isDragging = false;
            }
            else if (isDragging)
            {
                InventoryManager.Instance.isDraggingItem = false;
                InventoryManager.Instance.draggingCell = null;
                InventoryManager.Instance.enteredCell = null;
                isDragging = false;
            }
            InventoryManager.Instance.UpdateCells();
        }
        
        private void OnGUI()
        {
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isEntered = true;
            InventoryManager.Instance.enteredCell = this;

            // Показать название предмета, если ячейка не пуста
            if (currentItemScriptableObject != null && itemLabel != null)
            {
                itemLabel.text = currentItemScriptableObject.name; // Предполагается, что в ItemScriptableObject есть поле itemName
                itemLabel.gameObject.SetActive(true);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isEntered = false;
            InventoryManager.Instance.enteredCell = null;

            // Скрыть название предмета
            if (itemLabel != null)
            {
                itemLabel.gameObject.SetActive(false);
            }
        }
    }
}