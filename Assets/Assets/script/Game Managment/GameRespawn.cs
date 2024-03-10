using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRespawn : MonoBehaviour
{
    public float threshold;
    public Vector3 spawnPoint;

    void Update()
    {
        if(transform.position.y < threshold)
        {
            transform.position = spawnPoint;
            GameManager.health -= 1;
        }
    }
}
