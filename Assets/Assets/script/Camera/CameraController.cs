using System;
//using Unity.Cinemachine;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static event Action<ValueTuple<Vector3>> OnRotationChanged = delegate { };

    private void Update()
    {
        OnRotationChanged(new(this.transform.forward));
    }


}