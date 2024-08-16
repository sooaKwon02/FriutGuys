using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public static BulletController instance;
    public GameObject bulletPrefab;

    List<GameObject> bullets = new List<GameObject>();

    private void Awake()
    {
        if (BulletController.instance == null)
        {
            BulletController.instance = this;
        }
    }

    private void Start()
    {
        CreateBullets(15);
    }

    private void CreateBullets(int bulletCount)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab) as GameObject;
            bullet.transform.parent = transform;
            bullet.SetActive(false);
            bullets.Add(bullet);
        }
    }

    public GameObject GetBullet(Vector3 position , Quaternion rotation)
    {
        GameObject reqBullet = null;

        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i].activeSelf == false)
            {
                reqBullet = bullets[i];
                break;
            }
        }

        if (reqBullet == null)
        {
            GameObject newBullet = Instantiate(bulletPrefab) as GameObject;
            newBullet.transform.parent = transform;
            bullets.Add(newBullet);
            reqBullet = newBullet;
        }

        reqBullet.SetActive(true);
        reqBullet.transform.SetPositionAndRotation(position, rotation);
        return reqBullet;
    }
}