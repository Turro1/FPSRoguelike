using UnityEngine;

namespace Inventory
{
    public class InventoryInputHandler: MonoBehaviour
    {
        public GameObject inventoryUiGameObject;
        public bool isInventoryOpen = false;
        private void Start()
        {
            if (inventoryUiGameObject == null)
            {
                Debug.Log("Inventory Ui game object is null");
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (isInventoryOpen)
                {
                    inventoryUiGameObject.SetActive(isInventoryOpen);
                    isInventoryOpen = false;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    Time.timeScale = 0.1f;
                }
                else
                {
                    inventoryUiGameObject.SetActive(isInventoryOpen);
                    isInventoryOpen = true;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Time.timeScale = 1f;
                }
            }
        }
    }
}