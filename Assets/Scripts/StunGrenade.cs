using UnityEngine;

public class StunGrenade : MonoBehaviour
{
    public float cooldownDuration = 2f;
    private bool isOnCooldown = false;
    private float cooldownTimer = 0f;
    public GameObject stunGrenade;
    public float shootForce = 10f;

    void Update()
    {
        
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isOnCooldown = false;
                Debug.Log("Cooldown ended. Ready again.");
            }
        }

        
        if (Input.GetMouseButtonDown(2) && !isOnCooldown)
        {
            ShootGrenade();
            isOnCooldown = true;
            cooldownTimer = cooldownDuration;
        }
    }

    void ShootGrenade()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        GameObject projectile = Instantiate(stunGrenade, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(direction * shootForce, ForceMode2D.Impulse);
        }
    }
       
    }



   
   


