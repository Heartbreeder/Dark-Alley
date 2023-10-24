using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gandalf : MonoBehaviour {

    public Texture[] Gandulf;
    public GameObject[] Gandolf;
    public AudioClip Sax;
    public AudioSource SaxGuy;

    private bool enable=false;
    private int fps = 12;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (enable)
        {
            for(int i = 0; i < Gandolf.Length; i++)
            {
                int index = (int)(Time.time * fps) % Gandulf.Length;
                Gandolf[i].GetComponent<Renderer>().material.mainTexture = Gandulf[index];
            }
        }
	}

    void Press()
    {
        enable = true;
        SaxGuy.clip = Sax;
        SaxGuy.Play();
    }
}
