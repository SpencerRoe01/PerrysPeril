
using UnityEngine;
using System.Collections;
public class ExplodingEnemyScript : MonoBehaviour
{
    public bool Exploding;
    public GameObject Particle;
    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player" && transform.parent.Find("ExplodingEnemyRoot").GetComponent<EnemyRoot>().IsStunned == false)
        {
            StartCoroutine(Explode(3));
            Exploding = true;
            Particle.SetActive(true);


        }
    }
    private IEnumerator Explode(float duration)
    {

        yield return new WaitForSeconds(duration);
        transform.parent.transform.GetChild(0).GetComponent<EnemyRoot>().DestroyEnemy();
        
    }
}
