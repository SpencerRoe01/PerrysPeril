using UnityEngine;

public class ParryColliders : MonoBehaviour
{
    public string ParryName;
    public Parry ParryClass;

    void OnTriggerEnter2D(Collider2D other)
    {
        ParryClass.ParryMeathod(ParryName);
        
    }
}
