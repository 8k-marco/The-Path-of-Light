using UnityEngine;
using System.Collections;

public class PlayerCrouch : MonoBehaviour
{
    public float crouchHeight = 0.5f;
    public float playerHeight = 2.0f;
    public float crouchSpeed = 5f;

    private bool isCrouching = false;
    public Animator animator;
    public Rigidbody rb;
    public CapsuleCollider capsuleCollider;
    public BoxCollider boxCollider;

    void Start()
    {
       capsuleCollider.enabled = true;
       boxCollider.enabled = false;
    }

    void Update()
    {
        HandleCrouchInput();
    }

    void HandleCrouchInput()
    {
        if (Input.GetButtonDown("Fire5") && !isCrouching)
        {
            Crouch();
        }
        else if (Input.GetButtonDown("Fire5") && isCrouching)
        {
            StandUp();
        }
    }

    void Crouch()
    {
        isCrouching = true;
        animator.SetBool("IsCrouching", true);
        StartCoroutine(ResizePlayer(playerHeight, crouchHeight, crouchSpeed));
        capsuleCollider.enabled = false;
        boxCollider.enabled = true;
    }

    void StandUp()
    {
        isCrouching = false;
        animator.SetBool("IsCrouching", false);
        StartCoroutine(ResizePlayer(crouchHeight, playerHeight, crouchSpeed));
        capsuleCollider.enabled = true;
        boxCollider.enabled = false;
    }

    IEnumerator ResizePlayer(float startHeight, float targetHeight, float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            float newHeight = Mathf.Lerp(startHeight, targetHeight, elapsedTime / time);
            transform.localScale = new Vector3(1, newHeight, 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(1, targetHeight, 1);
    }
}
