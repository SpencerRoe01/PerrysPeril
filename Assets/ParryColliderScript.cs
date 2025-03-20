using UnityEngine;

public class ParryColliderScript : MonoBehaviour
{
    public string Name;
    public Parry ParryClass;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            ParryClass.ParryMeathod(Name, other);
        }

    }
}
