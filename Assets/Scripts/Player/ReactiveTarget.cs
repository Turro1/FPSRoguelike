using System;
using System.Collections;
using Enemy;
using UnityEngine;

namespace Player
{
    public class ReactiveTarget : MonoBehaviour
    {
        private EnemyAI _enemyAI;
        private void Start()
        {
            _enemyAI = GetComponent<EnemyAI>();
        }

        public void ReactToHit()
        {
            if (_enemyAI != null)
                _enemyAI.SetAlive(false);

            StartCoroutine(DieCourtine(3));
        }

        private IEnumerator DieCourtine(float waitSecond)
        {
            this.transform.Rotate(45,0,0);

            yield return new WaitForSeconds(waitSecond);
            
            Destroy(this.transform.gameObject);
        }
    }
}