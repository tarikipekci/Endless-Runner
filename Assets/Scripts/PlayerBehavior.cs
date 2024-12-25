using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector3 direction;
    public float forwardSpeed;

    private int desiredLane = 1; //0 = left : 2 = right
    public float laneDistance = 4; //the distance between two lanes
    
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        direction.z = forwardSpeed;
        
        //Gather the inputs on which lane player should be 
        if (Input.GetKeyDown(KeyCode.D))
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }
        
        //Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        _characterController.Move(direction * Time.fixedDeltaTime);
    }
}
