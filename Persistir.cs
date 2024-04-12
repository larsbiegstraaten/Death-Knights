using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistir : MonoBehaviour
{
    public GameObject CanvasChat;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(CanvasChat);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
