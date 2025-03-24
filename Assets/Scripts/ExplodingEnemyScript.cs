
using UnityEngine;
using System.Collections;
public class ExplodingEnemyScript : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Explode(3));
            
        }
    }
    private IEnumerator Explode(float duration)
    {

        yield return new WaitForSeconds(duration);
        transform.parent.transform.GetChild(0).GetComponent<EnemyRoot>().DestroyEnemy();
        
    }
}
