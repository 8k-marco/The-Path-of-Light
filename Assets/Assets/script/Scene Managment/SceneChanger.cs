using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadStart(int levelnumer){
        SceneManager.LoadScene(levelnumer);
    }

    public void Quit(){
        Application.Quit();
    }
}
