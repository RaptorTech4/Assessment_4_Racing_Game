using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager _Instance { get; private set; }
    public InputController _InputController { get; private set; }

    private void Awake()
    {
        _Instance = this;
        _InputController = GetComponentInChildren<InputController>();
    }

    void Update()
    {
        
    }
}
