using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class particleTest : MonoBehaviour
{
    public float Speed;
    public float TimeToStop;
    public VisualEffect effect;
    public float tiempo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Speed*Time.deltaTime, 0, 0));
        tiempo += Time.deltaTime;
        if (tiempo > TimeToStop)
        {
            effect.Stop();
            Destroy(this);
            tiempo = 0;

        }
        
    }
}
