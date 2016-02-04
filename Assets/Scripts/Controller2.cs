﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	//create a separate class to make the code useable
	public float xMin, xMax, yMin, yMax;
}

public class Controller2 : MonoBehaviour {

	public float maxSpeed = 10f;
	public bool ghostMode = false;

	public Boundary boundary;
	
	//	public bool facingRight = true;
//	
//	Animator anim;
	
	// Use this for initialization
	void Start () {
		
		//anim = GetComponent<Animator>();
		
	}
	
	void Update(){

		checkGhostMode ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (!ghostMode) {
			move ();
		} 

		//		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, rayDistance);
		//		if (hit.collider != null) {
		//			Debug.DrawRay (ray.origin, ray.direction * 100, Color.yellow);
		//		}
		
		//		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -Vector2.up, rayDistance);
		//
		//		Debug.Log( hits.Length );

	}

	void move()
	{


		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		transform.position += new Vector3 (moveHorizontal, moveVertical, 0).normalized * Time.deltaTime * maxSpeed;
		
		GetComponent<Rigidbody2D>().position = new Vector3 
			(
				Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax), 
				Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax),
				0.0f
				);

//		if (moveHorizontal  > 0 && ! facingRight)
//			Flip ();
//		else if (moveHorizontal < 0 && facingRight)
//			Flip ();
		
	}

	void checkGhostMode() {
		if (Input.GetKeyDown (KeyCode.X)){
			ghostMode = !ghostMode;
		}
	}



//	void Flip()
//	{
//		facingRight = ! facingRight;
//		Vector3 theScale = transform.localScale;
//		theScale.x *= -1;
//		transform.localScale = theScale;
//	}
}

