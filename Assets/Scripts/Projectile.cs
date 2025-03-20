using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float MoveSpeed = 22f;
    public GameObject ParticleOnHitPrefab;
    public bool isEnemyProjectile = true;
    public float ProjectileRange = 10f;

    private Vector3 StartPosition;

    public Vector2 MoveDirection;



    void Start()
    {
        StartPosition = transform.position;
        Destroy(gameObject, ProjectileRange);
        MoveDirection = transform.right;
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
        transform.Translate(MoveDirection * MoveSpeed * Time.deltaTime, Space.World);
    }
}
