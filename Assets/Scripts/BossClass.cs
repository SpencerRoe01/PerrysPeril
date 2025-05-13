using Pathfinding;
using System.Collections;
using UnityEngine;

public class BossClass : MonoBehaviour
{
    [Header("Health & I-Frames")]
    public float MaxHealth = 10f;
    [Tooltip("Seconds of invincibility after taking damage")] public float InvincibilityDuration = 1f;

    public float health;
    private float invincibilityCounter = 0f;

    [Header("References")]
    public GameObject Player;
    public GameObject Projectile1;
    public Rigidbody2D rb;
    private AIPath aiPath;

    [Header("Dash Settings")]
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashPause = 0.5f;
    public int dashCount = 3;
    private bool isDashing = false;

    private bool isShooting = false;

    public GameObject StunnedBoss;

    void Awake()
    {
        health = MaxHealth;
        aiPath = GetComponent<AIPath>();
    }

    void Update()
    {
        Player = GameObject.Find("PerryRoot").gameObject;
        if (health <= 0) 
        {
            Instantiate(StunnedBoss, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }


        // Countdown invincibility timer
        if (invincibilityCounter > 0f)
            invincibilityCounter -= Time.deltaTime;
    }

    void Start()
    {
        StartCoroutine(Boss());
    }

    IEnumerator Boss()
    {
        // Phase 1: ranged attacks
        while (health / MaxHealth > 0.7f)
        {
            yield return new WaitForSeconds(1f);
            if (!isShooting)
                StartCoroutine(ShootRoutine(75, 10, false, 1.5f, 5, false, Projectile1, 20, .5f));

            while (isShooting)
                yield return new WaitForSeconds(.5f);

            yield return new WaitForSeconds(.5f);
            if (!isShooting)
                StartCoroutine(ShootRoutine(359, 35, true, .5f, 2, true, Projectile1, 15, .2f));

            while (isShooting)
                yield return new WaitForSeconds(.5f);
        }

        // Phase 2: dashing
        while (health / MaxHealth <= 0.6f)
        {
            yield return new WaitForSeconds(2f);
            if (!isDashing)
                StartCoroutine(DashRoutine());
            yield return new WaitForSeconds(5f);
        }
    }

    private IEnumerator ShootRoutine(float AngleSpread, int ProjectilesPerBurst, bool Stagger, float TimeBetweenBursts,
        int BurstCount, bool Oscillate, GameObject BulletPrefab, float BulletMoveSpeed, float RestTime)
    {
        isShooting = true;
        isShooting = true;

        float StartAngle, CurrentAngle, AngleStep, EndAngle;
        float TimeBetweenProjectiles = 0;


        TargetConeOfInfluence(out StartAngle, out CurrentAngle, out AngleStep, out EndAngle, AngleSpread, ProjectilesPerBurst);
        if (Stagger)
        {
            TimeBetweenProjectiles = TimeBetweenBursts / ProjectilesPerBurst;
        }

        for (int i = 0; i < BurstCount; i++)
        {
            /*if (transform.GetChild(0).GetComponent<EnemyRoot>().IsStunned)
            {
                break;
            }
            */
            if (!Oscillate)
            {
                TargetConeOfInfluence(out StartAngle, out CurrentAngle, out AngleStep, out EndAngle, AngleSpread, ProjectilesPerBurst);
            }
            if (Oscillate && i % 2 != 1)
            {
                TargetConeOfInfluence(out StartAngle, out CurrentAngle, out AngleStep, out EndAngle, AngleSpread, ProjectilesPerBurst);
            }
            else if (Oscillate)
            {
                CurrentAngle = EndAngle;
                EndAngle = StartAngle;
                StartAngle = CurrentAngle;
                AngleStep *= -1;
            }


            for (int j = 0; j < ProjectilesPerBurst; j++)
            {


                Vector2 Position = FindBulletSpawnPosition(CurrentAngle);

                GameObject NewBullet = Instantiate(BulletPrefab, Position, Quaternion.identity);
                NewBullet.GetComponent<Projectile>().EnemyWhoShotProjectile = gameObject;

                NewBullet.transform.right = NewBullet.transform.position - transform.position;

                if (NewBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(BulletMoveSpeed);
                }

                CurrentAngle += AngleStep;

                if (Stagger)
                {
                    yield return new WaitForSeconds(TimeBetweenProjectiles);
                }
            }

            CurrentAngle = StartAngle;
            yield return new WaitForSeconds(TimeBetweenBursts);



        }

        yield return new WaitForSeconds(RestTime);
        isShooting = false;

        
    }

    private void TargetConeOfInfluence(out float StartAngle, out float CurrentAngle, out float AngleStep, out float EndAngle, float AngleSpread, int ProjectilesPerBurst)
    {
        Vector2 TargetDirection = Player.transform.position - transform.position;
        float TargetAngle = Mathf.Atan2(TargetDirection.y, TargetDirection.x) * Mathf.Rad2Deg;
        StartAngle = TargetAngle;
        EndAngle = TargetAngle;
        CurrentAngle = TargetAngle;
        float HalfAngleSpread = 0f;
        AngleStep = 0f;

        if (AngleSpread != 0)
        {
            AngleStep = AngleSpread / (ProjectilesPerBurst - 1);
            HalfAngleSpread = AngleSpread / 2f;
            StartAngle = TargetAngle - HalfAngleSpread;
            EndAngle = TargetAngle + HalfAngleSpread;
            CurrentAngle = StartAngle;
        }
    }


    private Vector2 FindBulletSpawnPosition(float CurrentAngle)
    {
        float x = transform.position.x + 1 * Mathf.Cos(CurrentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + 1 * Mathf.Sin(CurrentAngle * Mathf.Deg2Rad);

        Vector2 Position = new Vector2(x, y);
        return Position;
    }

    


    IEnumerator DashRoutine()
    {
        aiPath.enabled = false;
        isDashing = true;

        for (int i = 0; i < dashCount; i++)
        {
            Vector2 direction = (Player.transform.position - transform.position).normalized;
            // Flip sprite to face direction
            transform.localScale = new Vector3(Mathf.Sign(direction.x) * 2, 2, 2);

            float elapsed = 0f;
            while (elapsed < dashDuration)
            {
                rb.MovePosition(rb.position + direction * dashSpeed * Time.fixedDeltaTime);
                elapsed += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            BossSlam();
            yield return new WaitForSeconds(dashPause);
        }

        isDashing = false;
        aiPath.enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            var proj = other.GetComponent<Projectile>();
            bool isPlayerProjectile = (proj != null && !proj.isEnemyProjectile) || proj == null;

            if (isPlayerProjectile && invincibilityCounter <= 0f)
            {
                TakeDamage(1f);
                if (proj != null)
                    proj.DestoyProjectile();
            }
        }
    }

    private void TakeDamage(float amount)
    {
        health -= amount;
        invincibilityCounter = InvincibilityDuration;
        // TODO: add flash or feedback here
        
    }

    

    private void BossSlam()
    {
        StartCoroutine(ShootRoutine(359, 35, false, 0f, 1, true, Projectile1, 15f, .2f));
    }
}
