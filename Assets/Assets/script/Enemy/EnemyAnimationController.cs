using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Animator animator;

    void Start()
    {  
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.enabled = true;
        }
        else
        {
            Debug.LogWarning("Animator component missing from this GameObject", this);
        }
    }
}
