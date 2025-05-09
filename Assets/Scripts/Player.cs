using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float PlayerMoveSpeed = 5f;       // Normal movement speed
    public float SprintSpeed = 8f;           // Sprinting movement speed
    private float PlayerMovementX = 0f;
    private float PlayerMovementY = 0f;

    public float ActiveMoveSpeed;
    public float DashSpeed = 15f;            // Dash speed

    public float DashLength = 0.5f, DashCooldown = 1f;
    public float DashCounter;
    public float DashCoolCounter;

    public int Health;
    public bool IsDashing;

    private bool KilledAnEnemyOnDash;
    private Vector2 dashDirection;

    public GameObject SoundManager;

    void Start()
    {
        ActiveMoveSpeed = PlayerMoveSpeed;
    }

    void Update()
    {
        // Only update movement inputs if not dashing
        if (DashCounter <= 0)
        {
            PlayerMovementX = Input.GetAxisRaw("Horizontal");
            PlayerMovementY = Input.GetAxisRaw("Vertical");
        }

        // Dash initiation (using space key)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (DashCoolCounter <= 0 && DashCounter <= 0)
            {
                SoundManager = GameObject.Find("SoundManager");
                SoundManager.GetComponent<SoundManager>().PlayDashWoosh();
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dashDirection = ((Vector2)mousePos - (Vector2)transform.position).normalized;
                ActiveMoveSpeed = DashSpeed;
                DashCounter = DashLength;
            }
        }

        // Handle dashing
        if (DashCounter > 0)
        {
            IsDashing = true;
            DashCounter -= Time.deltaTime;
            GetComponent<Rigidbody2D>().linearVelocity = dashDirection * DashSpeed;

            if (DashCounter <= 0)
            {
                // Reset to normal speed when dash ends
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

            // Sprinting: If the player holds Left Shift, use SprintSpeed
            if (Input.GetKey(KeyCode.LeftShift))
            {
                ActiveMoveSpeed = SprintSpeed;
            }
            else
            {
                ActiveMoveSpeed = PlayerMoveSpeed;
            }

            // Regular movement (sprint or normal) using current input values
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(PlayerMovementX * ActiveMoveSpeed, PlayerMovementY * ActiveMoveSpeed);
        }

        // Dash cooldown timer
        if (DashCoolCounter > 0)
        {
            DashCoolCounter -= Time.deltaTime;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        try
        {
            if (other.gameObject.tag == "Projectile")
            {
                var projectile = other.gameObject.GetComponent<Projectile>();

                if (projectile != null && projectile.isEnemyProjectile == true && !IsDashing && !GetComponent<Parry>().PerfectParryStunCollider.enabled)
                {
                    Health -= 1;
                    projectile.DestoyProjectile();
                }
            }
        }
        catch (NullReferenceException e)
        {
            Debug.Log(other);
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
