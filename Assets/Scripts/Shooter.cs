using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;


public class Shooter : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject Player;
    public float BulletMoveSpeed;
    public int BurstCount;
    public float TimeBetweenBursts;

    private bool IsShooting = false;
    public float RestTime = 1f;
    public int ProjectilesPerBurst;
    [Range(0, 359)] public float AngleSpread;
    public float StartingDistance = 0.1f;
    public bool Stagger;
    public bool Oscillate;


    public AIPath AiPath;



    void Update()
    {
        if (AiPath.reachedDestination)
        {
            Attack();
        }

    }



    public void Attack()
    {
        if (!IsShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }
    private IEnumerator ShootRoutine()
    {

        IsShooting = true;

        float StartAngle, CurrentAngle, AngleStep, EndAngle;
        float TimeBetweenProjectiles = 0;


        TargetConeOfInfluence(out StartAngle, out CurrentAngle, out AngleStep, out EndAngle);
        if (Stagger)
        {
            TimeBetweenProjectiles = TimeBetweenBursts / ProjectilesPerBurst;
        }

        for (int i = 0; i < BurstCount; i++)
        {
            if (!Oscillate)
            {
                TargetConeOfInfluence(out StartAngle, out CurrentAngle, out AngleStep, out EndAngle);
            }
            if (Oscillate && i % 2 != 1)
            {
                TargetConeOfInfluence(out StartAngle, out CurrentAngle, out AngleStep, out EndAngle);
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
        IsShooting = false;
    }

    private Vector2 FindBulletSpawnPosition(float CurrentAngle)
    {
        float x = transform.position.x + StartingDistance * Mathf.Cos(CurrentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + StartingDistance * Mathf.Sin(CurrentAngle * Mathf.Deg2Rad);

        Vector2 Position = new Vector2(x, y);
        return Position;
    }

    private void TargetConeOfInfluence(out float StartAngle, out float CurrentAngle, out float AngleStep, out float EndAngle)
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

}
