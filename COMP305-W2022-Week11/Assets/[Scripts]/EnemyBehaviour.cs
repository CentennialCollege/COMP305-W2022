using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Enemy Attributes")]
    public float speed;
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public Vector2 groundColliderSize;
    public Transform groundAheadCheck;
    public LayerMask groundLayerMask;
    public bool isGroundAhead;
    public bool isGrounded;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGroundAhead = Physics2D.Linecast(transform.position, groundAheadCheck.position, groundLayerMask);
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, groundColliderSize, CapsuleDirection2D.Horizontal, 0.0f, groundLayerMask);
        
        if (isGrounded)
        {
            Move();
        }
        
        if(!isGroundAhead)
        {
            Flip();
        }
    }

    private void Flip()
    {
        float x = transform.localScale.x * -1.0f;

        transform.localScale = new Vector3(x, 1.0f);
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
    }
}
