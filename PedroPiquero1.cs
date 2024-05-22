using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;


public class PedroPiquero : Controlador
{
    public GameObject particulaCargaElectrica;
    public GameObject arma;


 
    
    public override void AtaqueSimple()
    {
        base.AtaqueSimple();


    }

    public override void AtaqueEspecial()
    {
        base.AtaqueEspecial();

    }
    public override void AtaqueUlti()
    {
        base.AtaqueUlti();
        // Instanciar particula de carga electrica en el arma
        Instantiate(particulaCargaElectrica, arma.transform, worldPositionStays:false);
    }
    public override void RecibirDanio(int d, string tipo, bool bStun, int id)
    {
        
        float danio = d;
        
        if (tipo == "1")
        {
            danio *= 1.5f;
            
        }
        else if (tipo == "2")
        {
            danio *= 1f;
            
        }
        Debug.Log("Danyo recibido " + danio);
        if (bStun)
        {
            Estuneado = true;
        }
        base.RecibirDanio(Mathf.CeilToInt(danio), tipo, bStun, id);
    }
}

