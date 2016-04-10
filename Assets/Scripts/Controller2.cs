﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Controller2 : MonoBehaviour {

	public GameObject levelManager;

	private float maxSpeed = 10f;
	public bool ghostMode = false;
	private int radius;
	private DollManager.Boundary boundary;

	public RaycastHit2D hit;

    public Animator anim;
	
	// Use this for initialization
	void Start () {

		levelManager = GameObject.FindGameObjectWithTag ("LevelManager");
		boundary = levelManager.GetComponent<DollManager>().boundary;
		maxSpeed = levelManager.GetComponent<DollManager> ().maxSpeed;
		anim = GetComponent<Animator>();
		anim.SetFloat("Y", -1); // face the front

    }
	
	void Update(){

		checkGhostMode ();
		if (Input.GetKeyDown (KeyCode.R))
			SceneManager.LoadScene (Application.loadedLevel);

		if (!ghostMode) {
			if (Input.GetKeyDown (KeyCode.Z)) {
				startInteraction ();


			}

			/*ANIMATION*/
			float input_x = Input.GetAxisRaw ("Horizontal");
			float input_y = Input.GetAxisRaw ("Vertical");
			move (input_x,input_y);
			bool isWalking = (Mathf.Abs (input_x) + Mathf.Abs (input_y)) > 0;
			anim.SetBool ("isWalking", isWalking);
			/*--------*/

			if (isWalking) {
				anim.SetFloat ("X", input_x);
				anim.SetFloat ("Y", input_y);
				DollAudioManager.getInstance ().playWalkingSound ();
			} else {
				DollAudioManager.getInstance().stopWalkingSound();
			}
		}
      
        else {
			DollAudioManager.getInstance().stopWalkingSound();
			anim.SetBool ("isWalking", false);
		}

	}
		

	void move(float moveHorizontal, float moveVertical)
	{
		
		transform.position += new Vector3 (moveHorizontal, moveVertical, 0).normalized * Time.deltaTime * maxSpeed;
		
		GetComponent<Rigidbody2D>().position = new Vector3 
			(
				Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax), 
				Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax),
				0.0f
				);
		
	}

	void checkGhostMode() {

		if (Input.GetKeyDown (KeyCode.X) && !SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("OpenScene"))){
			ghostMode = !ghostMode;
         
		}
	}

	//whenever player press Z
	void startInteraction(){

		float Horizontal = anim.GetFloat ("X");
		float Vertical = anim.GetFloat ("Y");

		var direction = new Vector3 (0, 0, 0);

		if (Horizontal> 0) {
			direction = new Vector3 (0.8f, 0, 0);
		} else if (Horizontal < 0) {
			direction = new Vector3 (-0.8f, 0, 0);
		} else if (Vertical > 0) {
			direction = new Vector3 (0, 1, 0);
		} else if (Vertical < 0) {
			direction = new Vector3 (0, -1, 0);
		}

		hit = 
			Physics2D.Raycast(this.transform.position, direction,1.5f, 1 << LayerMask.NameToLayer ("Interactive"));
		Debug.DrawRay (this.transform.position ,direction*1.5f, Color.green,0.2f);

		if (hit.collider != null && hit.collider.name != "wall") {
			hit.collider.gameObject.GetComponent<Interact> ().interact ();
		}


	}
		
		
}

