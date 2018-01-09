using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_BulletSpawn : MonoBehaviour {
    [SerializeField]
    GameObject bulletPrefab;
    public bool powerUp = false;
    [SerializeField]
    Transform shootPos2;
    [SerializeField]
    Transform shootPos3;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            BulletFire();

    }



    public void BulletFire()
    {

        if (powerUp)
        {
            Instantiate(bulletPrefab, transform.position, transform.parent.rotation);
            Instantiate(bulletPrefab, shootPos2.position, shootPos2.rotation);
            Instantiate(bulletPrefab, shootPos3.position, shootPos3.rotation);

        }
        else
        {
            Instantiate(bulletPrefab, transform.position, transform.parent.rotation);
        }
        
    }
}
