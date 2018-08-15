using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatItem : MonoBehaviour
{
    GameObject item;
    GameObject Enemy;

    int i = 1;
    // Use this for initialization
    private void Start()
    {
        item = GameObject.Find("item");
        if (ScoreUpdate.score > 0)
        {
            Enemy.SetActive(false);
            StartCoroutine("EnemyBlink");
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name=="item") 
        {
            
            item.SetActive(false);
            ScoreUpdate.score+=1;

        }
       
    }

    IEnumerator EnemyBlink()
    {
        int count = 0;
        while (count < 3)
        {
            Enemy.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            Enemy.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            count++;
        }
    }
}
	