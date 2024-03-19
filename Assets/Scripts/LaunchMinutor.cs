using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LaunchMinutor : MonoBehaviour
{
    public bool isCooking;
    public TMP_Text screenText;
    private float timer = 10f;
    public GameObject cookingLight;
    public GameObject pizza;
    public GameObject pizzaCooked;

    private void Start()
    {
        isCooking = false;
        cookingLight.SetActive(false);
        pizzaCooked.SetActive(false);
    }

    public void StartMinutor()
    {
        if (!isCooking)
        {
            isCooking = true;
            cookingLight.SetActive(true);
            InvokeRepeating("UpdateTimer", 0f, 1f);
        }
    }

    private void UpdateTimer()
    {
        timer -= 1f;
        screenText.text = Mathf.FloorToInt(timer / 60).ToString("00") + ":" + Mathf.FloorToInt(timer % 60).ToString("00");

        if (timer <= 0f)
        {
            screenText.text = "00:00";
            isCooking = false;
            cookingLight.SetActive(false);

            // Remplacer la pizza par la clé
            pizzaCooked.transform.position = pizza.transform.position;
            pizzaCooked.SetActive(true);
            pizza.SetActive(false);

            CancelInvoke("UpdateTimer");
        }
    }
}