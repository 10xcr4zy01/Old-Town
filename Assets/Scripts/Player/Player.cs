using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    //Player Stats
    public float speed;
    public int maxHealth;
    public int money;
    public float attackSpeed;
    public int maxBullets;

    
    [SerializeField] GameObject bulletPrefab, aniReloading;
    [SerializeField] SFXManager sfx;
    
    //Flash
    [SerializeField] Material flashMaterial;
    Material originalMaterial;
    SpriteRenderer sr;
    Coroutine flashRoutine;


    float bulletForce = 6f;
    public int health { get { return currentHealth; } }
    int currentHealth;

    public int bullet { get { return currentBullet; } }
    int currentBullet;

    //invincile
    float timeInvincible;
    bool isInvincible;

    //Animator
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    Vector3 mouse_pos;
    Vector3 object_pos;
    bool isShooting;
    
    //Moving
    Rigidbody2D rbPlayer;
    float horizontal;
    float vertical;


    //Timer 
    float attackTimer;
    float timerAnimationShoot;
    float invincibleTimer;
    float reloadTimer;
    bool isReloading;

    //Stop player attack when in safe zone
    bool isOnSafeZone;
    GameObject SafeZoneCondition;


    void Start()
    {
        LoadStats();
        rbPlayer = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();
        originalMaterial = sr.material;

        attackTimer = Time.time;
        reloadTimer = 0;

        timeInvincible = 1.5f;

        currentBullet = maxBullets;
        currentHealth = maxHealth;

        SafeZoneCondition = GameObject.Find("NPC");

        isReloading = false;
    }



    void Update()
    {
        if (SafeZoneCondition != null)
        {
            isOnSafeZone = true;
        }
        //Moving
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(horizontal, vertical);

        //Looking at mouse
        mouse_pos = Input.mousePosition;
        object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        Vector3 looking = new Vector3(mouse_pos.x, mouse_pos.y, 0);

        //Animation
        if (!Mathf.Approximately(looking.x, 0.0f) || !Mathf.Approximately(looking.y, 0.0f))
        {
            lookDirection.Set(looking.x, looking.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Move X", looking.x);
        animator.SetFloat("Move Y", looking.y);
        animator.SetFloat("Speed", move.magnitude);
        animator.SetBool("isShooting", isShooting);


        timerAnimationShoot -= 1;
        if (isShooting)
        {
            if (timerAnimationShoot < 0)
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
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackSpeed && currentBullet > 0)
        {
            if (Input.GetButtonDown("Fire1") && isOnSafeZone == false && isReloading == false)
            {
                Shoot();
                attackTimer = 0;
            }
        }

        //Reload
        reloadTimer += Time.deltaTime;
        if (currentBullet < maxBullets && isReloading == false)
        {
            if (Input.GetKeyDown("r"))
            {
                Reload();
                reloadTimer = 0;
                isReloading = true;
            }
        }
        if (reloadTimer > 1f)
        {
            isReloading = false;
        }

    }
    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rbPlayer.MovePosition(position);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && currentHealth > 0)
        {
            ChangeHealth(-1);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collectible")
        {
            sfx.PlaySound("pickup");
            money += 10;
        }
        if (collision.gameObject.layer == 11 && currentHealth > 0)
        {
            ChangeHealth(-1);
        }
    }



    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            else
            {
                Flash();
                sfx.PlaySound("damaged");
            } 
                

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

         currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    void Shoot()
    {
        sfx.PlaySound("shoot");
        timerAnimationShoot = 10f; //10 frames to keep animation
        isShooting = true; 

        ChangeBullet(-1);

        //Create bullet and force bullet
        GameObject bullet = Instantiate(bulletPrefab, rbPlayer.transform);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.AddForce(lookDirection * bulletForce, ForceMode2D.Impulse);
    }

    void ChangeBullet(int amout)
    {
        currentBullet = Mathf.Clamp(currentBullet + amout, 0, maxBullets);
    }


    void Reload ()
    {
        Destroy(Instantiate(aniReloading, new Vector2(transform.position.x, transform.position.y + 0.2f), transform.rotation), 1f);
        sfx.PlaySound("reload");
        ChangeBullet(maxBullets);
    }

    IEnumerator FlashRoutine()
    {
        sr.material = flashMaterial;
        yield return new WaitForSeconds(0.1f);
        sr.material = originalMaterial;
        flashRoutine = null;
    }

    void Flash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    public void SaveStats()
    {
        PlayerPrefs.SetFloat("speed", speed);
        PlayerPrefs.SetInt("maxHealth", maxHealth);
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetFloat("attackSpeed", attackSpeed);
        PlayerPrefs.SetInt("maxBullets", maxBullets);
    }

    public void LoadStats()
    {
        speed = PlayerPrefs.GetFloat("speed");
        maxHealth = PlayerPrefs.GetInt("maxHealth");
        money = PlayerPrefs.GetInt("money");
        attackSpeed = PlayerPrefs.GetFloat("attackSpeed");
        maxBullets = PlayerPrefs.GetInt("maxBullets");
    }
}