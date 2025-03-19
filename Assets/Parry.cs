using UnityEngine;

public class Parry : MonoBehaviour
{
    public Collider2D WideParryCollider;

    public Collider2D ParryCollider;
    public Collider2D PerfectParryCollider;


    public Animator SwordAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SwordAnimator == null)
        {
            SwordAnimator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SwordAnimator.SetTrigger("WideSwing");
            WideParryCollider.enabled = true;

        }
        if (Input.GetMouseButtonDown(1))
        {
            SwordAnimator.SetTrigger("Parry");
            ParryCollider.enabled = true;
            PerfectParryCollider.enabled = true;
        }
    }

    public void ParryMeathod(string TypeOfParry) 
    {
        if (TypeOfParry == "Perfect")
        {
            //Perfect Parry
        }
        else if (TypeOfParry == "Regular") 
        { 
            // regular Parry 
        }

        if (TypeOfParry == "Wide") 
        {
            Debug.Log("Wide PArry");
        }
    }
}
