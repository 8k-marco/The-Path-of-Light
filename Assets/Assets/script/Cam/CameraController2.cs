using System;
using UnityEngine;

namespace _Scripts
{
    public class CameraController2 : MonoBehaviour
    {
        public static event Action<(Vector3, Vector3)> OnRotationChanged = delegate { };

        [SerializeField] private float turnSpeedDelta = 1f;
        [SerializeField] private Transform targetObject; 
        [SerializeField] private float tiltSpeedDelta = 1f; 
        [SerializeField] private float minTiltAngle = -90f;
        [SerializeField] private float maxTiltAngle = 90f;
        private float currentTiltAngle = 0f;

        private void Update()
        {
            this.RotateCameraY();
            this.TiltObjectX();
        }

        private void RotateCameraY()
        {
            var currentRotation = this.transform.rotation.eulerAngles;
            currentRotation.y += Input.GetAxis("Mouse X") * this.turnSpeedDelta;
            this.transform.rotation = Quaternion.Euler(currentRotation);
            OnRotationChanged.Invoke((this.transform.forward, this.transform.right));
        }

        private void TiltObjectX()
        {
            if (targetObject != null)
            {
                float tiltAmount = Input.GetAxis("Mouse Y") * this.tiltSpeedDelta;
                currentTiltAngle -= tiltAmount;
                currentTiltAngle = Mathf.Clamp(currentTiltAngle, minTiltAngle, maxTiltAngle);
                targetObject.localRotation = Quaternion.Euler(currentTiltAngle, targetObject.localEulerAngles.y, targetObject.localEulerAngles.z);
            }
        }
    }
}
