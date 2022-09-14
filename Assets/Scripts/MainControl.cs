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
    private bool end = false;
    void Start()
    {
        gameTime = 12;
        end = false;
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
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
        
    }
}
