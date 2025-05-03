using Player.Enum;
using UnityEngine;

namespace Player
{
    public class MouseLook : MonoBehaviour
    {
        public RotationAxes axes = RotationAxes.XAndY;
        public float rotationSpeedHor = 5.0f;
        public float rotationSpeedVer = 5.0f;

        public float maxVert = 45.0f;
        public float minVert = -90.0f;

        private float _rotationX;

        private void Start()
        {
            var body = GetComponent<Rigidbody>();
            if (body != null)
                body.freezeRotation = true;
        }

        private void Update()
        {
            if (axes == RotationAxes.XAndY)
            {
                _rotationX -= Input.GetAxis("Mouse Y") * rotationSpeedVer;
                _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

                var delta = Input.GetAxis("Mouse X") * rotationSpeedHor;
                var rotationY = transform.localEulerAngles.y + delta;
                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }
            else if (axes == RotationAxes.X)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * rotationSpeedHor, 0);
            }
            else if (axes == RotationAxes.Y)
            {
                _rotationX -= Input.GetAxis("Mouse Y") * rotationSpeedVer;
                _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

                var rotationY = transform.localEulerAngles.y;
                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }
        }
    }
}