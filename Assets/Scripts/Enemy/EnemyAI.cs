using UnityEngine;
using Random = System.Random;

namespace Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        public float speed = 5.0f;
        public float obstacleRange = 5.0f;
        public bool alive = true;

        [SerializeField]
        private GameObject[] _fireballsPrefab;
        private GameObject _fireball;

        private void Start()
        {
            SetAlive(true);
        }

        public void SetAlive(bool isAlive)
        {
            alive = isAlive;
        }

        private void Update()
        {
            if (!alive) return;

            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, obstacleRange))
            {
                GameObject hitObject = hit.transform.gameObject;

                // Если перед врагом игрок — атакуем
                if (hitObject.GetComponent<CharacterController>())
                {
                    if (_fireball == null)
                    {
                        int randIndex = new Random().Next(0, _fireballsPrefab.Length);
                        _fireball = Instantiate(_fireballsPrefab[randIndex]);
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _fireball.transform.rotation = transform.rotation;
                    }
                }
                else
                {
                    // Если препятствие — разворачиваемся
                    TurnAround();
                }
            }
        }

        private void TurnAround()
        {
            // Поворачиваемся случайно влево или вправо на 180 градусов
            float rotationY = new Random().Next(160, 200);
            transform.Rotate(0, rotationY, 0);
        }
    }
}