using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_BulletSpawn : MonoBehaviour {
    [SerializeField]
    GameObject bulletPrefab;
    public void BulletFire()
    {
        Instantiate(bulletPrefab, transform.position, transform.parent.rotation);
    }
}
