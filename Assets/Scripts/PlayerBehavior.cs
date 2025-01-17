using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private static readonly int Jumped = Animator.StringToHash("jumped");
    private static readonly int isGrounded = Animator.StringToHash("isGrounded");
    private static readonly int SpeedMultiplier = Animator.StringToHash("speedMultiplier");
    private static readonly int Slide = Animator.StringToHash("slide");
    private CharacterController _characterController;
    private Animator _animator;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1; // 0 = left, 1 = middle, 2 = right
    public float laneDistance = 4; // The distance between two lanes
    public float jumpForce;
    public float gravityForce;
    private bool canJump;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _animator.SetFloat(SpeedMultiplier, 1.0f);
        canJump = true;
    }

    void Update()
    {
        if (!PlayerManager.isGameStarted) return;

        //Increase speed
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.05f * Time.deltaTime;
            if (forwardSpeed >= 12)
            {
                _animator.SetFloat(SpeedMultiplier, 1.2f);
                gravityForce = -12;
            }
        }
        else
        {
            _animator.SetFloat(SpeedMultiplier, 1.3f);
        }

        direction.z = forwardSpeed;

        if (_characterController.isGrounded)
        {
            if (SwipeManager.swipeUp)
            {
                _animator.SetBool(isGrounded, true);
                Jump();
            }

            if (SwipeManager.swipeDown)
            {
                PerformSlide();
            }
        }
        else
        {
            direction.y += gravityForce * Time.deltaTime;
        }

        // Gather the inputs for lane switching
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane > 2) // Clamp to the rightmost lane
            {
                desiredLane = 2;
            }
        }
        else if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane < 0) // Clamp to the leftmost lane
            {
                desiredLane = 0;
            }
        }

        // Calculate the target position
        Vector3 targetPosition = transform.position;
        targetPosition.x = (desiredLane - 1) * laneDistance;

        // Smoothly move the player towards the target position
        Vector3 movePosition = Vector3.Lerp(transform.position, targetPosition, 10 * Time.deltaTime);
        _characterController.Move((movePosition - transform.position) + direction * Time.deltaTime);
    }

    private void Jump()
    {
        if (canJump)
        {
            direction.y = jumpForce;
            _animator.SetTrigger(Jumped);
        }
    }

    private void PerformSlide()
    {
        _animator.SetTrigger(Slide);
    }

    private void FalseCanJump()
    {
        canJump = false;
    }

    private void TrueCanJump()
    {
        canJump = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Obstacle"))
        {
            PlayerManager.gameOver = true;
        }
    }
}