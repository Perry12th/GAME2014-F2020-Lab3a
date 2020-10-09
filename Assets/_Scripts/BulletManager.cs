using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[Serializable]
public class BulletManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public Queue<GameObject> bulletPool;
    public int MaxBullets;

    void Start()
    {
        _BuildBulletPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void _BuildBulletPool()
    {
        bulletPool = new Queue<GameObject>();
        for (int count = 0; count < MaxBullets; count++)
        {
           var tempBullet = Instantiate(bullet);
            tempBullet.SetActive(false);
            tempBullet.transform.parent = transform;
            bulletPool.Enqueue(tempBullet);
        }
    }

    public GameObject _GetBullet(Vector3 position)
    {
        var newBullet = bulletPool.Dequeue();
        newBullet.SetActive(true);
        newBullet.transform.position = position;
       
        return newBullet;
    }

    public void _ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
