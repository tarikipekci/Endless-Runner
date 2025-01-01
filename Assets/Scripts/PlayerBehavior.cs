using System;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private static readonly int Jumped = Animator.StringToHash("jumped");
    private static readonly int isGrounded = Animator.StringToHash("isGrounded");
    private CharacterController _characterController;
    private Animator _animator;
    private Vector3 direction;
    public float forwardSpeed;

    private int desiredLane = 1; // 0 = left, 1 = middle, 2 = right
    public float laneDistance = 4; // The distance between two lanes
    public float jumpForce;
    public float gravityForce;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        direction.z = forwardSpeed;

        if (_characterController.isGrounded)
        {
            if (SwipeManager.swipeUp)
            {
                _animator.SetBool(isGrounded, true);
                Jump();
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
        direction.y = jumpForce;
        _animator.SetTrigger(Jumped);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Obstacle"))
        {
            PlayerManager.gameOver = true;
        }
    }
}