using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorLevel : MonoBehaviour
{
    private GameObject doorLevel;
    private string level;
    private bool BothIsInside = false;
    private bool Player1IsInside = false;
    private bool Player2IsInside = false;
    private float timer = 0f;
    private float countdown = 5f;

    // Start is called before the first frame update
    void Start()
    {
        doorLevel = this.gameObject;
        string temp = doorLevel.name;
        if (!string.IsNullOrEmpty(temp))
        {
            level = temp.Substring(temp.Length - 1, 1);
            Debug.Log(level);
        }
        else
        {
            Debug.Log("String not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!BothIsInside)
        {
            if (Player1IsInside && Player2IsInside)
            {
                BothIsInside = true;
                Debug.Log("Both is inside");
                CountDown();
                FindObjectOfType<AudioManager>().Play("PortalWarp");
            }
        }
        else
        {
            CountDown();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Player1IsInside
            && string.Equals(collision.name, "Player1", StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("1 entered!");
            Player1IsInside = true;
        }
        if (!Player2IsInside
            && string.Equals(collision.name, "Player2", StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("2 entered!");
            Player2IsInside = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Player1IsInside
            && string.Equals(collision.name, "Player1", StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("1 is inside!");
        }
        if (Player2IsInside
            && string.Equals(collision.name, "Player2", StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("2 is inside!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Player1IsInside
            && string.Equals(collision.name, "Player1", StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("1 is outside!");
            Player1IsInside = false;
        }
        if (Player2IsInside
            && string.Equals(collision.name, "Player2", StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("2 is outside!");
            Player2IsInside = false;
        }
        FindObjectOfType<AudioManager>().ResetVolume("PortalWarp");
        BothIsInside = false;
        timer = 0f;
    }


    private void CountDown()
    {
        if (timer < countdown)
        {
            FindObjectOfType<AudioManager>().IncreaseVolumeGradually("PortalWarp");
            timer = timer + Time.deltaTime;
        }
        else
        {
            Debug.Log("Level loaded.");
            FindObjectOfType<AudioManager>().ResetVolume("PortalWarp");
            GameManager.Instance.CompleteLevel();
        }
    }
}
