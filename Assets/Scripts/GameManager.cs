using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    // Gestion du temps
    [SerializeField] private TextMeshProUGUI timeTextBox;
    [SerializeField] private float gameTime = 600;
    private string gameTimeClockDisplay;
    public bool timerIsRunning;

    // Gestion de l'avancement des énigmes
    public bool DesactivatedLights = false;
    public int enigmeAchivement;
    public int destructedTarget = 0;

    // Divers GameObjects référencés dans le script
    public GameObject player;
    public GameObject placeholder;
    public GameObject porteRegie;
    public GameObject enigmeRegieLight;
    public GameObject pizzaCooked;
    public GameObject escapeKey;

    // Gestion de la musique
    private AudioSource audioSource;
    public AudioClip[] playlist;
    public AudioClip monsterEatSound;

    // Gestion du verrouillage des portes
    public XRGrabInteractable porteEntreeGrab;
    public XRGrabInteractable porteRegieGrab;
    public XRGrabInteractable porteReserveGrab;

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

    /**************************************\
    |  Gestion du temps (compte à rebours) |
    \**************************************/
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

    /*******************************\
    |  Gestion de la musique du jeu |
    \*******************************/
    private void PlayNextTrack()
    {
        if (playlist.Length > 0)
        {
            audioSource.clip = playlist[0];
            audioSource.Play();
        }
    }

    /**************************************\
    |  Gestion de l'avancement des énigmes |
    \**************************************/
    public void IncrementEnigmeAchivement()
    {
        enigmeAchivement++;
    }

    /**********************************************\
    |  Méthodes spécifiques pour certaines énigmes |
    \**********************************************/
    public void IncrementDestructedTarget()
    {
        destructedTarget++;
        if (destructedTarget == 3){
            porteRegie.transform.Rotate(Vector3.up, 10f, Space.World);
            enigmeRegieLight.SetActive(true);
            porteRegieGrab.enabled = true;

            IncrementEnigmeAchivement();
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
 
    /*************************************\
    | Gestion des verrouillage des portes |
    \*************************************/
    public void OpenReserveDoor()
    {
        porteReserveGrab.enabled = true;
    }

    public void OpenEntreeDoor()
    {
        porteEntreeGrab.enabled = true;
    }
}
