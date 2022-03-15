using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarController : BaseTower
{
    public override void Shoot()
    {
        Instantiate(bullet, bulletSpawnPositions[0].transform.position, transform.rotation);
        Instantiate(flash, bulletSpawnPositions[0].transform.position, transform.rotation);
        src.Play();
        coolDown = shootSpeed;
        base.Shoot();
    }
}
