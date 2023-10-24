using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBalls : MonoBehaviour {
	public Rigidbody bullet;
	public float power = 1500f;

	void Start () {
		
	}
	
	void Update () {
		if(Input.GetButtonUp("Fire1")){
			Rigidbody instance= Instantiate(bullet, transform.position, transform.rotation) as Rigidbody;
			Vector3 fwd = transform.TransformDirection(Vector3.forward);
            instance.tag = "Ball";
			instance.AddForceAtPosition (fwd * power, fwd);
		}
			
	}
}
