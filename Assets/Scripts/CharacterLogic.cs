using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLogic : MonoBehaviour {

    enum CharState
    {
        Idle = 0,
        Held = 1
    }

    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller;
    public int power=3000;
    public GameObject HUD;
    private RaycastHit target;
    private GameObject rayTarget;
    public GameObject launcher;
    public GameObject holdPos;
    private GameObject interactText, controlsText;
    private CharState state;
    private GameObject heldObj;
    private string[] guiControls= new string[2];

    // Use this for initialization
    void Start () {
        interactText = HUD.transform.GetChild(0).gameObject;
        controlsText = HUD.transform.GetChild(1).gameObject;
        interactText.SetActive(false);
        state = CharState.Idle;
        launcher.SetActive(false);

        //Idle
        guiControls[0] = "[SHIFT]- walk/run\n" +
                         "[LMB]- interact";
        //Held
        guiControls[1] = "[SHIFT]- walk/run\n" +
                         "[LMB]- shoot";


    }
	
	// Update is called once per frame
	void Update () {

        if (state == CharState.Idle)
        {
            controller.m_WalkSpeed=3;
            controlsText.GetComponent<Text>().text = guiControls[0];
            Raycast();
            if(rayTarget != null)
            {
                if (rayTarget.tag == "Ball")
                {
                    interactText.SetActive(true);
                    interactText.GetComponent<Text>().text = "Grab Bowling Ball";
                    if (Input.GetButtonUp("Fire1"))
                    {
                        //Grab ball
                        heldObj = rayTarget;
                        heldObj.GetComponent<Rigidbody>().useGravity = false;
                        heldObj.GetComponent<Rigidbody>().isKinematic = true;
                        heldObj.GetComponent<SphereCollider>().enabled = false;
                        heldObj.transform.position = holdPos.transform.position;
                        heldObj.transform.rotation = launcher.transform.rotation;
                        heldObj.transform.Rotate(-launcher.transform.forward);
                        heldObj.transform.SetParent(gameObject.transform);

                        //Cleanup
                        interactText.SetActive(false);
                        launcher.SetActive(true);
                        state = CharState.Held;
                    }
                }
                else if(rayTarget.tag=="Button")
                {
                    interactText.SetActive(true);
                    interactText.GetComponent<Text>().text = "Press big red button";
                    if (Input.GetButtonUp("Fire1"))
                    {
                        rayTarget.SendMessage("Press");
                    }
                }
            }
        }
        else if (state == CharState.Held)
        {
            controller.m_WalkSpeed = 1;
            interactText.SetActive(true);
            interactText.GetComponent<Text>().text = "Shoot";
            controlsText.GetComponent<Text>().text = guiControls[1];
            if (Input.GetButtonUp("Fire1"))
            {
                //Release Ball
                heldObj.GetComponent<Rigidbody>().useGravity = true;
                heldObj.GetComponent<Rigidbody>().isKinematic = false;
                heldObj.GetComponent<SphereCollider>().enabled = true;
                heldObj.transform.position = launcher.transform.position;
                heldObj.transform.rotation = launcher.transform.rotation;
                heldObj.transform.Rotate(-launcher.transform.forward);
                heldObj.transform.SetParent(null);


                //Add forward force
                heldObj.GetComponent<Rigidbody>().AddForceAtPosition(launcher.transform.forward * power, heldObj.transform.GetChild(0).transform.forward);

                //Add Curve force
                //heldObj.transform.GetChild(0).GetComponent<Rigidbody>().AddTorque(0,0,0);

                //Cleanup
                state = CharState.Idle;
                launcher.SetActive(false);
                
                heldObj = null;
            }
        }
        
    }

    //Raycast to see of the player sees an object
    void Raycast()
    {
        if (Physics.Raycast(transform.position, transform.forward, out target, 2))
        {
            if (target.collider.gameObject.tag == "Ball" || target.collider.gameObject.tag == "Button")
            {
                rayTarget = target.collider.gameObject;
                //interactText;
            }
            else
            {
                rayTarget = null;
                interactText.SetActive(false);
            }

        }
        else
        {
            rayTarget = null;
            interactText.SetActive(false);
        }
        
    }

}
