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
    public override void RecibirDa�o(int d, string tipo, bool bStun, int id)
    {
        
        float da�o = d;
        
        if (tipo == "1")
        {
            da�o *= 1.5f;
            
        }
        else if (tipo == "2")
        {
            da�o *= 1f;
            
        }
        Debug.Log("Da�o recibido " + da�o);
        if (bStun)
        {
            Estuneado = true;
        }
        base.RecibirDa�o(Mathf.CeilToInt(da�o), tipo, bStun, id);
    }
}

