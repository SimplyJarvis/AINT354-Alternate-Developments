using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_BulletSpawn : MonoBehaviour {
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject sheildPrefab;
    public bool powerUp = false;
    public bool sheild = false;
    bool shieldDone = false;
    [SerializeField]
    Transform shootPos2;
    [SerializeField]
    Transform shootPos3;
    [SerializeField]
    Transform sheildPos;
    [SerializeField]
    Transform sheildPos2;
    [SerializeField]
    Transform sheildPos3;
    [SerializeField]
    Transform sheildPos4;
    [SerializeField]
    Transform sheildPos5;
    [SerializeField]
    Transform sheildPos6;
    [SerializeField]
    Transform sheildPos7;
    [SerializeField]
    Transform sheildPos8;
    [SerializeField]
    Transform bulletSheild;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BulletFire();
        }
        if (sheild == true & shieldDone == false)
        {
            shieldDone = true;
            Instantiate(sheildPrefab, sheildPos.position, transform.parent.rotation);
            Instantiate(sheildPrefab, sheildPos2.position, transform.parent.rotation);
            Instantiate(sheildPrefab, sheildPos3.position, transform.parent.rotation);
            Instantiate(sheildPrefab, sheildPos4.position, transform.parent.rotation);
            Instantiate(sheildPrefab, sheildPos5.position, transform.parent.rotation);
            Instantiate(sheildPrefab, sheildPos6.position, transform.parent.rotation);
            Instantiate(sheildPrefab, sheildPos7.position, transform.parent.rotation);
            Instantiate(sheildPrefab, sheildPos8.position, transform.parent.rotation);
            
        }
    }

    



    public void BulletFire()
    {

        if (powerUp)
        {
            Instantiate(bulletPrefab, transform.position, transform.parent.rotation);
            Instantiate(bulletPrefab, shootPos2.position, shootPos2.rotation);
            Instantiate(bulletPrefab, shootPos3.position, shootPos3.rotation);

        }
        else if (sheild == true)
        {
            Instantiate(bulletPrefab, bulletSheild.position, transform.parent.rotation);
        }
        else
        {
            Instantiate(bulletPrefab, transform.position, transform.parent.rotation);
        }
        
    }
}
