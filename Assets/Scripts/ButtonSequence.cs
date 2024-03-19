    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ButtonSequence : MonoBehaviour
    {
        private GameManager gameManager;

        // Variables de statements
        public bool IsSequenceCorrect = false;
        public bool IsSequenceCompletedOnce = false;

        // Variables pour fonctionnemnts des buzzers
        private List<string> userButtonSequence = new List<string>();
        public List<string> correctBuzzerSequence = new List<string>{"BuzzerJaune", "BuzzerRouge", "BuzzerBleu", "BuzzerVert"};
        public Dictionary<string, GameObject> buzzerDictionary = new Dictionary<string, GameObject>();
        private Dictionary<string, Vector3> originalPositions = new Dictionary<string, Vector3>();
        public float yOffset = -0.1f;

        // Autres GameObjects utilisés dans le script
        public List<GameObject> AmbiantLights;
        public GameObject nextEnigmeGuide;
        public GameObject laserGun;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            nextEnigmeGuide.SetActive(false);
            laserGun.SetActive(false);

            // Assigner les boutons à leurs noms correspondants
            buzzerDictionary["BuzzerVert"] = GameObject.Find("BuzzerVert");
            buzzerDictionary["BuzzerJaune"] = GameObject.Find("BuzzerJaune");
            buzzerDictionary["BuzzerRouge"] = GameObject.Find("BuzzerRouge");
            buzzerDictionary["BuzzerBleu"] = GameObject.Find("BuzzerBleu");

            // stocker les valeurs d'origines de la position de chaque bouton
            foreach (var pair in buzzerDictionary)
            {
                originalPositions[pair.Key] = pair.Value.transform.position;
            }
        }

        public void OnButtonPress(string buttonName)
        {
            userButtonSequence.Add(buttonName); 
            if (!IsSequenceCompletedOnce && buzzerDictionary.ContainsKey(buttonName))
            {
                GameObject button = buzzerDictionary[buttonName];
                MoveButton(button);
            }

            if (!IsSequenceCompletedOnce && userButtonSequence.Count == correctBuzzerSequence.Count)
            {
                for (int i = 0; i < userButtonSequence.Count; i++)
                {
                    if (userButtonSequence[i] != correctBuzzerSequence[i])
                    {
                        BuzzerReset();
                    } else {      
                        if (gameManager != null)
                        {
                            IsSequenceCorrect = true;
                            IsSequenceCompletedOnce = true;
                            gameManager.IncrementEnigmeAchivement();
                            nextEnigmeGuide.SetActive(true);
                            laserGun.SetActive(true);
                            gameManager.DesactivatedLights = true;
                            foreach (GameObject lightObject in AmbiantLights)
                            {
                                lightObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }

        private void MoveButton(GameObject button)
        {
            Vector3 currentPosition = button.transform.position;
            Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y + yOffset, currentPosition.z);
            button.transform.position = newPosition;
        }

        public void BuzzerReset()
        {
            userButtonSequence.Clear();
            if (!IsSequenceCompletedOnce){
                foreach (var pair in buzzerDictionary)
                {
                    pair.Value.transform.position = originalPositions[pair.Key];
                }
            }
        }
    }
