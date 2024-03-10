using System;
using UnityEngine;

namespace _Scripts
{
    public class CameraController1 : MonoBehaviour
    {
        public static event Action<ValueTuple<Vector3, Vector3>> OnRotationChanged = delegate { };
        [SerializeField] private float turnSpeedDelta = 1f;

        private void Update()
        {
            this.Rotate();
        }

        private void Rotate()
        {
            var currentRotation = this.transform.rotation.eulerAngles;
            currentRotation.y += Input.GetAxis("Mouse X") * this.turnSpeedDelta;
            this.transform.rotation = Quaternion.Euler(currentRotation);
            OnRotationChanged.Invoke(new ValueTuple<Vector3, Vector3>(this.transform.forward, this.transform.right));
        }
    }
}