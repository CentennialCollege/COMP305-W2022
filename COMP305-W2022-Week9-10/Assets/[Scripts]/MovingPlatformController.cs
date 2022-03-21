using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformDirection
{
    HORIZONTAL,
    VERTICAL,
    DIAGONAL_UP,
    DIAGONAL_DOWN,
    NUM_OF_DIRECTIONS
}


public class MovingPlatformController : MonoBehaviour
{
    [Header("Platform Properties")]
    [Range(1, 20)]
    public float distance;
    public PlatformDirection direction;
    [Range(0.1f, 10.0f)]
    public float speed;
    public float timer;
    public bool isLooping;

    private float pingPongDistance;

    private float startX;
    private float startY;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
        timer  = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if(isLooping)
        {
            timer += Time.deltaTime;
            pingPongDistance = Mathf.PingPong(timer * speed, distance);
        }

        Move();
        
    }

    public void Move()
    {
        switch(direction)
        {
            case PlatformDirection.HORIZONTAL:
            {
                if(!isLooping)
                {
                    if(transform.position.x < startX + distance - 0.1f)
                    {
                        timer += Time.deltaTime;
                        pingPongDistance = Mathf.PingPong(timer * speed, distance);
                    }
                    else
                    {
                        pingPongDistance = distance;
                    }
                }

                var horizontal = startX + pingPongDistance;
                transform.position = new Vector2(horizontal, transform.position.y);
            }
            

            break;
            case PlatformDirection.VERTICAL:  
            {
                if(!isLooping)
                {
                    if(transform.position.x < startY + distance - 0.1f)
                    {
                        timer += Time.deltaTime;
                        // keep moving
                        pingPongDistance = Mathf.PingPong(timer * speed, distance);
                    }
                    else
                    {
                        pingPongDistance = distance;
                    }
                }

                var vertical = startY + pingPongDistance;
                transform.position = new Vector2(transform.position.x, vertical);
            }
                
            break;
            case PlatformDirection.DIAGONAL_UP:
            {
                if(!isLooping)
                {
                    if(transform.position.x < startX + distance - 0.1f)
                    {
                        timer += Time.deltaTime;
                        pingPongDistance = Mathf.PingPong(timer * speed, distance);
                    }
                    else
                    {
                        pingPongDistance = distance;
                    }
                }

                var vertical = startY + pingPongDistance;
                var horizontal = startX + pingPongDistance;
                transform.position = new Vector2(horizontal, vertical);
            }
             
            break;

            case PlatformDirection.DIAGONAL_DOWN:
            {
                if(!isLooping)
                {
                    if(transform.position.x < startX + distance - 0.1f)
                    {
                        timer += Time.deltaTime;
                        pingPongDistance = Mathf.PingPong(timer * speed, distance);
                    }
                    else
                    {
                        pingPongDistance = distance;
                    }
                }

                var vertical = startY - pingPongDistance;
                var horizontal = startX + pingPongDistance;
                transform.position = new Vector2(horizontal, vertical);
            }
             
            break;
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(this.transform);
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
