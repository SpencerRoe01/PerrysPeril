using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Laser : MonoBehaviour
{

    float LengthOfLaser = 3;
    float LaserCooldown;

    public GameObject LaserObject;

    private void Start()
    {
        StartCoroutine(LaserLoop());
    }

    private IEnumerator LaserLoop()
    {
        while (true)
        {
           
            LaserObject.SetActive(true);
            yield return new WaitForSeconds(LengthOfLaser);

            
            LaserObject.SetActive(false);
            yield return new WaitForSeconds(LengthOfLaser);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collide");
    }

}
