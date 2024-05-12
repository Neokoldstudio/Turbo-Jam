using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Entity
{
    private Controls inputs;

    private Vector2 Direction = Vector2.zero;
    private Vector2 lookDirection = Vector2.zero;

    [SerializeField, Range(0f, 100f)]
    private float MaxSpeed = 10f;
    private float acceleration;

    [SerializeField, Range(0f, 100f)]
    private float accelerationBuildUp = 30f;

    [SerializeField, Range(0f, 100f)]
    private float accelerationFalloff = 60f;
    private float rotationSpeed = 10f;

    [SerializeField, Range(0f, 100f)]
    private float hitForce = 5f;
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

        inputs.Player.lookMouse.performed += OnLookMousePerformed;
        inputs.Player.lookMouse.canceled += OnLookMouseCanceled;

        inputs.Player.lookJoystick.performed += OnLookJoystickPerformed;
        inputs.Player.lookJoystick.canceled += OnLookJoystickCanceled;

        inputs.Player.hit.performed += OnHitPerformed;
        inputs.Player.hit.canceled += OnHitCanceled;

        inputs.Player.Parry.performed += OnParryPerformed;
        inputs.Player.Parry.canceled += OnParryCanceled;
    }

    private void OnDisable()
    {
        inputs.Disable();
        inputs.Player.Movement.performed -= OnMovementPerformed;
        inputs.Player.Movement.canceled -= OnMovementCanceled;

        inputs.Player.lookMouse.performed -= OnLookMousePerformed;
        inputs.Player.lookMouse.canceled -= OnLookMouseCanceled;

        inputs.Player.lookJoystick.performed -= OnLookJoystickPerformed;
        inputs.Player.lookJoystick.canceled -= OnLookJoystickCanceled;

        inputs.Player.hit.performed -= OnHitPerformed;
        inputs.Player.hit.canceled -= OnHitCanceled;


        inputs.Player.Parry.performed -= OnParryPerformed;
        inputs.Player.Parry.canceled -= OnParryCanceled;
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

    private void OnLookMousePerformed(InputAction.CallbackContext inputValue)
    {

            // Get mouse position in screen coordinates
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            // Convert mouse position to world coordinates
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

            // Calculate direction from player position to mouse position
            lookDirection = (worldMousePosition - transform.position).normalized;
    }

    private void OnLookMouseCanceled(InputAction.CallbackContext inputValue){}

    private void OnLookJoystickPerformed(InputAction.CallbackContext inputValue)
    {
        lookDirection = inputValue.ReadValue<Vector2>();
    }

    private void OnLookJoystickCanceled(InputAction.CallbackContext inputValue){}

    private void OnHitPerformed(InputAction.CallbackContext inputValue)
    {
        hit();
    }

    private void OnHitCanceled(InputAction.CallbackContext inputValue){}

    private void OnParryPerformed(InputAction.CallbackContext inputValue)
    {
        parry();
    }

    private void OnParryCanceled(InputAction.CallbackContext inputValue){}

    public override void getHit(int Damage, Vector2 Direction)
    {
    }

    public override void hit()
    {
        weapon.GetComponent<weaponManager>().Attack(lookDirection);
        rb.AddForce(lookDirection.x * hitForce, lookDirection.y * hitForce, 0, ForceMode.Impulse);
    }

    public override void parry()
    {
        weapon.GetComponent<weaponManager>().Parry();
    }

    void FixedUpdate()
    {
        Vector3 velocity=rb.velocity;

        float maxSpeedChange=acceleration*Time.deltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x,Direction.x*MaxSpeed,maxSpeedChange);
        velocity.y = Mathf.MoveTowards(velocity.y,Direction.y*MaxSpeed,maxSpeedChange);

        rb.velocity = velocity;

        //rotate sword
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
        weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
