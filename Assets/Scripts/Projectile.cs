using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float MoveSpeed = 22f;
    public GameObject ParticleOnHitPrefab;
    public bool isEnemyProjectile = true;
    public float ProjectileRange = 10f;

    private Vector3 StartPosition;



    void Start()
    {
        StartPosition = transform.position;
        Destroy(gameObject, ProjectileRange);
    }
    void Update()
    {
        MoveProjectile();

    }

    public bool GetIsEnemyProjectile()
    {
        return isEnemyProjectile;
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.MoveSpeed = moveSpeed;
    }



    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
    }
}
