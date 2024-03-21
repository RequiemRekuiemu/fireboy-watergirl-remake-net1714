using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    public GameObject pauseMenuUI;

    [SerializeField] private Text volume;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject networkManagerUI;

    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        networkManagerUI.SetActive(true);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        networkManagerUI.SetActive(false);
    }

    public void LoadOptions()
    {
        Debug.Log("Options");
        optionsMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
        volume.text = ((int)(AudioListener.volume * 10)).ToString();
    }

    public void BackOptions()
    {
        optionsMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void LoadMenu()
    {
        Debug.Log("Menu");
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void IncreaseVol()
    {
        bool con = audioMixer.GetFloat("volume", out float vol);
        if (con)
        {
            if (vol < 0f)
            {
                vol += 8.0f;
                audioMixer.SetFloat("volume", vol);
                volume.text = ((int)(1.2f * vol + 100)).ToString();
            }
        }
    }

    public void DecreaseVol()
    {
        bool con = audioMixer.GetFloat("volume", out float vol);
        if (con)
        {
            if (vol > -80f)
            {
                vol -= 8.0f;
                audioMixer.SetFloat("volume", vol);
                volume.text = ((int)(1.2f * vol + 100)).ToString();
            }
        }
    }
}
