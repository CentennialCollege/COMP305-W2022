using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public GameController controller;

    void Start()
    {
        controller = GameObject.FindObjectOfType<GameController>();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = controller.currentCheckpoint.position;
        }
    }
}
