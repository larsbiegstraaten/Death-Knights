using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorProxy : MonoBehaviour
{ // NO CREO QUE SE USE
    public Controlador C;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Morir() {
        C.muertePersonaje();
    }


    public void apagarBoolAccion()
    {
        C.haciendoAccion = false;
    }
}
