using UnityEngine;

public class ToggleObjectWithOptionButton : MonoBehaviour
{
    public GameObject objectToToggle; 

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetButtonDown("Fire6"))
        {
            if (objectToToggle != null)
            {
                objectToToggle.SetActive(!objectToToggle.activeSelf);
            }
        }
    }
}
