using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameController : MonoBehaviour
{
    public Transform initialSpawnPoint;
    public Transform currentCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        currentCheckpoint = initialSpawnPoint; // first checkpoint
    }
}
