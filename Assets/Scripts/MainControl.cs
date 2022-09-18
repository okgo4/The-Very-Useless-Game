using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float gameTime = 120;
    public GameObject timeText;
    public GameObject scoreText;
    public GameObject EndPanel;
    public GameObject EndText;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public float gameScore;
    private bool end = false;
    public float refreshCD = 5;
    void Start()
    {
        gameScore = 0;
        gameTime = 120;
        end = false;
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {

        refreshCD -= Time.deltaTime;
        if (GameObject.Find("Enemy").transform.childCount < 4 && refreshCD <= 0)
        {

            int tank_random = (int)Random.Range(1, 3);
            int pos_random = (int)Random.Range(0, 6);
            GameObject tankGenerate  = null;
            if (tank_random == 1)
            {
                tankGenerate = Instantiate(Enemy1, GameObject.Find("WayPoints").transform.GetChild(pos_random).transform);
            }
            if (tank_random == 2)
            {
                tankGenerate = Instantiate(Enemy2, GameObject.Find("WayPoints").transform.GetChild(pos_random).transform);
            }
            if (tank_random == 3)
            {
                tankGenerate = Instantiate(Enemy3, GameObject.Find("WayPoints").transform.GetChild(pos_random).transform);
            }
            tankGenerate.transform.parent = GameObject.Find("Enemy").transform;
            refreshCD = 5;
        }
        gameTime -= Time.deltaTime;
        timeText.GetComponent<TextMeshProUGUI>().text = gameTime.ToString("F2");
        if (gameTime <= 0 && !end)
        {
            endGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
        }
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + gameScore.ToString("F0");
    }
    public void timeBuff(float value)
    {
        gameTime += value;
    }
    public void endGame()
    {
        Time.timeScale = 0;
        end = true;
        EndPanel.active = true;
        EndText.GetComponent<TextMeshProUGUI>().text = "Game Over! \n Score: " + gameScore.ToString("F0");
    }
}
