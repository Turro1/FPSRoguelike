using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        public Slider healthBar;
        public Slider staminaBar;

        private int _health = MaxHealth;
        private float _stamina = MaxStamina;
        private bool _isRun = true;
        

        private const float MaxStamina = 10;
        private const int MaxHealth = 10;

        public void Start()
        {
            _health = MaxHealth;
            _stamina = MaxStamina;
            _isRun = true;

            if (healthBar != null)
            {
                healthBar.maxValue = MaxHealth;
                healthBar.value = _health;
            }
            if (staminaBar != null)
            {
                staminaBar.maxValue = MaxStamina;
                staminaBar.value = _stamina;
            }
        }

        public void Hurt(int damage)
        {
            _health -= damage;
            if (_health < 0) _health = 0;
            Debug.Log($"Player health: {_health}");

            if (healthBar != null)
            {
                healthBar.value = _health;
            }
        }

        public void Run(float stamina, bool isCharged)
        {
            if (isCharged)
            {
                var addStamina = (stamina * Time.deltaTime);
                _stamina += addStamina;
            }
            else
            {
                var removeStamina = (stamina * Time.deltaTime);
                _stamina -= removeStamina;
            }

            if (_stamina < 0) _stamina = 0;
            if (_stamina > MaxStamina) _stamina = MaxStamina;

            staminaBar.value = _stamina;
        }

        public bool IsRun()
        {
            _isRun = _stamina switch
            {
                MaxStamina => true,
                0 => false,
                _ => _isRun
            };

            return _isRun;
        }
    }
}