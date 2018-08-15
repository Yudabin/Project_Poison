using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool isItem = false;
    public GameObject Item;
    public UiButtonManager UIM;


    // Use this for initialization
    void Start()
    {
        Item.SetActive(false);
    }

    // Update is called once per frame
    public void ItemOn()
    {
        UIM.isItem = true;
        isItem = true;
        Invoke("ItemOff", 10f);
    }
    private void ItemOff()
    {
        UIM.isItem = false;
        isItem = false;

    } 
}
