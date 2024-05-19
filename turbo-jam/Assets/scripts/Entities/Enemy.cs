using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    private Transform player;
    private Vector2 direction = Vector2.zero;
    private Vector2 lookDirection = Vector2.zero;

    [SerializeField, Range(0f, 100f)]
    private float maxSpeed = 5f;
    private float acceleration = 10f;

    [SerializeField, Range(0f, 100f)]
    private float attackRange = 2f;

    [SerializeField, Range(0f, 100f)]
    private float hitForce = 5f;


    [SerializeField, Range(0f, 100f)]
    private float rotationSpeed = 5f;

    private float spriteSize;
    public GameObject weapon;
    public GameObject sprite;

    public Animator enemyAnim;

    [SerializeField] AudioClip SFX_hurt;
    [SerializeField] AudioClip SFX_death;
    [SerializeField] AudioClip SFX_attack;

    private AudioSource audioSource;

    private State currentState;
    private enum State{
        Idle,
        Move,
        Attacking,
        Dead
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void Awake()
    { 
        currentState = State.Idle;
        rb = GetComponent<Rigidbody2D>();
        spriteSize = sprite.transform.localScale.x;

        // Find the player by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }

    private void OnEnable()
    {
        currentState = State.Move;
    }

    private void Move()
    {
        Vector3 velocity = rb.velocity;

        float maxSpeedChange = acceleration * Time.deltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, direction.x * maxSpeed, maxSpeedChange);
        velocity.y = Mathf.MoveTowards(velocity.y, direction.y * maxSpeed, maxSpeedChange);

        rb.velocity = velocity;

        if (Mathf.Sign(sprite.transform.localScale.x) != Mathf.Sign(direction.x) && direction.x != 0.0f)
            sprite.transform.localScale = new Vector3(Mathf.Sign(direction.x) * spriteSize, sprite.transform.localScale.y, sprite.transform.localScale.z);

        // Rotate weapon
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        weapon.transform.localScale = new Vector3(Mathf.Sign(direction.x), 1.0f, weapon.transform.localScale.z);


        if (player != null)
        {
            // Calculate direction to player
            Vector3 directionToPlayer = player.position - transform.position;
            direction = new Vector2(directionToPlayer.x, directionToPlayer.y).normalized;

            // Cast a ray in the direction the enemy is facing
            RaycastHit hit;
            Vector3 rayDirection = new Vector3(direction.x, 0, 0);

            if (Physics.Raycast(transform.position, rayDirection, out hit, attackRange))
            {
                if (hit.collider.transform == player)
                {
                    print("attack !!!");
                    currentState = State.Attacking;
                }
            }
        }
    }

    private void Idle()
    {
        // Idle behavior here
        StartCoroutine(AttackStun());
    }

    public override void hit()
    {
        if (weapon.GetComponent<weaponManager>().Attack(lookDirection))
        {
            rb.AddForce(new Vector2(lookDirection.x * hitForce, lookDirection.y * hitForce),ForceMode2D.Impulse);
        }
        currentState = State.Move;
    }

    public override void parry()
    {
        // Enemies might not parry
    }

    public override void getHit(int Damage, Vector2 Direction)
    {
        // Handle getting hit
        currentState = State.Idle;
        print("ouch");
        rb.AddForce(new Vector2(Direction.x * hitForce, Direction.y * hitForce),ForceMode2D.Impulse);

        // hurt SFX plays 
        audioSource.clip = SFX_hurt;
        audioSource.Play();
    }

    private void Attack()
    {
        // Attack behavior here
        if (weapon.GetComponent<weaponManager>().Attack(direction))
        {
            rb.AddForce(new Vector2(direction.x * hitForce, direction.y * hitForce),ForceMode2D.Impulse);

            // attack sfx plays
            // ...
        }
        currentState = State.Idle;
    }

    void FixedUpdate()
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
                Attack();
                break;
            default:
                break;
        }
    }

    //Coroutines
    IEnumerator AttackStun()
    {
        yield return new WaitForSeconds(weapon.GetComponent<weaponManager>().getParryStun());
        currentState = State.Move;
    }
}
