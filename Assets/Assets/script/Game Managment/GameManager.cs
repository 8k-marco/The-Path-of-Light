using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOver, heart0, heart1, heart2, heart3;
    public static int health;
    internal static object instance;

    // Start is called before the first frame update
    void Start()
    {
       health = 4;
       heart0.gameObject.SetActive(true);
       heart1.gameObject.SetActive(true);
       heart2.gameObject.SetActive(true);
       heart3.gameObject.SetActive(true);
       gameOver.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (health)
        {
           case 4:
           heart0.gameObject.SetActive(true);
           heart1.gameObject.SetActive(true);
           heart2.gameObject.SetActive(true);
           heart3.gameObject.SetActive(true);
           break;

            case 3:
           heart0.gameObject.SetActive(true);
           heart1.gameObject.SetActive(true);
           heart2.gameObject.SetActive(true);
           heart3.gameObject.SetActive(false);
           break;

           case 2:
           heart0.gameObject.SetActive(true);
           heart1.gameObject.SetActive(true);
           heart2.gameObject.SetActive(true);
           heart3.gameObject.SetActive(false);
           break;

           case 1:
           heart0.gameObject.SetActive(true);
           heart1.gameObject.SetActive(true);
           heart2.gameObject.SetActive(false);
           heart3.gameObject.SetActive(false);
           break;

           case 0:
           heart0.gameObject.SetActive(true);
           heart1.gameObject.SetActive(true);
           heart2.gameObject.SetActive(false);
           heart3.gameObject.SetActive(false);
           break;

           default:
           heart0.gameObject.SetActive(false);
           heart1.gameObject.SetActive(false);
           heart2.gameObject.SetActive(false);
           heart3.gameObject.SetActive(false);
           gameOver.gameObject.SetActive(true);
           Time.timeScale = 0;
           break;
        }
    }
}
