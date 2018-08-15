using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour {
    public CreatureMovement target;
    public Transform Camara = null;
    private Animator myAnimator;
    string ThisTag;

	// Use this for initialization
	void Start () {
		myAnimator = GetComponent<Animator> ();
        ThisTag = transform.tag;

	}
	private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        //if (tag.Equals("floor_middle"))
            //target.Change_i();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
