using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    [Header("Timer Components")]
    [SerializeField] private float gameTime = 600;
    [SerializeField] private TextMeshProUGUI timeTextBox;
 
    public bool timerIsRunning;

    public GameObject player;
    public GameObject placeholder;
    private string gameTimeClockDisplay;
 
    public int enigmeAchivement;

    public bool DesactivatedLights = false;
    

    public int destructedTarget = 0;
    public GameObject porteRegie;
    public GameObject enigmeRegieLight;
    public GameObject escapeKey;
    public GameObject pizzaCooked;

    public XRGrabInteractable porteEntreeGrab;
    public XRGrabInteractable porteRegieGrab;
    public XRGrabInteractable porteReserveGrab;

    // Liste des musiques dans la playlist
    public AudioClip[] playlist;
    private AudioSource audioSource;
    public AudioClip monsterEatSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timerIsRunning = true;
        porteEntreeGrab.enabled = false;
        porteRegieGrab.enabled = false;
        porteReserveGrab.enabled = false;
        enigmeRegieLight.SetActive(false);
        escapeKey.SetActive(false);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;

        if (playlist.Length > 0)
        {
            PlayNextTrack();
        }
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

                gameTimeClockDisplay = string.Format("Sors vite... {0:0}:{1:00}", minutes, seconds);

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

    public void IncrementEnigmeAchivement()
    {
        enigmeAchivement++;
    }

    public void IncrementDestructedTarget()
    {
        destructedTarget++;
        if (destructedTarget == 3){
            porteRegie.transform.Rotate(Vector3.up, 10f, Space.World);
            IncrementEnigmeAchivement();
            enigmeRegieLight.SetActive(true);
            porteRegieGrab.enabled = true;
        }
    }

    public void OpenReserveDoor()
    {
        porteReserveGrab.enabled = true;
    }

    private void PlayNextTrack()
    {
        // Jouer la piste suivante dans la playlist
        if (playlist.Length > 0)
        {
            audioSource.clip = playlist[0]; // Assurez-vous d'ajuster l'index si nécessaire
            audioSource.Play();
        }
    }

    public void GiveEscapeKey()
    {
        audioSource.PlayOneShot(monsterEatSound);
        escapeKey.SetActive(true);
        escapeKey.transform.position = pizzaCooked.transform.position;
        pizzaCooked.SetActive(false);
        IncrementEnigmeAchivement();
    }

    public void OpenEntreeDoor()
    {
        porteEntreeGrab.enabled = true;
    }
}
