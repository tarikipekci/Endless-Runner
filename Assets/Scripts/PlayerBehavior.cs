using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector3 direction;
    public float forwardSpeed;
    
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        direction.z = forwardSpeed;
    }

    private void FixedUpdate()
    {
        _characterController.Move(direction * Time.fixedDeltaTime);
    }
}
