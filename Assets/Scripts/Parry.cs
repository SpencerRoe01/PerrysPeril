using UnityEngine;
using System.Collections;

public class Parry : MonoBehaviour
{

    public Collider2D WideParryCollider;

    public Collider2D ParryCollider;
    public Collider2D PerfectParryCollider;
    public Collider2D PerfectParryStunCollider;

    public Animator SwordAnimator;
    public float colliderActiveTime = 0.5f;
    public GameObject SoundManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SwordAnimator == null)
        {
            SwordAnimator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SwordAnimator.SetTrigger("WideSwing");
            WideParryCollider.enabled = true;
            SoundManager = GameObject.Find("SoundManager");
            SoundManager.GetComponent<SoundManager>().PlaySwordSlash();
            StartCoroutine(DisableColliderAfterDelay(WideParryCollider, colliderActiveTime));

        }
        if (Input.GetMouseButtonDown(1))
        {
            SwordAnimator.SetTrigger("Parry");
            PerfectParryCollider.enabled = true;
            ParryCollider.enabled = true;
            SoundManager = GameObject.Find("SoundManager");
            SoundManager.GetComponent<SoundManager>().PlaySwordSlash();
            StartCoroutine(DisableColliderAfterDelay(ParryCollider, colliderActiveTime));
            StartCoroutine(DisableColliderAfterDelay(PerfectParryCollider, colliderActiveTime));
        }
    }


    public void ParryMeathod(string name, Collider2D other)
    {

        if (name == "WideParryCollider" && other.gameObject.GetComponent<Projectile>().MoveDirection != null)
        {

            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 NewDirection = (MousePos - gameObject.transform.position).normalized;
            other.gameObject.GetComponent<Projectile>().MoveDirection = NewDirection;
            other.gameObject.GetComponent<Projectile>().MoveSpeed = other.gameObject.GetComponent<Projectile>().MoveSpeed + 5;
            other.gameObject.GetComponent<Projectile>().isEnemyProjectile = false;
        }
        if (name == "PerfectParryCollider")
        {
            Debug.Log("Perfect!");
            GameObject.Find("ComboManager").GetComponent<Combo>().RegisterParry();
            other.gameObject.GetComponent<Projectile>().isEnemyProjectile = false;
            PerfectParryStunCollider.enabled = true;
            StartCoroutine(DisableColliderAfterDelay(PerfectParryStunCollider, .5f));


        }
        if (name == "RegularParryCollider")
        {

            if (other.gameObject.GetComponent<Projectile>().EnemyWhoShotProjectile != null)
            {
                Vector2 NewDirection = (other.gameObject.GetComponent<Projectile>().EnemyWhoShotProjectile.transform.position - gameObject.transform.position).normalized;
                other.gameObject.GetComponent<Projectile>().MoveDirection = NewDirection;
                other.gameObject.GetComponent<Projectile>().MoveSpeed = other.gameObject.GetComponent<Projectile>().MoveSpeed + 10;
                PerfectParryCollider.enabled = false;
                other.gameObject.GetComponent<Projectile>().isEnemyProjectile = false;
            }
            else 
            {
                Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 NewDirection = (MousePos - gameObject.transform.position).normalized;
                other.gameObject.GetComponent<Projectile>().MoveDirection = NewDirection;
                other.gameObject.GetComponent<Projectile>().MoveSpeed = other.gameObject.GetComponent<Projectile>().MoveSpeed + 5;
                other.gameObject.GetComponent<Projectile>().isEnemyProjectile = false;
            }

        }
    }

    IEnumerator DisableColliderAfterDelay(Collider2D col, float delay)
    {
        yield return new WaitForSeconds(delay);
        col.enabled = false;
    }
    
}
