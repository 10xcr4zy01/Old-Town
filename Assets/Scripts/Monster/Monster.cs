using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Monster : MonoBehaviour
{
    [SerializeField] private Material flashMaterial;

    public float speed = 0.5f;
    public int maxHealth = 3;

    public int health { get { return currentHealth; } }
    int currentHealth;

    Animator animator;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Material originalMaterial;
    Coroutine flashRoutine;
    Transform target;

    void Start()
    {
        //Set target to player
        target = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        originalMaterial = sr.material;

        //GetComponent
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //Health
        currentHealth = maxHealth;
    }

    void Update()
    {
        
        //Auto chasing player
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);      
        if (currentHealth == 0)
        {
            speed = 0;
            animator.SetTrigger("die"); 
            Destroy(gameObject, 0.2f);
        }

        //Set animation

        Vector2 move = target.position - transform.position;
        animator.SetFloat("MoveX", move.normalized.x);
        animator.SetFloat("MoveY", move.normalized.y);

    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null && currentHealth > 0)
        {
            player.ChangeHealth(-1);
        }
    }

    public void ChangeHealth(int amount)
    {
        Flash();
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    IEnumerator FlashRoutine()
    {
        sr.material = flashMaterial;
        yield return new WaitForSeconds(0.1f);
        sr.material = originalMaterial;
        flashRoutine = null;
    }


    void Flash ()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(FlashRoutine());
    }
}
