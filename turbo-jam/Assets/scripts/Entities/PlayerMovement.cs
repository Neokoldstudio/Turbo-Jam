using System.Text.RegularExpressions;
using System;
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

    private float spriteSize;
    public GameObject weapon;
    public GameObject hands;
    public GameObject sprite;

    public Animator playerAnim;

    public SfxManager sfxManager;

    private State currentState;

    private bool IsParrying = false;

    [HideInInspector]
    public bool inEvent = false;
    [HideInInspector]
    public bool cantMove = false;
    [HideInInspector]
    public bool needParry = false;

    private IEnumerator parryStun;
    private enum State {
        Idle,
        Move,
        Parrying,
        Attacking,
        Buying,
        Dead
    }

    private void Awake()
    {
        currentState = State.Move;
        inputs = new Controls();
        acceleration = accelerationBuildUp;
        rb = GetComponent<Rigidbody2D>();
        spriteSize = sprite.transform.localScale.x;
        parryStun = ParryStun();
    }

    #region INPUTS
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
        playerAnim.SetBool("run", true);
        acceleration = accelerationBuildUp;
    }

    private void OnMovementCanceled(InputAction.CallbackContext inputValue)
    {
        acceleration = accelerationFalloff;
        playerAnim.SetBool("run", false);
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

    private void OnLookMouseCanceled(InputAction.CallbackContext inputValue) { }

    private void OnLookJoystickPerformed(InputAction.CallbackContext inputValue)
    {
        lookDirection = (inputValue.ReadValue<Vector2>()).normalized;
    }

    private void OnLookJoystickCanceled(InputAction.CallbackContext inputValue) { }

    private void OnHitPerformed(InputAction.CallbackContext inputValue)
    {
        currentState = State.Attacking;
    }

    private void OnHitCanceled(InputAction.CallbackContext inputValue) { }

    private void OnParryPerformed(InputAction.CallbackContext inputValue)
    {
        currentState = State.Parrying;
    }

    private void OnParryCanceled(InputAction.CallbackContext inputValue) { }

    #endregion

    #region STATES
    public override void getHit(int Damage, Vector2 Direction)
    {
        if (IsParrying)
        {
            print("parried");
            StopCoroutine(parryStun);
            currentState = State.Move;

            // sfx succesful parry
            sfxManager.PlaySound("parry_succesful");

            TimeManager.Instance.SlowTimeSmooth(0.5f, 0.3f, 0.5f);
            IsParrying = false;
        }
    }

    public override void hit()
    {
        if (weapon.GetComponent<weaponManager>().Attack(lookDirection))
        {
            Vector2 vec = new Vector2(lookDirection.x * hitForce, lookDirection.y * hitForce);
            rb.AddForce(vec, ForceMode2D.Impulse);

            // SFX player attacking
            // AudioSource source = Instantiate(audioSource, transform.position, Quaternion.identity);
            sfxManager.PlaySound("attack");
        }
        currentState = State.Move;
    }

    public override void parry()
    {
        if (needParry)
        {
            needParry = false;
            AnimationController.Instance.ParryKnife();
        }

        weapon.GetComponent<weaponManager>().Parry();
        IsParrying = true;
        rb.velocity = Vector3.zero;
        currentState = State.Idle;

        // parry sfx 
        sfxManager.PlaySound("parry_proc");

        StartCoroutine(parryStun);
    }

    private void Idle()
    {
        //Pour plus tard, si jamais on veut avoir des events qui se passent sur le Idle (comme des petites animations qui se lancent après un certains temps)
    }

    private void Move()
    {
        Vector3 velocity = rb.velocity;

        float maxSpeedChange = acceleration * Time.deltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, Direction.x * MaxSpeed, maxSpeedChange);
        velocity.y = Mathf.MoveTowards(velocity.y, Direction.y * MaxSpeed, maxSpeedChange);

        rb.velocity = velocity;

        if (Mathf.Sign(sprite.transform.localScale.x) != Mathf.Sign(Direction.x) && Direction.x != 0.0f)
            sprite.transform.localScale = new Vector3(Mathf.Sign(Direction.x) * spriteSize, sprite.transform.localScale.y, sprite.transform.localScale.z);

        //rotate sword
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
        weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        weapon.transform.localScale = new Vector3(Mathf.Sign(lookDirection.x), 1.0f, weapon.transform.localScale.z);
    }

    void UpdateVelocity()
    {

    }

    #endregion

    void FixedUpdate()
    {
        if (!inEvent)
        {
            switch (currentState)
            {
                case State.Idle:
                    Idle();
                    break;
                case State.Move:
                    if(!cantMove)
                        Move();
                    break;
                case State.Attacking:
                    hit();
                    break;
                case State.Parrying:
                    parry();
                    break;
                default:
                    break;
            }
        }
        
        //rotatesword()
        UpdateVelocity();
    }


    //Coroutines
    IEnumerator ParryStun()
    {
        yield return new WaitForSeconds(weapon.GetComponent<weaponManager>().getParryStun());
        IsParrying = false;
        currentState = State.Move;
    }
}
