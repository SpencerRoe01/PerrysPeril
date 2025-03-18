using UnityEngine;

public class TEst : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("WideSwing");
        }
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Parry");
        }
    }
}
