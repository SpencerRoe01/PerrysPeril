using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float PlayerMoveSpeed = 0f;
	 private float PlayerMovementX = 0f;
	 private float PlayerMovementY = 0f;


	 public float ActiveMoveSpeed;
	 public float DashSpeed;

	 public float DashLength = .5f, DashCooldown = 1f;

	 private float DashCounter;
	 private float DashCoolCounter;



	 void Start(){
	 
		ActiveMoveSpeed = PlayerMoveSpeed;

	 }




	// do Physics Calcs here
	void Update () 
	{
		if(!(DashCounter > 0)){
		PlayerMovementX = Input.GetAxisRaw ("Horizontal");
		PlayerMovementY = Input.GetAxisRaw ("Vertical");
		}
		GetComponent<Rigidbody2D>().linearVelocity = new Vector2 (PlayerMovementX * ActiveMoveSpeed, PlayerMovementY * ActiveMoveSpeed);

		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(DashCoolCounter <= 0 && DashCounter <=0)
			{
				ActiveMoveSpeed = DashSpeed;
				DashCounter = DashLength;
			}
		
		}

		if(DashCounter > 0)
		{
			DashCounter -= Time.deltaTime;

			if(DashCounter <= 0)
			{
				ActiveMoveSpeed = PlayerMoveSpeed;
				DashCoolCounter = DashCooldown;
			}
		}

		if(DashCoolCounter > 0)
		{
			DashCoolCounter -= Time.deltaTime;
		}



	}
}
