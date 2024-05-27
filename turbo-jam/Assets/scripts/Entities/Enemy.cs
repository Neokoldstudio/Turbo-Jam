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
    public weaponManager weapon;
    public GameObject sprite;

    public Animator enemyAnim;
    public SfxManager sfxManager;

    private AudioSource audioSource;

    bool dead = false;

    private State currentState;
    private enum State{
        Idle,
        Move,
        Attacking,
        Dead
    }

    private void Start()
    {
    }


    private void Awake()
    { 
        currentState = State.Idle;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        sfxManager = GetComponent<SfxManager>();
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

    private void Update()
    {
        if (player != null)
        {
            // Calculate direction to player
            Vector3 directionToPlayer = player.position - transform.position;
            direction = new Vector2(directionToPlayer.x, directionToPlayer.y).normalized;
            lookDirection = direction;

            // Check if player is within attack range
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                currentState = State.Attacking;
            }
            else
            {
                currentState = State.Move;
            }
        }
    }

    private void Move()
    {
        if (!enemyAnim.GetBool("run"))
        {
            enemyAnim.SetBool("run", true);
        }

        if (Mathf.Sign(sprite.transform.localScale.x) != Mathf.Sign(direction.x) && direction.x != 0.0f)
            sprite.transform.localScale = new Vector3(Mathf.Sign(direction.x) * spriteSize, sprite.transform.localScale.y, sprite.transform.localScale.z);
    }

    private void Idle()
    {
        // Idle behavior hereif
        if(!enemyAnim.GetBool("run"))
        {
            enemyAnim.SetBool("run", false);
        }
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
        maxSpeed = 0;
        print("ouch");
        enemyAnim.SetTrigger("dead");
        TimeManager.Instance.SlowTimeSmooth(.01f, .2f, .01f);
        weapon.GetComponent<Animator>().SetTrigger("dead");
        dead = true;
        GetComponent<Collider2D>().isTrigger = true;

        rb.AddForce(new Vector2(Direction.x * hitForce, Direction.y * hitForce),ForceMode2D.Impulse);

        // hurt SFX plays 
        sfxManager.PlaySound("hurt");
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

    void UpdateVelocity()
    {
        Vector3 velocity = rb.velocity;

        float maxSpeedChange = acceleration * Time.deltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, direction.x * maxSpeed, maxSpeedChange);
        velocity.y = Mathf.MoveTowards(velocity.y, direction.y * maxSpeed, maxSpeedChange);

        rb.velocity = velocity;
    }

    void FixedUpdate()
    {
        if (!dead)
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
            weapon.UpdateRotation(lookDirection);
        }
        UpdateVelocity();
    }

    //Coroutines
    IEnumerator AttackStun()
    {
        yield return new WaitForSeconds(weapon.GetComponent<weaponManager>().getParryStun());
        currentState = State.Move;
    }
}
