using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public StaminaBar staminaBar;
    public float stamina;
    private void OnTriggerEnter(Collider other)
    {
        staminaBar.Add(stamina);
        Destroy(gameObject);  
    }
} 
