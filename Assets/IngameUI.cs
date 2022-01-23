using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IngameUI : MonoBehaviour
{
    private static IngameUI _instance;
    public static IngameUI Instance { get { return _instance; } }

    public Text mobsCounterText;
    public Text chestsCounterText;
    public Text roomsCounterText;
    public Text shrinesCounterText;
    public int mobsCounter = 0;
    public int chestsCounter = 0;
    public int roomsCounter = 0;
    public int shrinesCounter = 0;

    public bool isGameOver = false;

    public Rotate rotate;
    public float levelTransitionDelay = 1f;

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
    }

    private void Start()
    {
        rotate = GameObject.Find("Main Camera").GetComponent<Rotate>();

        mobsCounterText.text = mobsCounter.ToString();
        chestsCounterText.text = chestsCounter.ToString();
        roomsCounterText.text = roomsCounter.ToString();
    }

    public void AddMob() 
    {
        mobsCounter++;
        mobsCounterText.text = mobsCounter.ToString();
    }

    public void AddChest()
    {
        chestsCounter++;
        chestsCounterText.text = chestsCounter.ToString();
    }

    public void AddRoom()
    {
        roomsCounter++;
        roomsCounterText.text = roomsCounter.ToString();
    }

    public void AddShrine()
    {
        shrinesCounter++;
        shrinesCounterText.text = shrinesCounter.ToString();
    }


    public void EndRun() 
    {
        isGameOver = true;
        rotate.RotateDisable();
        StartCoroutine(BackToMenu());
    }

    public IEnumerator BackToMenu() 
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("StartScreen");
    }
}
