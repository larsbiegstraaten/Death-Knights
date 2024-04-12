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

    public override void RecibirDa�o(int d, string tipo, bool bStun, int id)
    {
        
        float da�o = 0;
        if (tipo == "1")
        {
            da�o = d * 1.5f;
            maxVida -= Mathf.CeilToInt(da�o);
        }
        else if (tipo == "2")
        {
            da�o = d * 1f;
            maxVida -= Mathf.CeilToInt(da�o);
        }
        Debug.Log("Da�o recibido " + da�o);
        if (bStun)
        {
            Estuneado = true;
        }
        base.RecibirDa�o(d, tipo, bStun, id);
    }
}
