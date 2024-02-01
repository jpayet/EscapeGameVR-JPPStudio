using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Timer Components")]
    [SerializeField] private float gameTime = 10;
    [SerializeField] private TextMeshProUGUI timeTextBox;
 
    public bool timerIsRunning;

    public GameObject player;
    public GameObject placeholder;
    private string gameTimeClockDisplay;
 

    private void Start()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        UpdateGameTimer();
    }

    private void UpdateGameTimer()
    {
        if (timerIsRunning)
        {
            if (gameTime > 0)
            {
                gameTime -= Time.deltaTime;

                var minutes = Mathf.FloorToInt(gameTime / 60);
                var seconds = Mathf.FloorToInt(gameTime - minutes * 60);

                gameTimeClockDisplay = string.Format("{0:0}:{1:00}", minutes, seconds);

                timeTextBox.text = gameTimeClockDisplay;
            }
            else
            {
                timerIsRunning = false;
                gameTime = 0;
                gameTimeClockDisplay = "";
                timeTextBox.text = gameTimeClockDisplay;

                EndGameLoose();
            }
        }
        
    }

    private void EndGameLoose()
    {           
        player.transform.position = placeholder.transform.position;
        player.transform.rotation = placeholder.transform.rotation;


        Transform locomotion = player.transform.Find("Locomotion System");
        locomotion.gameObject.SetActive(false);
    }
}
