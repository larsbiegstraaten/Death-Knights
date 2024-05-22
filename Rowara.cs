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

    public override void RecibirDanio(int d, string tipo, bool bStun, int id)
    {
        
        float danio = 0;
        if (tipo == "1")
        {
            danio = d * 1.5f;
            maxVida -= Mathf.CeilToInt(danio);
        }
        else if (tipo == "2")
        {
            danio = d * 1f;
            maxVida -= Mathf.CeilToInt(danio);
        }
        Debug.Log("Danio recibido " + danio);
        if (bStun)
        {
            Estuneado = true;
        }
        base.RecibirDanio(d, tipo, bStun, id);
    }
}
