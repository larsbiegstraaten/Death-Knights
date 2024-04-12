using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class alma : MonoBehaviour
{
    public int idBd;
    public int characterSelected;
    public int team;
    public string nickname;
    public int idPhoton;
    public bool soyMaster = false;
    public GameObject[] Almas;
    public string[] Datos;
    public PhotonView photonView;
    public PrimeraPantalla pp;
    public controladorEscenas SceneController;
    public GameObject miAlma;

    public GameObject jugador;
    public GameObject[] jugadores;

    public List<int> photones;
    public TextMeshProUGUI TextoPrueba;

    public int deaths;
    public int kills;

    public GameObject elManager;

    public int idTemp;
    // Start is called before the first frame update
    void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        elManager = GameObject.FindGameObjectWithTag("scoreManager");
        DontDestroyOnLoad(this.gameObject);
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    soyMaster = true;
        //    if (elManager == null)
        //    {
        //        Debug.Log("entra al if de crear manager");
        //        PhotonNetwork.InstantiateRoomObject("scoreManager", new Vector3(0, 0, 0), Quaternion.identity);
        //        elManager = GameObject.FindGameObjectWithTag("scoreManager");
        //    }

        //}
        //else
        //{
        //    soyMaster = false;
        //}
        idPhoton = photonView.ViewID;


        if (photonView.IsMine)
        {
            photonView.RPC("SetNameAlma", RpcTarget.All);

        }


    }
    [PunRPC]
    public void SetNameAlma()
    {

        nickname = photonView.Controller.NickName;
        gameObject.name = "Alma " + photonView.Controller.NickName;
        pp = GameObject.Find("PrimeraPantalla").GetComponent<PrimeraPantalla>();
        buscartodasalmas();
        for (int i = 0; i < Almas.Length; i++)
        {
            Almas[i].GetComponent<PhotonView>().RPC("buscartodasalmas", RpcTarget.All);
        }


    }

    [PunRPC]
    public void buscartodasalmas()
    {
        pp = GameObject.Find("PrimeraPantalla").GetComponent<PrimeraPantalla>();
        Almas = GameObject.FindGameObjectsWithTag("Alma");
        nickname = photonView.Controller.NickName;
        gameObject.name = "Alma " + photonView.Controller.NickName;
        Datos = photonView.Controller.NickName.Split('*');
        idBd = int.Parse(Datos[1]);
    }
    // Update is called once per frame
    public void recorrerAlmas()
    {
        for (int i = 0; i < Almas.Length; i++)
        {
            //if (Almas[i]
        }
    }
    void Update()
    {
        if (TextoPrueba != null)
        {
            //TextoPrueba.text = "Equipo MIo: " + team;
        }

    }

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("Escena cargada " + level);
        if (photonView.IsMine && level == 1)
        {
            SceneController = GameObject.FindGameObjectWithTag("mapa1").GetComponent<controladorEscenas>();
            SceneController = GameObject.Find("Mapa1").GetComponent<controladorEscenas>();
            Debug.Log("Level 2 was loaded, Team: " + team);
            idTemp = GetComponent<PhotonView>().ViewID;
            //GameObject o = PhotonNetwork.Instantiate("Personaje", GameObject.Find("Mapa1").GetComponent<controladorEscenas>().elegirSpawn(team).transform.position, Quaternion.identity);
            //o.GetComponent<Controlador>().alma = gameObject.GetComponent<alma>();
            GameObject ob = PhotonNetwork.Instantiate("Personaje", GameObject.Find("Mapa1").GetComponent<controladorEscenas>().elegirSpawn(team).transform.position, Quaternion.identity);
            string x = ob.GetComponent<PhotonView>().Controller.NickName;
            //photonView.RPC("ReferenciaAlma", RpcTarget.All);
            //idPhoton = o.GetComponent<PhotonView>().ViewID;
            //o.GetComponent<Controlador>().ID = idBd;
            //Debug.Log("Team es " + team);
            //o.GetComponent<Controlador>().team = team;
        }
    }
    [PunRPC]
    public IEnumerator ReferenciaAlma()
    {
        yield return new WaitForSeconds(1f);
        GameObject o = GameObject.Find("Alma " + nickname);
        GameObject y = GameObject.Find("Personaje " + nickname);
        y.GetComponent<Controlador>().alma = o.GetComponent<alma>();
    }


    [PunRPC]
    public void DefinirEquipos()
    {
        jugadores = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < Almas.Length; i++)
        {
            if (i % 2 == 0)
            {
                Almas[i].GetComponent<alma>().photonView.RPC("AsignarEquipo", RpcTarget.All, 1);
            }
            else
            {
                Almas[i].GetComponent<alma>().photonView.RPC("AsignarEquipo", RpcTarget.All, 2);
            }
        }
        for (int i = 0; i < jugadores.Length; i++)
        {
            photones.Add(jugadores[i].GetPhotonView().ViewID);

        }
        for (int i = 0; i < photones.Count; i++)
        {
            if (photones[i] == Almas[i].GetPhotonView().ViewID)
            {
                Debug.Log("Coinciden IDS en script alma");
            }
        }

    }
    [PunRPC]
    public void AsignarEquipo(int x)
    {
        team = x;
    }
    [PunRPC]
    public IEnumerator BuscarAlmas()
    {
        Debug.Log("Buscando " + gameObject.name);
        Almas = GameObject.FindGameObjectsWithTag("Alma");
        yield return new WaitForSeconds(1f);
        Debug.Log("Almas - " + Almas.Length);
        for (int i = 0; i < Almas.Length; i++)
        {
            if (Almas[i].GetComponent<alma>().soyMaster)
            {
                Almas[i].GetComponent<alma>().photonView.RPC("DefinirEquipos", RpcTarget.All);

                break;
            }

        }
        DefinirEquipos();
        yield return new WaitForSeconds(1f);
        if (photonView.IsMine && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("MainScene");
        }
    }

    public IEnumerator BuscarAl()
    {
        Debug.Log("Buscando " + gameObject.name);
        Almas = GameObject.FindGameObjectsWithTag("Alma");
        yield return new WaitForSeconds(1f);
        Debug.Log("Almas - " + Almas.Length);
        for (int i = 0; i < Almas.Length; i++)
        {
            if (Almas[i].GetComponent<PhotonView>().AmOwner)
            {
                // este es mi alma creo
                miAlma = Almas[i].GameObject();
                Debug.Log(miAlma.name);
                break;
            }

        }
    }
    public void OnButtonLoadMainSceneClicked()
    {
        PhotonView photonView = GetComponent<PhotonView>();
        photonView.RPC("CargarMainScene", RpcTarget.All);
    }

    [PunRPC]
    public void CargarMainScene()
    {
        Debug.Log("Entro en PrimeraPantalla");
        PhotonNetwork.LoadLevel("MainScene");
        PhotonNetwork.CurrentRoom.IsOpen = false;
    }

    [PunRPC]
    public void identificarDeaths()
    {
        deaths++;
    }

    [PunRPC]
    public void addKill()
    {
        kills++;
        if (this.team == 1)
        {
            SceneController.KillsT1++;
        }
        else if (this.team == 2)
        {
            SceneController.KillsT2++;
        }
    }
    [PunRPC]
    public void identificarKills(int id)
    {
        SceneController.addKilltoPlayer(id);
    }
}