using System;
using UnityEngine;

public class PlayerCannon : MonoBehaviour
{
    [SerializeField] private PlayerBullet bulletPrefab;
    [SerializeField] private Transform exit;

    public void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, exit.position, transform.rotation);
    }
}
