using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NarrationManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI narrationTextBox;

    public void SetTextTimer()
    {
        narrationTextBox.text = "J'ai 10 min pour m'échapper de la pièce, faut pas trainer...";
    }
}
