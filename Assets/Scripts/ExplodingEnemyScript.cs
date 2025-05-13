
using UnityEngine;
using System.Collections;
using Pathfinding;
public class ExplodingEnemyScript : MonoBehaviour
{
    public bool Exploding;
    public GameObject Particle;
    public GameObject Explosion;
    public LevelManager LevelManager;
    public GameObject SoundManager;

    private void Update()
    {
        LevelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }


    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player" && transform.parent.Find("ExplodingEnemyRoot").GetComponent<EnemyRoot>().IsStunned == false)
        {
            SoundManager = GameObject.Find("SoundManager");
            SoundManager.GetComponent<SoundManager>().PlayBombFuse();
            StartCoroutine(Explode(3));
            Exploding = true;
            Particle.SetActive(true);
            transform.parent.gameObject.GetComponent<AIPath>().maxSpeed = transform.parent.Find("ExplodingEnemyRoot").GetComponent<EnemyRoot>().MoveSpeed + 3;

        }
    }
    private IEnumerator Explode(float duration)
    {

        yield return new WaitForSeconds(duration);
        SoundManager = GameObject.Find("SoundManager");
        SoundManager.GetComponent<SoundManager>().PlayBombExplosion();
        LevelManager.EnemiesInScene.Remove(transform.parent.gameObject);
        Destroy(Instantiate(Explosion, transform.position,Quaternion.identity),3);
        transform.parent.transform.GetChild(0).GetComponent<EnemyRoot>().DestroyEnemy(true);



    }
}
