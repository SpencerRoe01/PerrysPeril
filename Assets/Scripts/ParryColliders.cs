using UnityEngine;

public class ParryColliders : MonoBehaviour
{
    public string ParryName;
    public Parry ParryClass;

    public void OnTriggerEnter2D(Collider2D other)
    {
        ParryClass.ParryMeathod(ParryName, other);

    }
}
