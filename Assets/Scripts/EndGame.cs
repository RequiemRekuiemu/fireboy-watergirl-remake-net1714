using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private bool ignisIsInside = false;
    private bool aquaIsInside = false;

    private float timer = 0f;
    private float countdown = 5f;

    private bool completed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!completed)
        {
            if (ignisIsInside && aquaIsInside)
            {
                Debug.Log("Both is inside");
                CountDown();
                FindObjectOfType<AudioManager>().Play("PortalWarp");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (string.Equals(this.tag, "EndGameIgnis", StringComparison.OrdinalIgnoreCase)
            && string.Equals(collision.tag, "Ignis", StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("1 is inside!");
            ignisIsInside = true;
        }
        else
        {
            ignisIsInside = false;
        }
        if (string.Equals(this.tag, "EndGameAqua", StringComparison.OrdinalIgnoreCase)
            && string.Equals(collision.tag, "Aqua", StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("2 is inside!");
            aquaIsInside = true;
        }
        else
        {
            aquaIsInside = false;
        }
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
            Time.timeScale = 0f;
            GameManager.Instance.CompleteLevel();
        }
    }

    public bool ReturnCondition()
    {
        if (string.Equals(this.tag, "EndGameIgnis", StringComparison.OrdinalIgnoreCase))
        {
            return ignisIsInside;
        }
        else
        {
            return aquaIsInside;
        }
    }
}
