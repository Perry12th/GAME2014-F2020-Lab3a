using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float verticalSpeed;
    public BulletManager bulletManager;
    public float verticalBoundary;
    // Start is called before the first frame update
    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0.0f, verticalSpeed, 0.0f);
        _CheckBounds();
    }

    private void _CheckBounds()
    {
        if(transform.position.y > verticalBoundary)
        {
            bulletManager._ReturnBullet(this.gameObject);
        }
    }
}
