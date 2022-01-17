using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player Stats
    public float speed = 1f;
    public int maxHealth = 5;
    public int coin;
    public float timeInvincible = 1.0f;
    public GameObject bulletPrefab;
    public float shootingCooldown;
    public int maxBullets;


    float bulletForce = 6f;
    public int health { get { return currentHealth; } }
    int currentHealth;

    public int bullet { get { return currentBullet; } }
    int currentBullet;

    bool isInvincible;
    float invincibleTimer;


    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    Vector3 mouse_pos;
    Vector3 object_pos;
    bool isShooting;

    Rigidbody2D rbPlayer;
    float horizontal;
    float vertical;


    float timer;
    float timerShoot;


    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        timer = Time.time;

        currentBullet = maxBullets;
        currentHealth = maxHealth;
    }


    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(horizontal, vertical);

        mouse_pos = Input.mousePosition;
        object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        Vector3 looking = new Vector3(mouse_pos.x, mouse_pos.y, 0);

        if (!Mathf.Approximately(looking.x, 0.0f) || !Mathf.Approximately(looking.y, 0.0f))
        {
            lookDirection.Set(looking.x, looking.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Move X", looking.x);
        animator.SetFloat("Move Y", looking.y);
        animator.SetFloat("Speed", move.magnitude);
        animator.SetBool("isShooting", isShooting);

        timerShoot -= 1;
        if (isShooting)
        {
            if (timerShoot < 0)
            {
                isShooting = false;
            }
        }

        //Invincible when takes damage
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        //Shoot
        timer += Time.deltaTime;
        if (timer >= shootingCooldown && currentBullet > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                timer = 0;
            }
        }

        //Reload
        if (currentBullet == 0)
        {
            if (Input.GetKeyDown("r"))
                ChangeBullet(maxBullets);
        }



    }
    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rbPlayer.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

         currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    void Shoot()
    {
        timerShoot = 10f;
        isShooting = true;
        ChangeBullet(-1);
        GameObject bullet = Instantiate(bulletPrefab, rbPlayer.transform);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.AddForce(lookDirection * bulletForce, ForceMode2D.Impulse);
    }

    void ChangeBullet(int amout)
    {
        currentBullet = Mathf.Clamp(currentBullet + amout, 0, maxBullets);
    }
    
}

/*player temp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 1f;
    public int maxHealth = 5;
    public int coin;
    public float timeInvincible = 1.0f;
    public GameObject bulletPrefab;
    public float shootingCooldown;
    public float bulletForce;
    public int maxBullets;

    

    public int health { get { return currentHealth; } }
    int currentHealth;

    public int bullet { get { return currentBullet; } }
    int currentBullet;

    bool isInvincible;
    float invincibleTimer;


    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    public bool isShooting;

    Rigidbody2D rbPlayer;
    float horizontal;
    float vertical;

    float timer;
    
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        timer = Time.time;

        currentBullet = maxBullets;
        currentHealth = maxHealth;
    }

    
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");


        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f) )
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
            animator.SetFloat("Move X", lookDirection.x);
            animator.SetFloat("Move Y", lookDirection.y);
            animator.SetFloat("Speed", move.magnitude);
            animator.SetBool("isShooting", isShooting);


        //Invincible when takes time
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        //Shoot
        timer += Time.deltaTime;
        if (timer >= shootingCooldown && currentBullet > 0)
        {
            if (Input.GetKeyDown("up"))
            {
                animator.SetFloat("Move X", 0);
                animator.SetFloat("Move Y", 1);
                Shoot(1);
                timer = 0;
            }
            if (Input.GetKeyDown("down"))
            {
                animator.SetFloat("Move X", 0);
                animator.SetFloat("Move Y", -1);
                Shoot(2);
                timer = 0;
            }
            if (Input.GetKeyDown("left"))
            {
                animator.SetFloat("Move X", -1);
                animator.SetFloat("Move Y", 0);
                Shoot(3);
                timer = 0;
            }
            if (Input.GetKeyDown("right"))
            {
                animator.SetFloat("Move X", 1);
                animator.SetFloat("Move Y", 0);
                Shoot(4);
                timer = 0;
            }
        }

            

        //Reload
        if (currentBullet == 0)
        {
            if (Input.GetKeyDown("r"))
                ChangeBullet(maxBullets);
        }


    }
    void FixedUpdate()
    {
        isShooting = false;
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal  * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rbPlayer.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    void Shoot(int n)
    {
        isShooting = true;
        ChangeBullet(-1);
        GameObject bullet = Instantiate(bulletPrefab, rbPlayer.transform);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        switch (n)
        {
            case 1:
                rbBullet.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
                break;
            case 2:
                rbBullet.AddForce(-transform.up * bulletForce, ForceMode2D.Impulse);
                break;
            case 3:
                rbBullet.AddForce(-transform.right * bulletForce, ForceMode2D.Impulse);
                break;
            case 4:
                rbBullet.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
                break;
        }
    }

    void ChangeBullet(int amout)
    {
        currentBullet = Mathf.Clamp(currentBullet + amout, 0, maxBullets);
        Debug.Log("Bullets" + currentBullet + "/" + maxBullets);
    }


    
}
*/