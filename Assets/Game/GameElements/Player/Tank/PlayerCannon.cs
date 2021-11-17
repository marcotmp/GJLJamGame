using System;
using System.Collections;
using UnityEngine;

public class PlayerCannon : MonoBehaviour
{
    [SerializeField] private PlayerBullet bulletPrefab;
    [SerializeField] private Transform exit;
    [SerializeField] private float delay;
    public bool IsAutoShooting { get; set; }
    private IEnumerator shootCoroutine;

    public void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, exit.position, transform.rotation);
    }

    public void StartAutoShoot()
    {
        StopShoot();
        shootCoroutine = AutoShootCoroutine();
        StartCoroutine(shootCoroutine);
        IsAutoShooting = true;
    }

    public void StopShoot()
    {
        if (shootCoroutine != null)
            StopCoroutine(shootCoroutine);
        IsAutoShooting = false;
    }

    private IEnumerator AutoShootCoroutine()
    {
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(delay);
        }
    }
}
