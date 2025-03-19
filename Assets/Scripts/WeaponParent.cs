using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public GameObject child;
    private float yscale;
    public GameObject PerryArt;

    void Start()
    {

        yscale = child.transform.localScale.y;



    }



    void Update()
    {



        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;



        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;


        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        child.transform.localScale = new Vector3(child.transform.localScale.x, yscale, 1); ;



        if (angle > 90 || angle < -90)
        {
            Vector3 Scale = transform.localScale;
            Scale.y = -1;
            transform.localScale = Scale;

            Scale = PerryArt.transform.localScale;
            Scale.x = -1;
            PerryArt.transform.localScale = Scale;
        }
        else
        {
            Vector3 Scale = transform.localScale;
            Scale.y = 1;
            transform.localScale = Scale;

            Scale = PerryArt.transform.localScale;
            Scale.x = 1;
            PerryArt.transform.localScale = Scale;
        }

    }

}