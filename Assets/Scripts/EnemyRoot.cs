using Pathfinding;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyRoot : MonoBehaviour
{
    [Header("Stats")]
    public int Health;
    public float MoveSpeed;
    public bool IsStunned;
    public float StunDuration;
    public bool IsRoamingEnemy;
    public bool IsArmored;

    [Header("I-Frames")]
    // fixed 1-second invincibility after taking damage
    private float invincibilityCounter = 0f;

    [Header("References")]
    public Animator Animator;
    public LevelManager LevelManager;
    public AIPath AiPath;
    private Coroutine stunCoroutine;

    public GameObject DeathParticle;

    private void Start()
    {
        // Set move speed
        transform.parent.gameObject.GetComponent<AIPath>().maxSpeed = MoveSpeed;
        transform.parent.GetComponent<Shooter>().Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {

        LevelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        // Countdown i-frames
        if (invincibilityCounter > 0f)
            invincibilityCounter -= Time.deltaTime;

        // Armored visual toggle
        if (IsArmored)
        {
            transform.parent.GetChild(1).gameObject.SetActive(Health > 1);
        }

        // Flip to face player
        Transform parent = transform.parent;
        Vector3 parentScale = parent.localScale;
        float xScale = Mathf.Abs(parentScale.x);
        var playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        parent.localScale = new Vector3(
            playerPos.x > transform.position.x ? xScale : -xScale,
            parentScale.y,
            parentScale.z);

        // Animation parameters
        Animator.SetBool("moving", !AiPath.reachedDestination);

        // Exploding enemy override
        var explodeRad = transform.parent.Find("ExplodeRad");
        if (explodeRad != null && explodeRad.GetComponent<ExplodingEnemyScript>().Exploding)
        {
            Health = 1;
        }

        // Check stun / death
        if (Health <= 0 && !IsStunned)
        {
            TriggerStun();
        }
    }

    private void TriggerStun()
    {
        IsStunned = true;
        GameObject.Find("ComboManager").GetComponent<Combo>().RegisterStun();

        // Disable visuals & AI
        GetComponent<SpriteRenderer>().enabled = true;
        foreach (Behaviour beh in transform.parent.GetComponents<Behaviour>())
            beh.enabled = false;
        transform.parent.GetComponent<SpriteRenderer>().enabled = false;

        // Start stun recovery
        if (stunCoroutine == null)
            stunCoroutine = StartCoroutine(StunCoroutine(StunDuration));

        Health = 1; // survive with 1 health
    }

    private IEnumerator StunCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        IsStunned = false;

        // Restore AI & visuals
        GetComponent<SpriteRenderer>().enabled = false;
        foreach (Behaviour beh in transform.parent.GetComponents<Behaviour>())
            beh.enabled = true;
        transform.parent.GetComponent<SpriteRenderer>().enabled = true;

        stunCoroutine = null;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            var proj = other.GetComponent<Projectile>();
            bool isPlayerProj = (proj != null && !proj.isEnemyProjectile) || proj == null;

            if (isPlayerProj && invincibilityCounter <= 0f)
            {
                // Take damage and start i-frames
                Health -= 1;
                invincibilityCounter = 1f;

                if (proj != null)
                    proj.DestoyProjectile();

                // If killed outright
                
            }
        }
    }

    public void DestroyEnemy(bool explode)
    {
        LevelManager.EnemiesInScene.Remove(transform.parent.gameObject);

        if (!explode)
        {
            GameObject p = Instantiate(DeathParticle, transform.position, transform.rotation);
            GameObject.Find("ComboManager").GetComponent<Combo>().RegisterKill();
            Destroy(p, 2f);
        }
        Destroy(transform.parent.gameObject);
    }

   
}
