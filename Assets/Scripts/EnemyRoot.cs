using Pathfinding;
using UnityEngine;
using System.Collections;

public class EnemyRoot : MonoBehaviour
{
    public int Health;
    public float MoveSpeed;
    public bool IsStunned;
    public float StunDuration;
    public bool IsRoamingEnemy;

    public Animator Animator;

    private Coroutine stunCoroutine;




    private void Start()
    {
        transform.parent.gameObject.GetComponent<AIPath>().maxSpeed = MoveSpeed;
        transform.parent.GetComponent<Shooter>().Player = GameObject.FindGameObjectWithTag("Player");

    }
    

    private void Update()
    {

        if (transform.parent.Find("ExplodeRad") != null) 
        {
            if (transform.parent.Find("ExplodeRad").GetComponent<ExplodingEnemyScript>().Exploding) 
            {
                Health = 1;
            }
        }

        if (Health <= 0) 
        {
            IsStunned = true;
        }

        if (Health <= 0)
        {
           
            //play stun animation


            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            foreach (Behaviour behaviour in transform.parent.GetComponents<Behaviour>())
            {
                behaviour.enabled = false;

            }
            transform.parent.GetComponent<SpriteRenderer>().enabled = false;


            if (stunCoroutine == null)
            {
                 stunCoroutine = StartCoroutine(StunCoroutine(StunDuration));
            }
            Health = 1;




        }
        
        
    }
    public void PlayShootAnimation()
    {
        //play Shoot animation
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile" )
        {
            if (other.gameObject.GetComponent<Projectile>().isEnemyProjectile == false)
            {
                Health -= 1;
                other.gameObject.GetComponent<Projectile>().DestoyProjectile();
            }
            

        }

    }
    private IEnumerator StunCoroutine(float duration)
    {
       
        yield return new WaitForSeconds(duration);
        IsStunned = false;
        

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        foreach (Behaviour behaviour in transform.parent.GetComponents<Behaviour>())
        {
            behaviour.enabled = true;

        }
        transform.parent.GetComponent<SpriteRenderer>().enabled = true;

        stunCoroutine = null;
    }
    public void DestroyEnemy() 
    {

        Destroy(gameObject.transform.parent.gameObject);
    }



   
}
