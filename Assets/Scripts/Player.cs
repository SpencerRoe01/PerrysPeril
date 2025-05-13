using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float PlayerMoveSpeed = 5f;
    public float SprintSpeed = 8f;
    public float DashSpeed = 15f;

    [Header("Dash Settings")]
    public float DashLength = 0.5f;
    public float DashCooldown = 1f;

    [Header("Health & I-Frames")]
    public int Health = 3;
    public float InvincibilityDuration = 1f; // seconds of invincibility after taking damage

    private float playerMovementX = 0f;
    private float playerMovementY = 0f;
    private float activeMoveSpeed;
    private float dashCounter;
    private float dashCoolCounter;
    private float invincibilityCounter;

    public bool IsDashing { get; private set; }

    private bool killedAnEnemyOnDash;
    private Vector2 dashDirection;

    public GameObject SoundManager;
    public GameObject[] Hearts;

    void Start()
    {
        activeMoveSpeed = PlayerMoveSpeed;
        invincibilityCounter = 0f;
    }

    void Update()
    {
        // Update hearts UI
        for (int i = 0; i < Hearts.Length; i++)
            Hearts[i].SetActive(Health > i);

        // Countdown timers
        if (dashCoolCounter > 0)
            dashCoolCounter -= Time.deltaTime;
        if (invincibilityCounter > 0)
            invincibilityCounter -= Time.deltaTime;

        if (!IsDashing)
        {
            // Allow normal movement
            playerMovementX = Input.GetAxisRaw("Horizontal");
            playerMovementY = Input.GetAxisRaw("Vertical");

            // Handle sprint
            activeMoveSpeed = Input.GetKey(KeyCode.LeftShift) ? SprintSpeed : PlayerMoveSpeed;

            // Handle dash input
            if (Input.GetKeyDown(KeyCode.Space) && dashCoolCounter <= 0f)
            {
                SoundManager = GameObject.Find("SoundManager");
                SoundManager.GetComponent<SoundManager>().PlayDashWoosh();

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dashDirection = ((Vector2)mousePos - (Vector2)transform.position).normalized;

                IsDashing = true;
                dashCounter = DashLength;
                invincibilityCounter = DashLength;
                GetComponent<Rigidbody2D>().linearVelocity = dashDirection * DashSpeed;
            }
            else
            {
                // Apply normal movement
                GetComponent<Rigidbody2D>().linearVelocity = new Vector2(playerMovementX * activeMoveSpeed, playerMovementY * activeMoveSpeed);
            }
        }
        else // Currently dashing
        {
            GetComponent<Rigidbody2D>().linearVelocity = dashDirection * DashSpeed;
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0f)
            {
                IsDashing = false;
                activeMoveSpeed = PlayerMoveSpeed;
                if (!killedAnEnemyOnDash)
                {
                    dashCoolCounter = DashCooldown;
                }
                killedAnEnemyOnDash = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Projectile damage
        if (other.CompareTag("Projectile"))
        {
            var projectile = other.GetComponent<Projectile>();
            bool parryActive = GetComponent<Parry>().PerfectParryStunCollider.enabled;

            if (projectile != null && projectile.isEnemyProjectile && invincibilityCounter <= 0f && !parryActive)
            {
                // Take damage and start i-frames
                Health -= 1;
                invincibilityCounter = InvincibilityDuration;
                projectile.DestoyProjectile();
            }
        }

        // Dash kill logic
        if (other.CompareTag("Enemy"))
        {

            var enemy = other.GetComponent<EnemyRoot>();
            if (enemy != null && enemy.IsStunned && IsDashing)
            {
                enemy.DestroyEnemy(false);
                dashCoolCounter = 0f;
                killedAnEnemyOnDash = true;
            }
        }
        if (other.CompareTag("Boss"))
        {
            Destroy(other.gameObject);
        }
    }
}
