using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Bullet Properties")] 
    public BulletManager bulletManager;
    public Vector3 direction;
    public float speed;
    public float duration;
    public float timer;

    void OnEnable()
    {
        timer = duration;
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletManager = GameObject.FindObjectOfType<BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            bulletManager.ReleaseBullet(this.gameObject);
        }

        Move();
    }

    public void Move()
    {
        direction.z = 0.0f;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Ground":
                bulletManager.ReleaseBullet(this.gameObject);
                break;
            case "Player":
                bulletManager.ReleaseBullet(this.gameObject);
                break;
        }
    }
}
