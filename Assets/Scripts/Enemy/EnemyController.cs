using System;
using UnityEngine;
using Random = System.Random;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _enemyPrefab;

        private GameObject _enemy;

        private void Update()
        {
            if (_enemy == null)
            {
                var randEnemy = new Random().Next(1, _enemyPrefab.Length);
                _enemy = Instantiate(_enemyPrefab[randEnemy]) as GameObject;
                _enemy.transform.position = new Vector3(0, 3, 0);
                float angle = new Random().Next(0, 360);
                _enemy.transform.Rotate(0,angle,0);
            }
        }
    }
}