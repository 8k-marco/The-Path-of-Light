using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player; 
    public float speed = 5.0f; 
    public float detectionRange = 10.0f;

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized; 
            transform.position += directionToPlayer * speed * Time.deltaTime;

            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
    }
}
