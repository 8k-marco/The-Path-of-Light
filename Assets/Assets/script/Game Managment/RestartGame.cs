using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void RestartLevel()
    {
        Time.timeScale = 1;
        GameManager.health = 4;
        SceneManager.LoadScene(2);
    }
}
