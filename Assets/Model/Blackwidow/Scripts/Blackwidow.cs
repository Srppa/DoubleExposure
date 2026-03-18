using UnityEngine;
using System.Collections;

public class Blackwidow : MonoBehaviour {
    Animator blackwidow;
    private IEnumerator coroutine;
	// Use this for initialization
	void Start () {
        blackwidow = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            blackwidow.SetBool("idle", true);
            blackwidow.SetBool("walk", false);
            blackwidow.SetBool("run", false);
            blackwidow.SetBool("hit", false);
            blackwidow.SetBool("attack", false);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            blackwidow.SetBool("idle", false);
            blackwidow.SetBool("attack", true);
            blackwidow.SetBool("run", false);
            blackwidow.SetBool("walk", false);
            blackwidow.SetBool("hit", false);
            StartCoroutine("idle");
            idle();
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            blackwidow.SetBool("idle", false);
            blackwidow.SetBool("hit", true);
            blackwidow.SetBool("walk", false);
            StartCoroutine("idle");
            idle();
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            blackwidow.SetBool("idle", false);
            blackwidow.SetBool("die", true);
        }
        if (Input.GetKey("down"))
        {
            blackwidow.SetBool("walk", true);
            blackwidow.SetBool("idle", false);
            blackwidow.SetBool("run", false);
            blackwidow.SetBool("hit", false);
        }
        if (Input.GetKey("up"))
        {
            blackwidow.SetBool("run", true);
            blackwidow.SetBool("walk", false);
        }
        if (Input.GetKey("left"))
        {
            blackwidow.SetBool("turn_left", true);
            blackwidow.SetBool("run", false);
            blackwidow.SetBool("walk", false);
            StartCoroutine("walk");
            walk();
        }
        if (Input.GetKey("right"))
        {
            blackwidow.SetBool("turn_right", true);
            blackwidow.SetBool("run", false);
            blackwidow.SetBool("walk", false);
            StartCoroutine("walk");
            walk();
        }
    }
    IEnumerator walk()
    {
        yield return new WaitForSeconds(0.7f);
        blackwidow.SetBool("turn_left", false);
        blackwidow.SetBool("turn_right", false);
        blackwidow.SetBool("walk", true);
    }

    IEnumerator idle()
    {
        yield return new WaitForSeconds(0.5f);
        blackwidow.SetBool("idle", true);
        blackwidow.SetBool("hit", false);
        blackwidow.SetBool("attack", false);
    }
}
