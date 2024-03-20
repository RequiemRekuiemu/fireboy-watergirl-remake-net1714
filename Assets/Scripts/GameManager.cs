using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentLevel = 0;
    public int endLevel = 12;
    public bool levelComplete = false;
    public bool isReloading = false;

    [SerializeField] private GameObject playerIgnis;
    [SerializeField] private GameObject playerAqua;
    [SerializeField] private GameObject victoryUI;

    private Transform spawnPointIgnis;
    private Transform spawnPointAqua;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.sceneCount < 2)
        {
            SceneManager.LoadScene(currentLevel + 1, LoadSceneMode.Additive);
        }
    }

    private void Awake()
    {
        Instance = this;
        Debug.Log(currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        EndGame eg1 = GameObject.FindGameObjectWithTag("EndGameIgnis").GetComponent<EndGame>();
        EndGame eg2 = GameObject.FindGameObjectWithTag("EndGameAqua").GetComponent<EndGame>();
        bool con1 = eg1.ReturnCondition();
        bool con2 = eg2.ReturnCondition();
        if (con1 && con2)
        {
            CompleteLevel();
        }
    }

    public void RestartLevel(string name)
    {
        if (isReloading)
            return;

        if (string.Equals(tag, "Ignis", StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("You are extinguished");
        }
        if (string.Equals(tag, "Aqua", StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("You are vaporized");
        }

        Debug.Log("One more time...");
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Ignis"))
        {
            player.transform.position = GameObject.FindGameObjectWithTag("Spawnpoint").GetComponent<Transform>().position;
        }
        /*StartCoroutine(ReloadLevel());*/
    }

    IEnumerator ReloadLevel()
    {
        isReloading = true;

        AsyncOperation unload = SceneManager.UnloadSceneAsync(currentLevel);

        while (!unload.isDone)
        {
            yield return 0;
        }

        Debug.Log("Scene loaded");
        SceneManager.LoadScene(currentLevel, LoadSceneMode.Additive);
        //Fader.instance.FadeIn();

        isReloading = false;
    }

    public void CompleteLevel()
    {
        if (levelComplete)
            return;

        levelComplete = true;
        Debug.Log("You won!");
        Time.timeScale = 0f;
        FindObjectOfType<AudioManager>().Play("Victory");
        victoryUI.SetActive(true);
        /*StartCoroutine(LoadNextLevel());*/
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1f);

        //Fader.instance.FadeOut();

        yield return new WaitForSeconds(0.6f);

        if (endLevel == currentLevel + 1)
        {
            SceneManager.LoadScene(currentLevel + 1, LoadSceneMode.Single);
        }
        else
        {
            AsyncOperation unload = SceneManager.UnloadSceneAsync(currentLevel);

            while (!unload.isDone)
            {
                yield return 0;
            }

            currentLevel++;
            SceneManager.LoadScene(currentLevel, LoadSceneMode.Additive);
        }

        //Fader.instance.FadeIn();

        levelComplete = false;
    }
}
