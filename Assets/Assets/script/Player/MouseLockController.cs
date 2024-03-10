using UnityEngine;

public class MouseLockController : MonoBehaviour
{
    private void Start()
    {
      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Locked;
    }  
}

