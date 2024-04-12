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
    public override void RecibirDaño(int d, string tipo, bool bStun, int id)
    {
        
        float daño = d;
        
        if (tipo == "1")
        {
            daño *= 1.5f;
            
        }
        else if (tipo == "2")
        {
            daño *= 1f;
            
        }
        Debug.Log("Daño recibido " + daño);
        if (bStun)
        {
            Estuneado = true;
        }
        base.RecibirDaño(Mathf.CeilToInt(daño), tipo, bStun, id);
    }
}

