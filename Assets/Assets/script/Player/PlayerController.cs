using System.Collections;
using System.ComponentModel;
using UnityEngine;


namespace _Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed;
        public float swingSpeed;
        public float groundDrag;
        public float grappleFov = 95f;
        //public PlayerCam cam;

        [SerializeField] private float jumpForce = 300f;
        public float jumpCooldown;
        [SerializeField] public float airMultiplier = 3.0f;
        bool readyToJump;

        [SerializeField] private float speedDelta = 5f;

        public Animator playerAnim;
        public Animator doorAnimator;
        public Animator door2Animator;
        public bool isCollected;

        [Header("Ground Check")]
        public Transform groundSensor;
        public float sensorDistance;
        public LayerMask whatIsGround;
        bool grounded;

        [Header("Gravity")]
        [SerializeField] private float downPull = 1f;

        [Header("Audio")]
        [SerializeField] private AudioSource jumpSoundEffect;
        

        float fallTimer = 0f;

        Vector3 moveDirection;

        public bool jumping;
        Rigidbody physics;

        public bool freeze;
        public bool activeGrapple;
        public bool swinging;

        private float velocity;

        [Header("Slope Handling")]
        private RaycastHit slopeHit;
        public float maxSlopeAngle;
        public float playerHeight;

        public MovementState state;

        public enum MovementState 
        {
            freeze,
            swinging,
        }
        private void Start()
        {
            //this.physics = this.GetComponentInChildren<Rigidbody>();
            physics = GetComponent<Rigidbody>();
            readyToJump = true;
        }
        
        private void OnEnable()
        {
            CameraController1.OnRotationChanged += this.Rotate;
            CameraController2.OnRotationChanged += this.Rotate;
        }
        
        private void OnDisable()
        {
            CameraController1.OnRotationChanged -= this.Rotate;
            CameraController2.OnRotationChanged -= this.Rotate;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "DoorCollider")
            {
                isCollected = true;
                doorAnimator.SetBool("isCollected", isCollected);
            }
            else if (other.gameObject.tag == "ClosedDoor")
            {
                isCollected = false;
                doorAnimator.SetBool("isCollected", isCollected);
            }

            if (other.gameObject.tag == "DoorCollider")
            {
                isCollected = true;
                door2Animator.SetBool("isCollected", isCollected);
            }
            else if (other.gameObject.tag == "ClosedDoor")
            {
                isCollected = false;
                door2Animator.SetBool("isCollected", isCollected);
            }

            if (other.gameObject.tag == "Enemy")
            {
                GameManager.health -= 1;
            }
        }



        private void Update()
        {
            ShouldJump();
            SpeedControl();
            PlayLocomotionAnimation();

            if (grounded && !activeGrapple)
                physics.drag = groundDrag;
            else
                physics.drag = 0;
        }

        private void FixedUpdate()
        {
            grounded = Physics.Raycast(this.groundSensor.position
                , Vector3.down, this.sensorDistance, this.whatIsGround);
            if (grounded)
                this.fallTimer = 0;
            Moveplayer();
            this.PullDown();
        }

        private void PullDown()
        {
            this.fallTimer += Time.deltaTime;
            var currentVelocity = this.physics.velocity;
            currentVelocity.y -= this.downPull * this.fallTimer * this.fallTimer;
            this.physics.velocity = currentVelocity;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.groundSensor.position, this.groundSensor.position + Vector3.down * this.sensorDistance);
        }
        private void ShouldJump()
        {
            if (Input.GetButtonDown("Fire2") && readyToJump && grounded)
            {
                readyToJump = false;
                //this.playerAnim.Play("Locomotoin", 0, 0);

                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
                jumpSoundEffect.Play();
            }
        }

        private void StateHandler()
        {
            // Mode - Freeze
            if (freeze)
            {
                state = MovementState.freeze;
                moveSpeed = 0;
                physics.velocity = Vector3.zero;
            }

            // Mode - Swinging
            else if (swinging)
            {
                state = MovementState.swinging;
                moveSpeed = swingSpeed;
            }
        }
        private void Moveplayer()
        {
            if (activeGrapple) return;
            if (swinging) return;
            var vertical = Input.GetAxis("Vertical");
            var currentMovementSpeed = this.speedDelta * (grounded ? 1 : airMultiplier);
            this.physics.velocity = this.transform.forward * vertical * currentMovementSpeed;
        }

        private void Rotate((Vector3 forward, Vector3 right) cameraOrientation)
        {
            var forward = cameraOrientation.forward;
            forward.y = 0;
            var right = cameraOrientation.right;
            right.y = 0;
            this.transform.forward =
                (forward.normalized
                 + right.normalized);
        }
       
        private void SpeedControl()
        {
            Vector3 flatVel = new Vector3(physics.velocity.x, 0f, physics.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                physics.velocity = new Vector3(limitedVel.x, physics.velocity.y, limitedVel.z);
            }
        }

        private void Jump()
        {
            physics.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            this.playerAnim.Play("Jumping", 0, 0);
        }
        private void ResetJump()
        {
            readyToJump = true;
        }


        //private bool enableMovementOnNextTouch;
        public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
        {
            activeGrapple = true;

            velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
            Invoke(nameof(SetVelocity), 0.1f);

            Invoke(nameof(ResetRestrictions), 3f);
        }
        private Vector3 velocityToSet;
        private void SetVelocity()
        {
            //enableMovementOnNextTouch = true;
            physics.velocity = velocityToSet;

            //cam.DoFov(grappleFov);
        }
        public void ResetRestrictions()
        {
            activeGrapple = false;
            //cam.DoFov(85f);
        }
        private void PlayLocomotionAnimation() =>
        this.playerAnim.SetFloat("Locomotoin", new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Vertical")).normalized.magnitude);

        public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
        {
            float gravity = Physics.gravity.y;
            float displacementY = endPoint.y - startPoint.y;
            Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
            Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
                + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

            return velocityXZ + velocityY;
        }

       /* private bool IsOnSlope()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
            {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle < maxSlopeAngle && angle != 0;
            }

            return false;
        }
       */
    }
}