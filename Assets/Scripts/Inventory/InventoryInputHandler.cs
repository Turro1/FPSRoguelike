using EvolveGames;
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
        
        private PlayerController _playerController;
        private RayShooter _rayShooter;

        private void Start()
        {
            _playerController = player.GetComponent<PlayerController>();
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
                    _playerController.enabled = false;
                    _rayShooter.enabled = false;
                    
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    Time.timeScale = 0.0f;
                }
                else
                {
                    inventoryUiGameObject.SetActive(isInventoryOpen);
                    isInventoryOpen = true;
                    
                    _playerController.enabled = true;
                    _rayShooter.enabled = true; 
                    
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Time.timeScale = 1f;
                }
            }
        }
    }
}