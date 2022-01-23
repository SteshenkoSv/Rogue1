using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{
    private static StartScreenController _instance;
    public static StartScreenController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(_instance);
    }

    public void StartGame() 
    {
        SceneManager.LoadScene("Main");
    }

    public void RestarGame()
    {
        SceneManager.LoadScene("StartScreen");

    }
}
