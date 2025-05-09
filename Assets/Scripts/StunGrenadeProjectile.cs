using UnityEngine;

public class StunGrenadeProjectile : MonoBehaviour
{
    public float timerDuration = 2f;         
    public GameObject particlePrefab;         
    public float particleDestroyDelay = 2f;  

    private CircleCollider2D circleCollider;
    private bool hasTriggered = false;

    public GameObject SoundManager;
    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        if (circleCollider != null)
        {
            circleCollider.enabled = false; 
        }

       
        Invoke(nameof(TriggerEffect), timerDuration);
    }

    private void TriggerEffect()
    {
        if (hasTriggered) return;
        hasTriggered = true;

       
        if (circleCollider != null)
        {
            circleCollider.enabled = true;
        }

        
        if (particlePrefab != null)
        {
            SoundManager = GameObject.Find("SoundManager");
            SoundManager.GetComponent<SoundManager>().PlayBombExplosion();
            GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);

            Destroy(particleInstance, particleDestroyDelay);
        }

        
        Destroy(gameObject,.05f);
    }
}
