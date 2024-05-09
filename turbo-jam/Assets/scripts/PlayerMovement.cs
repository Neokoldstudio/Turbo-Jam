using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Controls inputs;

    private Vector2 Direction = Vector2.zero;

    private Rigidbody rb;

    [SerializeField, Range(0f, 100f)]
    private float MaxSpeed = 10f;

    private float acceleration;

    [SerializeField, Range(0f, 100f)]
    private float accelerationBuildUp = 30f;

    [SerializeField, Range(0f, 100f)]
    private float accelerationFalloff = 60f;
    float angle = 0f;

    private float rotationSpeed = 10f;

    public GameObject weapon;
    private void Awake()
    {
        inputs = new Controls();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        inputs.Enable();
        inputs.Player.Movement.performed += OnMovementPerformed;
        inputs.Player.Movement.canceled += OnMovementCanceled;
    }

    private void OnDisable()
    {
        inputs.Disable();
        inputs.Player.Movement.performed -= OnMovementPerformed;
        inputs.Player.Movement.canceled -= OnMovementCanceled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext inputValue)
    {
        Direction = inputValue.ReadValue<Vector2>();
        acceleration=accelerationBuildUp;

    }

    private void OnMovementCanceled(InputAction.CallbackContext inputValue)
    {
        acceleration=accelerationFalloff;
        Direction = Vector2.zero;
    }
    void FixedUpdate()
    {
        Vector3 velocity=rb.velocity;

        float maxSpeedChange=acceleration*Time.deltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x,Direction.x*MaxSpeed,maxSpeedChange);
        velocity.y = Mathf.MoveTowards(velocity.y,Direction.y*MaxSpeed,maxSpeedChange);

        Debug.Log(velocity);

        rb.velocity = velocity;

        //rotate sword
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, Direction);
        weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
