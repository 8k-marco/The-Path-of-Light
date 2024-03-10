using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;
    private bool isThirdPerson = true;

    void Update()
    {
        if (Input.GetButtonDown("Fire8")) 
        {
            isThirdPerson = !isThirdPerson;
            UpdateCameraView();
        }
    }
    private void UpdateCameraView()
    {
        if (isThirdPerson)
        {
            thirdPersonCamera.SetActive(true);
            firstPersonCamera.SetActive(false);
        }
        else
        {
            firstPersonCamera.SetActive(true);
            thirdPersonCamera.SetActive(false);
        }
    }
}

