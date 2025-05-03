using UnityEngine;

namespace Player
{
    public class FPSController : MonoBehaviour
    {
        public float speed = 6.0f;
        public float runSpeed = 12f;
        public float gravity = -9.8f;
        public float jumpForce = 5.0f;

        private CharacterController _characterController;
        private PlayerCharacter _playerCharacter;
        private float _verticalVelocity;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            if (_characterController == null)
            {
                Debug.Log($"{nameof(CharacterController)} is null");
            }

            _playerCharacter = GetComponent<PlayerCharacter>();
            if (_playerCharacter == null)
            {
                Debug.Log($"{nameof(PlayerCharacter)} is null");
            }
        }

        private void Update()
        {
            if (_characterController.isGrounded)
            {
                if (Mathf.Approximately(Input.GetAxis("Jump"), 1))
                    _verticalVelocity = jumpForce;
            }
            else
            {
                _verticalVelocity += gravity * Time.deltaTime;
            }

            Vector3 movement;
            if (Input.GetKey(KeyCode.LeftShift) && _playerCharacter.IsRun())
            {
                var deltaX = Input.GetAxis("Horizontal") * runSpeed;
                var deltaZ = Input.GetAxis("Vertical") * runSpeed;
                movement = new Vector3(deltaX, _verticalVelocity, deltaZ);
                movement = Vector3.ClampMagnitude(movement, runSpeed);
                _playerCharacter.Run(1, false);
            }
            else
            {
                var deltaX = Input.GetAxis("Horizontal") * speed;
                var deltaZ = Input.GetAxis("Vertical") * speed;
                movement = new Vector3(deltaX, _verticalVelocity, deltaZ);
                movement = Vector3.ClampMagnitude(movement, speed);
                _playerCharacter.Run(1, true);
            }

            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);

            _characterController.Move(movement);
            movement.y = gravity;
        }
    }
}