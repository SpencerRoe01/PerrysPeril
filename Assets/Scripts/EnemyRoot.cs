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
    public LevelManager LevelManager;

    public AIPath AiPath;




    private void Start()
    {
        transform.parent.gameObject.GetComponent<AIPath>().maxSpeed = MoveSpeed;
        transform.parent.GetComponent<Shooter>().Player = GameObject.FindGameObjectWithTag("Player");

    }
    

    private void Update()
    {

        Transform parent = transform.parent;
        Vector3 parentScale = parent.localScale;
        float xScale = Mathf.Abs(parentScale.x);

        if (GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x)
            parent.localScale = new Vector3(xScale, parentScale.y, parentScale.z);
        else
            parent.localScale = new Vector3(-xScale, parentScale.y, parentScale.z);


        if (AiPath.reachedDestination) { Animator.SetBool("moving", false); }
        else {Animator.SetBool("moving", true); }


            LevelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
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
            GameObject.Find("ComboManager").GetComponent<Combo>().RegisterStun();
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
        Animator.SetTrigger("shoot");
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile" )
        {
            if (other.gameObject.GetComponent<Projectile>() != null && other.gameObject.GetComponent<Projectile>().isEnemyProjectile == false)
            {
                Health -= 1;
                
                other.gameObject.GetComponent<Projectile>().DestoyProjectile();
            }
            else if (other.gameObject.GetComponent<Projectile>() == null)
            {
                Health -= 1;
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
        LevelManager.EnemiesInScene.Remove(transform.parent.gameObject);
        Destroy(gameObject.transform.parent.gameObject);
        GameObject.Find("ComboManager").GetComponent<Combo>().RegisterKill();
    }



   
}
