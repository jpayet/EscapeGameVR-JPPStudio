using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunShoot : MonoBehaviour
{
    public Transform bulletSpawn;
    public float bulletSpeed = 30f;
    public GameObject bulletPrefab;

    // Variables pour système de son
    public AudioClip shootSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        audioSource.PlayOneShot(shootSound);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        if (bulletRB != null)
        {
            bulletRB.velocity = bulletSpawn.forward * bulletSpeed;
        }
    }
}
