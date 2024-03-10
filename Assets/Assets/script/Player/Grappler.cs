using _Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    [Header("References")]
    private PlayerController pm;
    public Transform cam;
    public Transform gunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;

    [Header("Input")]
    public string grappleButton = "Grapple";
    private bool isGrappling;

    [Header("StaminaBar")]
    public StaminaBar staminaBar;

    [Header("Audio")]
    [SerializeField] private AudioSource swingingSoundEffect;


    private void Start()
    {
        pm = GetComponent<PlayerController>();
    }

    private void Update()
    {
        print(this.staminaBar.CanMakeSpecialMove);
        if (Input.GetButtonDown(grappleButton) && this.staminaBar.CanMakeSpecialMove) SetGrapplingPoint();
        {
            swingingSoundEffect.Play();
        }
        if (Input.GetButtonUp(grappleButton)) StopGrapple();

        ExecuteGrapple();

        if (grapplingCdTimer > 0)
            grapplingCdTimer -= Time.deltaTime;
    }

    private void SetGrapplingPoint()
    {
        this.staminaBar.EmptyStamina();
        GetComponent<Swinging>().StopSwing();
        isGrappling = true;
        pm.freeze = true;
        if (Physics.Raycast(cam.position, cam.forward, out var hit, maxGrappleDistance, whatIsGrappleable))
            grapplePoint = hit.point;
        else
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;

    }

    private void ExecuteGrapple()
    {
        if (!this.isGrappling) return;

        DisplayRope();
        pm.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        pm.JumpToPosition(grapplePoint, highestPointOnArc);

    }
    private void DisplayRope()
    {
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
        lr.enabled = true;
    }

    public void StopGrapple()
    {
        isGrappling = false;
        pm.freeze = false;
        grapplingCdTimer = grapplingCd;
        HideRope();
    }

    private void HideRope()
    {
        lr.enabled = false;
    }

    public bool IsGrappling()
    {
        return isGrappling;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}

