using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    
    public GameObject bulletPrefab;
    public int bulletCapacity;
    public int currentPoolSize;

    public Queue<GameObject> bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        bulletPool = new Queue<GameObject>(); // empty bullet pool container
        BuildPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddNewBullet()
    {
        var temp_bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
        temp_bullet.transform.SetParent(this.transform);
        temp_bullet.SetActive(false);
        bulletPool.Enqueue(temp_bullet);
    }

    private void BuildPool()
    {
        for (int i = 0; i < bulletCapacity; i++)
        {
           AddNewBullet();
        }

        currentPoolSize = bulletPool.Count; 
    }

    public GameObject GetBullet(Vector3 position = new Vector3())
    {
        GameObject temp_bullet = null;

        if (bulletPool.Count < 1)
        {
            temp_bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
            temp_bullet.transform.SetParent(this.transform);
            currentPoolSize++;
        }
        else
        {
            temp_bullet = bulletPool.Dequeue();
            temp_bullet.SetActive(true);
        }

        temp_bullet.transform.position = position;
        return temp_bullet;
    }

    public void ReleaseBullet(GameObject bullet_to_release)
    {
        bullet_to_release.SetActive(false);
        bulletPool.Enqueue(bullet_to_release);
    }
}
