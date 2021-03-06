using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public float horizontalForce;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rigidBody2D.AddForce(Vector2.right * x * horizontalForce * Time.deltaTime);
    }
}
