using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miotauk : Controlador
{
    public GameObject minotauk;
    public GameObject arma;
    public GameObject auraParticle;
    public GameObject bodyUlti;
    public GameObject hornsUlti;
    public GameObject axeUlti;
    public GameObject bodyNormal;
    public GameObject hornsNormal;
    public GameObject axeNormal;
    public GameObject legUlti;
    public GameObject armorUlti;
    public GameObject legNormal;
    public GameObject armorNormal;

    public Vector3 scaleChange = new Vector3(0.2f, 0.2f, 0.2f);


    public override void AtaqueSimple()
    {
        base.AtaqueSimple();

    }

    public override void AtaqueEspecial()
    {
        base.AtaqueEspecial();

        // empujar a enemigo (se puede sacar una posicion en el forward del personaje para aplicar fuerza al rBody del enemigo??)
        
        

    }
    public override void AtaqueUlti()
    {
        
        base.AtaqueUlti();
        StartCoroutine(UltiMinotauk());
        
        // como hago para hacer mas da�o

        // como hago que reciba menos da�o 

        // apagar esto a los 7 segundos (corrutina??)
    }
    public IEnumerator UltiMinotauk()
    {
        Debug.Log("Entra");
        yield return new WaitForSeconds(0.5f);
        Instantiate(auraParticle, minotauk.transform, worldPositionStays: false);
        bodyNormal.gameObject.SetActive(false);
        axeNormal.gameObject.SetActive(false);
        hornsNormal.gameObject.SetActive(false);
        bodyUlti.gameObject.SetActive(true);
        hornsUlti.gameObject.SetActive(true);
        axeUlti.gameObject.SetActive(true);
        armorNormal.gameObject.SetActive(false);
        legNormal.gameObject.SetActive(false);
        armorUlti.gameObject.SetActive(true);
        legUlti.gameObject.SetActive(true);
        minotauk.gameObject.transform.localScale += scaleChange; 
        this.Speed = Speed * 1.5f;
        yield return new WaitForSeconds(7);
        bodyNormal.gameObject.SetActive(true);
        axeNormal.gameObject.SetActive(true);
        hornsNormal.gameObject.SetActive(true);
        bodyUlti.gameObject.SetActive(false);
        hornsUlti.gameObject.SetActive(false);
        axeUlti.gameObject.SetActive(false);
        armorNormal.gameObject.SetActive(true);
        legNormal.gameObject.SetActive(true);
        armorUlti.gameObject.SetActive(false);
        legUlti.gameObject.SetActive(false);
        minotauk.gameObject.transform.localScale -= scaleChange;
        this.Speed = 4; // la velocidad de los tanques es de 4 ahora mismo 
        yield return null;
    }

    public override void RecibirDa�o(int d, string tipo, bool bStun, int id)
    {
        base.RecibirDa�o(d, tipo, bStun, id);
        float da�o = 0;
        if (tipo == "1")
        {
            da�o = d * 1.5f;
            currentVida -= Mathf.CeilToInt(da�o);
        }
        else if (tipo == "2")
        {
            da�o = d * 1f;
            currentVida -= Mathf.CeilToInt(da�o);
        }
        Debug.Log("Da�o recibido " + da�o);
        if (bStun)
        {
            Estuneado = true;
        }

    }
}
