
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinLogic : MonoBehaviour {
    public Vector3 initPos;
    public Quaternion initRot;
    public bool isStanding;

	// Use this for initialization
	void Start () {
        SetPos();
        StartCoroutine("CheckStanding");
    }
	
	// Update is called once per frame
	void Update () {

	}

    void ResetPos()
    {
        //wait a few seconds
        transform.position = initPos;
        transform.rotation = initRot;
        isStanding = true;
    }

    void SetPos()
    {
        //yield return new WaitForSeconds(.5f);
        initPos = transform.position;
        initRot = transform.rotation;
        isStanding = true;
    }

    IEnumerator CheckStanding()
    {
        while (true)
        {
            if (transform.rotation.x > 0.1 || transform.rotation.y > 0.1 || transform.rotation.z > 0.1 ) { isStanding = false; }
            if (transform.rotation.x < -0.1 || transform.rotation.y < -0.1 || transform.rotation.z < -0.1) { isStanding = false; }
            yield return new WaitForSeconds(1f);
        }
    }
}
