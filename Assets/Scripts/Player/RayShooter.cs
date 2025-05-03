using System.Collections;
using UnityEngine;
using Inventory;

namespace Player
{
    public class RayShooter : MonoBehaviour
    {
        private Camera _camera;
        [SerializeField] private float pickupDistance = 3f;
        private bool _showPickupPrompt = false;
        
        private void Start()
        {
            _camera = GetComponent<Camera>();
            if(_camera == null)
                Debug.Log($"{nameof(Camera)} is null");

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        private void Update()
        {
            var screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            var ray = _camera.ScreenPointToRay(screenCenter);
            RaycastHit hit;

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    var hitObject = hit.transform.gameObject;
                    var target = hitObject.GetComponent<ReactiveTarget>();

                    if (target != null)
                    {
                        target.ReactToHit();
                    }
                    else
                    {
                        StartCoroutine(SphereIndicatorCoroutine(hit.point));
                        
                        Debug.DrawLine(this.transform.position, hit.point, Color.green, 6);
                    }
                }
            }
            
            // Проверка на возможность подобрать предмет
            if (Physics.Raycast(ray, out hit, pickupDistance))
            {
                // Проверяем, что объект имеет тег "AK47" или другой подходящий тег
                if (hit.transform.CompareTag("Weapon") || hit.transform.CompareTag("Item") || hit.transform.name.Contains("AK47"))
                {
                    // Отображаем подсказку, если смотрим на объект, который можно подобрать
                    DisplayPickupPrompt(true);
                    
                    // Если нажата клавиша E - подбираем предмет
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        PickupItem(hit.transform.gameObject);
                    }
                }
                else
                {
                    DisplayPickupPrompt(false);
                }
            }
            else
            {
                DisplayPickupPrompt(false);
            }
        }

        private void OnGUI()
        {
            var size = 24;
            var posX = _camera.pixelWidth / 2 - size / 4;
            var posY = _camera.pixelHeight / 2 - size / 2;
            GUI.Label(new Rect(posX, posY, size, size), "*");
            
            // Отображение подсказки для подбора предмета
            if (_showPickupPrompt)
            {
                int promptWidth = 200;
                int promptHeight = 30;
                int promptX = _camera.pixelWidth / 2 - promptWidth / 2;
                int promptY = _camera.pixelHeight / 2 + 30;
                
                GUI.Label(new Rect(promptX, promptY, promptWidth, promptHeight), "Нажмите E, чтобы подобрать");
            }
        }

        private IEnumerator SphereIndicatorCoroutine(Vector3 pos)
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = pos;

            yield return new WaitForSeconds(6);
                
            Destroy(sphere);
        }
        
        private void DisplayPickupPrompt(bool show)
        {
            _showPickupPrompt = show;
        }
        
        private void PickupItem(GameObject itemObject)
        {
            // Проверяем, есть ли у предмета компонент со ссылкой на Item

            var item = itemObject.GetComponent<Item>();
            if (item != null)
            {
                // Получаем инвентарь и добавляем предмет
                var inventoryManager = InventoryManager.Instance;
                if (inventoryManager != null)
                {
                    var freeCell = inventoryManager.GetFreeCell();
                    if (freeCell.Item1)
                    {
                        inventoryManager.AddItem(item.ItemScriptableObject, freeCell.Item2);
                        inventoryManager.UpdateCells();
                        Debug.Log($"Поднят предмет: {item.ItemScriptableObject.Name}");
                        
                        // Удаляем объект со сцены
                        Destroy(itemObject);
                    }
                    else
                    {
                        Debug.Log("Инвентарь полон!");
                    }
                }
            }
            else
            {
                Debug.LogWarning($"У объекта {itemObject.name} нет компонента ItemReference с Item");
            }
        }
    }
}