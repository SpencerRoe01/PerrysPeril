using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float PlayerMoveSpeed = 0f;
    private float PlayerMovementX = 0f;
    private float PlayerMovementY = 0f;

    public float ActiveMoveSpeed;
    public float DashSpeed;

    public float DashLength = .5f, DashCooldown = 1f;
    public float DashCounter;
    public float DashCoolCounter;

    public int Health;
    public bool IsDashing;

    private bool KilledAnEnemyOnDash;

    
    private Vector2 dashDirection;

    void Start()
    {
        ActiveMoveSpeed = PlayerMoveSpeed;
    }

    void Update()
    {
        
        if (DashCounter <= 0)
        {
            PlayerMovementX = Input.GetAxisRaw("Horizontal");
            PlayerMovementY = Input.GetAxisRaw("Vertical");
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (DashCoolCounter <= 0 && DashCounter <= 0)
            {
                
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                dashDirection = ((Vector2)mousePos - (Vector2)transform.position).normalized;

                ActiveMoveSpeed = DashSpeed;
                DashCounter = DashLength;
            }
        }

       
        if (DashCounter > 0)
        {
            IsDashing = true;
            DashCounter -= Time.deltaTime;

           
            GetComponent<Rigidbody2D>().linearVelocity = dashDirection * DashSpeed;

            if (DashCounter <= 0)
            {
                ActiveMoveSpeed = PlayerMoveSpeed;
                if (!KilledAnEnemyOnDash)
                {
                    DashCoolCounter = DashCooldown;
                }
                KilledAnEnemyOnDash = false;
            }
        }
        else
        {
            IsDashing = false;
            
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(PlayerMovementX * ActiveMoveSpeed, PlayerMovementY * ActiveMoveSpeed);
        }

        if (DashCoolCounter > 0)
        {
            DashCoolCounter -= Time.deltaTime;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            if (other.gameObject.GetComponent<Projectile>().isEnemyProjectile == true
                && !IsDashing
                && !GetComponent<Parry>().PerfectParryStunCollider.enabled)
            {
                Health -= 1;
                other.gameObject.GetComponent<Projectile>().DestoyProjectile();
            }
        }
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<EnemyRoot>().IsStunned && IsDashing)
            {
                other.gameObject.GetComponent<EnemyRoot>().DestroyEnemy();
                DashCoolCounter = 0;
                KilledAnEnemyOnDash = true;
            }
        }
    }
}