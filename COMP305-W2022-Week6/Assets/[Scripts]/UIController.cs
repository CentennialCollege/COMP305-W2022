using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    public void OnStartButton_Pressed()
    {
        SceneManager.LoadScene("Level 1");
    }
}