using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    public class InventoryInputHandler : MonoBehaviour
    {
        public GameObject inventoryUiGameObject;
        public bool isInventoryOpen = false;
        [FormerlySerializedAs("Player")] public GameObject player;

        private MouseLook _mouseLook;
        private FPSController _fpsController;
        private RayShooter _rayShooter;

        private void Start()
        {
            _mouseLook = player.GetComponent<MouseLook>();
            _fpsController = player.GetComponent<FPSController>();
            _rayShooter = player.GetComponent<RayShooter>();
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
                    
                    _mouseLook.enabled = false;
                    _fpsController.enabled = false;
                    _rayShooter.enabled = false;
                    
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    Time.timeScale = 0.0f;
                }
                else
                {
                    inventoryUiGameObject.SetActive(isInventoryOpen);
                    isInventoryOpen = true;
                    
                    _mouseLook.enabled = true;
                    _fpsController.enabled = true;
                    _rayShooter.enabled = true; 
                    
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Time.timeScale = 1f;
                }
            }
        }
    }
}