using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMovement : Entity
{
    private Controls inputs;
    private Vector2 Direction = Vector2.zero;
    private Vector2 lookDirection = Vector2.zero;
    private Vector2 DodgeDirection = Vector2.zero;

    [SerializeField, Range(0f, 100f)]
    private float MaxSpeed = 10f;
    private float acceleration;

    [SerializeField, Range(0f, 100f)]
    private float accelerationBuildUp = 30f;

    [SerializeField, Range(0f, 100f)]
    private float accelerationFalloff = 60f;

    [SerializeField, Range(0f, 100f)]
    private float hitForce = 5f;

    [SerializeField, Range(0f, 100f)]
    private float DodgeSpd = 5f;

    [SerializeField, Range(0f, 5f)]
    private float DodgeAnimSpeed = 1f;

    [SerializeField]
    private AnimationCurve dodgeSpeedCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f); // Exemple de courbe linéaire par défaut
    private float DodgeElapsedTime;

    [SerializeField, Range(0f, 10f)]
    private float maxLookRange;

    public Color skinColor;

    private float spriteSize;
    public weaponManager weaponManager;
    public GameObject hands;
    public GameObject sprite;
    public GameObject camMidpoint;

    public Animator playerAnim;

    public SfxManager sfxManager;

    private State currentState;
    private State lastState;

    private bool isParrying = false;

    [HideInInspector]
    public bool inEvent = false;
    [HideInInspector]
    public bool cantMove = false;
    [HideInInspector]
    public bool needParry = false;

    private IEnumerator parryStun;
    private enum State
    {
        Idle,
        Move,
        Parrying,
        Attacking,
        Dodging,
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
        weaponManager.skinColor = skinColor;
        weaponManager.hands = hands;
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

        inputs.Player.parry.performed += OnParryPerformed;
        inputs.Player.parry.canceled += OnParryCanceled;

        inputs.Player.dodge.performed += OnDodgePerformed;
        inputs.Player.dodge.canceled += OnDodgeCanceled;

        inputs.Player.throwWeapon.performed += OnThrowPerformed;
        inputs.Player.throwWeapon.canceled += OnThrowCanceled;

        inputs.Player.interact.performed += OnInteractPerformed;
        inputs.Player.interact.canceled += OnInteractCanceled;
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

        inputs.Player.parry.performed -= OnParryPerformed;
        inputs.Player.parry.canceled -= OnParryCanceled;

        inputs.Player.dodge.performed -= OnDodgePerformed;
        inputs.Player.dodge.canceled -= OnDodgeCanceled;

        inputs.Player.throwWeapon.performed -= OnThrowPerformed;
        inputs.Player.throwWeapon.canceled -= OnThrowCanceled;

        inputs.Player.interact.performed -= OnInteractPerformed;
        inputs.Player.interact.canceled -= OnInteractCanceled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext inputValue)
    {
        Direction = inputValue.ReadValue<Vector2>();
        if(currentState == State.Move)
        {
            playerAnim.SetBool("run", true);
        }
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
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        float zDistance = 10.0f; // Adjust this value based on your scene depth

        // Convert mouse position to world coordinates
        Vector3 screenPosition = new Vector3(mousePosition.x, mousePosition.y, zDistance);
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(screenPosition);

        Debug.Log("Screen Position: " + screenPosition);
        Debug.Log("World Mouse Position: " + worldMousePosition);
        Debug.Log("Player Position: " + transform.position);

        // Calculate direction from player position to mouse position
        Vector3 direction = worldMousePosition - transform.position;
        Debug.Log("Direction before normalization: " + direction);

        if (currentState != State.Attacking) lookDirection = direction.normalized;

        if (camMidpoint != null)
            camMidpoint.gameObject.transform.position = Vector3.ClampMagnitude(direction / 2, maxLookRange) + transform.position;
    }

    private void OnLookMouseCanceled(InputAction.CallbackContext inputValue) { }

    private void OnLookJoystickPerformed(InputAction.CallbackContext inputValue)
    {
        lookDirection = (inputValue.ReadValue<Vector2>()).normalized;

        if (camMidpoint != null)
            camMidpoint.gameObject.transform.position = Vector3.ClampMagnitude(lookDirection / 2, maxLookRange) + transform.position;
    }

    private void OnLookJoystickCanceled(InputAction.CallbackContext inputValue) { }

    private void OnHitPerformed(InputAction.CallbackContext inputValue)
    {
        if (hasWeapon())
        {
            if (throwing)
            {
                Debug.LogWarning(throwTimer.PercentTime());
                weaponManager.ThrowWeapon(throwTimer.PercentTime(), lookDirection);
                CancelThrow();
                return;
            }
            if (currentState != State.Dodging) currentState = State.Attacking;
        }
    }

    private void OnHitCanceled(InputAction.CallbackContext inputValue) { }

    private void OnDodgePerformed(InputAction.CallbackContext inputValue)
    {
        if (currentState != State.Dodging && currentState != State.Attacking)
        {
            playerAnim.SetTrigger("dodge");
            playerAnim.speed = 1f / DodgeAnimSpeed;
            weaponManager.gameObject.SetActive(false);

            DodgeDirection = rb.velocity;

            if (DodgeDirection == Vector2.zero)
            {
                DodgeDirection = new Vector2(1, 0) * Mathf.Sign(sprite.transform.localScale.x);
            }
            lastState = currentState;
            currentState = State.Dodging;
            DodgeElapsedTime = 0f;
        }
    }

    private void OnDodgeCanceled(InputAction.CallbackContext inputValue) { }

    public void StopDodge()
    {
        currentState = lastState;
        if(Direction != Vector2.zero)
        {
            playerAnim.SetBool("run", true);
        }
        weaponManager.gameObject.SetActive(true);
        playerAnim.speed = 1f;
    }

    private void OnParryPerformed(InputAction.CallbackContext inputValue)
    {
        if (hasWeapon())
        {
            if (throwing)
            {
                weaponManager.DropWeapon();
                CancelThrow();
                return;
            }
            if (currentState != State.Dodging && currentState != State.Attacking)
            {
                lastState = currentState;
                Debug.LogWarning(lastState);
                currentState = State.Parrying;
            }
        }
    }

    private void OnParryCanceled(InputAction.CallbackContext inputValue) { }

    private void OnInteractPerformed(InputAction.CallbackContext inputValue)
    {
        if (CanInteract() && interactables.Count > 0)
        {
            Debug.Log(NearestInteractables());
            NearestInteractables().OnInteract(this);
        }
    }

    public Interactable NearestInteractables()
    {
        if (interactables.Count > 1)
        {
            Interactable nearestInteractable = interactables[0];
            float nearestDistance = (interactables[0].transform.position - transform.position).sqrMagnitude;
            for(int i = 0; i < interactables.Count; i++)
            {
                interactables[0].RemoveHighLight();
                float distance = (interactables[i].transform.position - transform.position).sqrMagnitude;
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestInteractable = interactables[i];
                    interactables[i].HighLight();
                }
            }
            return nearestInteractable;
        }
        else if(interactables.Count == 1)
        {
            interactables[0].HighLight();
            return interactables[0];
        }
        return null;
    }

    public void GrabItem(ItemPickUp pickUp)
    {
        if (hasWeapon())
        {
            weaponManager.DropWeapon();
        }
        weaponManager.EquipWeapon(pickUp.weapon);
    }
    private void OnInteractCanceled(InputAction.CallbackContext inputValue)
    {
    }
    Timer throwTimer;
    bool throwing = false;
    private void OnThrowPerformed(InputAction.CallbackContext inputValue)
    {
        if (hasWeapon())
        {
            throwTimer = new Timer(weaponManager.weapon.throwChargeTime);

            throwing = true;

            weaponManager.animator.SetBool("charging", true);
        }
    }

    private void OnThrowCanceled(InputAction.CallbackContext inputValue)
    {
        CancelThrow();
    }
    void CancelThrow()
    {
        throwing = false;
        weaponManager.animator.SetBool("charging", false);
        throwTimer.End();
    }

    #endregion

    #region STATES
    public override void getHit(int Damage, Vector2 Direction)
    {
        if (currentState == State.Dodging) return;

        if (isParrying)
        {
            print("parried");
            StopCoroutine(parryStun);
            currentState = State.Move;

            // sfx succesful parry
            sfxManager.PlaySound("parry_succesful");


            weaponManager.Sparks();
            if (weaponManager.getPerfectParryState())
            {
                TimeManager.Instance.SlowTimeSmooth(0.5f, 0.3f, 0.5f);
            }
            isParrying = false;
        }
    }

    private void Idle()
    {
        //Pour plus tard, si jamais on veut avoir des events qui se passent sur le Idle (comme des petites animations qui se lancent après un certains temps)
        UpdateSpriteScale();
        UpdateVelocity();
    }

    private void Move()
    {
        UpdateSpriteScale();
        UpdateVelocity();
    }

    private void UpdateSpriteScale()
    {
        if (Mathf.Sign(sprite.transform.localScale.x) != Mathf.Sign(lookDirection.x) && lookDirection.x != 0.0f)
            sprite.transform.localScale = new Vector3(Mathf.Sign(lookDirection.x) * spriteSize, sprite.transform.localScale.y, sprite.transform.localScale.z);
    }

    private void Dodge()
    {
        //voir comment implémenter la logique du dodge en tant qu'état
        sprite.transform.localScale = new Vector3(Mathf.Sign(DodgeDirection.x) * spriteSize, sprite.transform.localScale.y, sprite.transform.localScale.z);

        float t = DodgeElapsedTime / DodgeAnimSpeed;
        float speedMultiplier = dodgeSpeedCurve.Evaluate(t);
        Vector2 velocity = DodgeDirection.normalized * DodgeSpd * speedMultiplier;

        rb.velocity = velocity;

        DodgeElapsedTime += Time.fixedDeltaTime;

        //rb.velocity = DodgeDirection.normalized * DodgeSpd;
    }

    public override void hit()
    {
        if (weaponManager.Attack(lookDirection))
        {
            Vector2 vec = new Vector2(lookDirection.x * hitForce, lookDirection.y * hitForce);
            rb.AddForce(vec, ForceMode2D.Impulse);

            // SFX player attacking
            // AudioSource source = Instantiate(audioSource, transform.position, Quaternion.identity);
            sfxManager.PlaySound("attack");
        }
        currentState = State.Move;
        UpdateVelocity();
    }

    public override void parry()
    {
        if (!isParrying)
        {
            weaponManager.Parry();
            isParrying = true;
            rb.velocity = Vector3.zero;
            playerAnim.SetBool("run", false);
            //currentState = State.Idle;

            // parry sfx
            sfxManager.PlaySound("parry_proc");

            StartCoroutine(ParryStun());
        }
    }


    void UpdateVelocity()
    {
        Vector3 velocity = rb.velocity;

        float maxSpeedChange = acceleration * Time.deltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, Direction.x * MaxSpeed, maxSpeedChange);
        velocity.y = Mathf.MoveTowards(velocity.y, Direction.y * MaxSpeed, maxSpeedChange);

        rb.velocity = velocity;
    }

    #endregion

    void FixedUpdate()
    {
        if (CanInteract())
            NearestInteractables();
        if (!inEvent)
        {
            switch (currentState)
            {
                case State.Idle:
                    Idle();
                    break;
                case State.Move:
                    Move();
                    break;
                case State.Attacking:
                        hit();
                    break;
                case State.Parrying:
                    if(hasWeapon())
                        parry();
                    break;
                case State.Dodging:
                    Dodge();
                    break;
                default:
                    break;
            }
            weaponManager.UpdateRotation(lookDirection);
        }
        //rotatesword()

        //UpdateVelocity();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public bool hasWeapon()
    {
        return weaponManager.weapon != null;
    }

    public bool CanInteract()
    {
        return (currentState != State.Attacking) && (currentState != State.Parrying) && (currentState != State.Dodging);
    }

    #region COROUTINES
    IEnumerator ParryStun()
    {
        yield return new WaitForSeconds(weaponManager.getParryStun());
        isParrying = false;
        currentState = lastState;
        if (currentState == State.Move)
        {
            playerAnim.SetBool("run", true);
        }
        yield return null;
    }
    #endregion
    #region DEBUG
    public string GetCurrentState()
    {
        return currentState.ToString();
    }
    #endregion

}
