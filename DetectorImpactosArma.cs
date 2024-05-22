using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorImpactosArma : MonoBehaviour
{
    public Controlador C;
    public GameObject particula;
    public int Danio;
    public string tipo;
    public bool HaceStun;
    public int idPlayer;
    public controladorEscenas ce;
    public GameObject personaje;
    public PhotonView photon;

    public GameObject[] almas;

    public int idAgresor;
    public bool invulnerable;
    public GameObject prueba;
    public enum TipoAtaque { Q, E, simple }
    public TipoAtaque esteTipo;


    // Start is called before the first frame update
    void Start()
    {
        ce = GameObject.FindGameObjectWithTag("mapa1").GetComponent<controladorEscenas>();
        idPlayer = personaje.GetComponent<Controlador>().ID;
        photon = personaje.GetPhotonView();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (photon.IsMine && other.gameObject.layer == 6 && !invulnerable)
        {
            invulnerable = true;

            if (personaje.gameObject.GetComponent<Controlador>().team != other.gameObject.GetComponent<Controlador>().team)
            {
                Debug.Log("Impacta con " + other.transform.name);
                C.Impactar();
                if (esteTipo == TipoAtaque.simple)
                {
                    PhotonNetwork.Instantiate("ImpactoSimplePedro", other.transform.position, Quaternion.identity);
                }
                if (esteTipo == TipoAtaque.Q)
                {
                    PhotonNetwork.Instantiate("explosionPedro", other.transform.position, Quaternion.identity);
                }
                if (esteTipo == TipoAtaque.E)
                {
                    PhotonNetwork.Instantiate("UltiPedroPart", other.transform.position, Quaternion.identity);
                }
                other.GetComponent<Controlador>().photonView.RPC("recibirdaniorpc", RpcTarget.All, Danio, tipo, HaceStun, idPlayer);
            }
            else
            {
                Debug.Log("Somos del mismo equipo");
            }
            StartCoroutine(resetInvul());

        }

    }

    public IEnumerator resetInvul()
    {
        yield return new WaitForSeconds(0.5f);
        invulnerable = false;
    }

}