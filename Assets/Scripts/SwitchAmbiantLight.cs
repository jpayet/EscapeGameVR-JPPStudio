using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAmbiantLight : MonoBehaviour
{
    public bool IsOn { get; private set; } = false;

    public List<GameObject> AmbiantLights;
    private Quaternion initialRotation;

    private GameManager gameManager;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        SetAmbiantLights(false);
        initialRotation = transform.rotation;
    }

    public void ToggleSwitch()
    {
        if (gameManager != null){
            if (!gameManager.DesactivatedLights)
            {
                IsOn = !IsOn;
                SetAmbiantLights(IsOn);

                if (IsOn)
                {
                    transform.Rotate(new Vector3(0.0f, 0.0f, -50f));
                }
                else
                {
                    transform.rotation = initialRotation;
                }
            }
        }
    }

    private void SetAmbiantLights(bool state)
    {
        foreach (GameObject lightObject in AmbiantLights)
        {
            lightObject.SetActive(state);
        }
    }  
}
