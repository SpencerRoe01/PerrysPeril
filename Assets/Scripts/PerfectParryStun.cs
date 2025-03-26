using UnityEngine;

public class PerfectParryStun : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyRoot>().Health -= 1;
        }
    }
}
