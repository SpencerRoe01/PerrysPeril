using System.Collections;
using UnityEngine;

public class BossClass : MonoBehaviour
{
    public float Health;
    public float MaxHealth;
    public int BossPhase;
    public GameObject Player;
    bool isShooting;

    public GameObject Projectile1;

    void Start()
    {
        StartCoroutine(Boss());
    }

    
    IEnumerator Boss()
    {
        while ((MaxHealth / Health) > 0.7f ){


            yield return new WaitForSeconds(1f);

            if (!isShooting) 
            {
                StartCoroutine(ShootRoutine(75, 10, false, 2, 3, false, Projectile1, 10, 5));
                Debug.Log("Shoot");
            }
            while (isShooting) 
            { 
                yield return new WaitForSeconds(.5f);
            }

           
        }

       
        yield break;
    }

    private IEnumerator ShootRoutine(float AngleSpread, int ProjectilesPerBurst, bool Stagger , float TimeBetweenBursts , int BurstCount , bool Oscillate, GameObject BulletPrefab, float BulletMoveSpeed, float RestTime)
    {
        Debug.Log("1");
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
        Debug.Log("2");
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

}
