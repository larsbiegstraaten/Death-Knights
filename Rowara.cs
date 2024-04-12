using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rowara : Controlador
{
    public GameObject espada;
    public GameObject particulaTorbellino;
    public GameObject particulaUlti;

    public override void AtaqueSimple()
    {
        base.AtaqueSimple();

    }

    public override void AtaqueEspecial()
    {
        base.AtaqueEspecial();
        Instantiate(particulaTorbellino, espada.transform, worldPositionStays: false);

    }
    public override void AtaqueUlti()
    {
        base.AtaqueUlti();
        Instantiate(particulaUlti, espada.transform, worldPositionStays: false);
        
        // hacer snagrado de enemigo

    }

    public override void RecibirDaño(int d, string tipo, bool bStun, int id)
    {
        
        float daño = 0;
        if (tipo == "1")
        {
            daño = d * 1.5f;
            maxVida -= Mathf.CeilToInt(daño);
        }
        else if (tipo == "2")
        {
            daño = d * 1f;
            maxVida -= Mathf.CeilToInt(daño);
        }
        Debug.Log("Daño recibido " + daño);
        if (bStun)
        {
            Estuneado = true;
        }
        base.RecibirDaño(d, tipo, bStun, id);
    }
}
