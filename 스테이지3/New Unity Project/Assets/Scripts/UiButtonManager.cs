using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButtonManager : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerScript;
    public bool isItem= false;

    public void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerMovement>();
    }
    public void LeftDown()
    {
        playerScript.inputLeft = true;
    }
    public void LeftUp()
    {
        playerScript.inputLeft = false;
    }
    public void RightDown()
    {
        playerScript.inputRight = true;
    }
    public void RightUp()
    {
        playerScript.inputRight = false;
    }
    public void DownDown()
    {
        playerScript.inputDown = true;
    }
    public void DownUp()
    {
        playerScript.inputDown = false;
    }
    public void UpDown()
    {
        playerScript.inputUp = true;
    }
    public void UpUp()
    {
        playerScript.inputUp = false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

