using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private GameManager gameManager;
    public int ciblesDetruites = 0;
    public string targetTag = "LaserGunTarget";

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            Destroy(collision.gameObject);
            if (gameManager != null){
                gameManager.IncrementDestructedTarget();
            }
        }
        Destroy(gameObject);
    }
}
