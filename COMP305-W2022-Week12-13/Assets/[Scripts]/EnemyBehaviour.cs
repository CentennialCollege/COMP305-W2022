using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Player Detection")]
    //public Transform LOSPoint;
    public float LOSXOffset;
    public float playerYOffset;
    public Vector2 playerPosition;
    public LayerMask playerLayerMask;
    public Collider2D LOSCollider;
    public bool hasLOS;

    [Header("Enemy Attributes")]
    public float speed;
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public Vector2 groundColliderSize;
    public Transform groundAheadCheck;
    public LayerMask groundLayerMask;
    public Transform wallAheadCheck;
    public bool isGroundAhead;
    public bool isWallAhead;
    public bool isGrounded;

    [Header("Bullet Firing")] 
    public BulletManager bulletManager;
    public Transform bulletSpawn;
    public float fireDelay;
    

    // Start is called before the first frame update
    void Start()
    {
        bulletManager = GameObject.FindObjectOfType<BulletManager>();
        InvokeRepeating("FireBullet", fireDelay, fireDelay);
    }

    // Update is called once per frame
    void Update()
    {
        isGroundAhead = Physics2D.Linecast(transform.position, groundAheadCheck.position, groundLayerMask);
        isWallAhead = Physics2D.Linecast(transform.position, wallAheadCheck.position, groundLayerMask);
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, groundColliderSize, CapsuleDirection2D.Horizontal, 0.0f, groundLayerMask);
        
        if (isGrounded)
        {
            //HasLOS();

            if (!hasLOS)
            {
                Move();
            }
        }
        
        if(!isGroundAhead || isWallAhead)
        {
            Flip();
        }

        
    }

    private void FireBullet()
    {
        if (hasLOS)
        {
            var temp_bullet = bulletManager.GetBullet(bulletSpawn.position);
            var bullet_direction = new Vector3(playerPosition.x, playerPosition.y) - bulletSpawn.position;
            temp_bullet.GetComponent<BulletController>().direction = Vector3.Normalize(bullet_direction);
        }
    }


    private void Flip()
    {
        float x = transform.localScale.x * -1.0f;

        transform.localScale = new Vector3(x, 1.0f);

        LOSXOffset *= transform.localScale.x;
    }

    private void Move()
    {
        transform.position += Vector3.left * speed * transform.localScale.x * Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, groundAheadCheck.position);
        Gizmos.DrawWireCube(groundCheck.position, groundColliderSize);
        
        Gizmos.color = new Color(1.0f, 0.5f, 0.0f);
        Gizmos.DrawLine(transform.position, wallAheadCheck.position);

        if (hasLOS)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position - new Vector3(LOSXOffset, 0.0f, 0.0f), playerPosition - new Vector2(0.0f, playerYOffset));
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var hit = Physics2D.Linecast(transform.position - new Vector3(LOSXOffset, 0.0f, 0.0f), other.transform.position - new Vector3(0.0f, playerYOffset, 0.0f), playerLayerMask);
            if (hit)
            {
                LOSCollider = hit.collider;
                hasLOS = LOSCollider.gameObject.CompareTag("Player");
                playerPosition = other.transform.position;
            }
            else
            {
                hasLOS = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasLOS = false;
        }
    }
}
