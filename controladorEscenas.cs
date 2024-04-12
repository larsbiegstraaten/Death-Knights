using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using ExitGames.Client.Photon;
using JetBrains.Annotations;
using System.Linq;

public class controladorEscenas : MonoBehaviourPunCallbacks
{
    public GameObject zonaRespawn;
    public GameObject zonaRespawn2;
    public GameObject zonaRespawn3;
    public GameObject zonaRespawn4;
    public GameObject zonaRespawn5;
    public GameObject zonaRespawn6;
    public GameObject zonaRespawn7;
    public GameObject zonaRespawn8;
    public GameObject zonaMuerte;

    public float gameTime = 600f;
    public TextMeshProUGUI timerText;

    public bool PartidaEmpezada;

    public Controlador controlador;


    public alma alma;

    public GameObject jugador;
    public GameObject[] jugadores;

    public List<int> photones;

    public GameObject[] Almas;

    public int KillsT1;
    public int KillsT2;


    public GameObject ZonaFantasma;
    public TextMeshProUGUI team1;
    public TextMeshProUGUI team2;


    public TimerManager timerManager;
    public bool GanadorT1 = false;
    public bool GanadorT2 = false;




    // Start is called before the first frame update
    void Start()
    {
        timerManager = GameObject.Find("timer manager").GetComponent<TimerManager>();
        zonaRespawn = GameObject.Find("ZonaRespawn1");
        zonaRespawn2 = GameObject.Find("ZonaRespawn2");
        alma = GameObject.FindGameObjectWithTag("Alma").GetComponent<alma>();
        Almas = GameObject.FindGameObjectsWithTag("Alma");
        team1 = GameObject.Find("score 1").GetComponent<TextMeshProUGUI>();
        team2 = GameObject.Find("score 2").GetComponent<TextMeshProUGUI>();

        if (PhotonNetwork.InRoom)
        {
            StartCoroutine(ordenarAlmas());
        }
    }
    void Update()
    {

    }

    public GameObject elegirSpawn(int team)
    {
        Debug.Log("team:" + team);
        if (team == 1)
        {

            return zonaRespawn;
        }
        if (team == 2)
        {

            return zonaRespawn2;
        }
        return null;

    }
    public void IdAgresor(int id)
    {
        Debug.LogError("Me ha matado " + id);
        for (int i = 0; i < jugadores.Length; i++)
        {
            if (jugadores[i].GetComponent<Controlador>().ID == id)
            {
                jugadores[i].GetComponent<Controlador>().photonView.RPC("SumarKills", RpcTarget.All);
                
            }
        }
        //CalcularTotalKills();
    }
    public void CalcularTotalKills()
    {
        KillsT1 = 0;
        KillsT2 = 0;
        for (int i = 0;i < jugadores.Length;i++)
        {
            if (jugadores[i].GetComponent<Controlador>().team == 1)
            {
                Debug.LogError("Team: 1" + jugadores[i].GetComponent<Controlador>().team);
                Debug.LogError("Kills: " + jugadores[i].GetComponent<Controlador>().kills);
                KillsT1 += jugadores[i].GetComponent<Controlador>().kills;
            }
            if (jugadores[i].GetComponent<Controlador>().team == 2)
            {
                Debug.LogError("Team: 2" + jugadores[i].GetComponent<Controlador>().team);
                Debug.LogError("Kills: " + jugadores[i].GetComponent<Controlador>().kills);
                KillsT2 += jugadores[i].GetComponent<Controlador>().kills;
            }
        }
        team1.SetText(KillsT1.ToString());
        team2.SetText(KillsT2.ToString());
        ComprobarWinner();
    }
    public void ComprobarWinner()
    {
        if (KillsT1 >= 15)
        {
            timerManager.FinPartida(1);
        }
        if (KillsT2 >= 15)
        {
            timerManager.FinPartida(2);
        }

    }
    [PunRPC]
    public void EmpezarPartida(bool p)
    {
        PartidaEmpezada = p;
    }
    public IEnumerator ordenarAlmas()
    {
        yield return new WaitForSeconds(1f);
        jugadores = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < jugadores.Length; i++)
        {
            photones.Add(jugadores[i].GetPhotonView().ViewID);

        }
        for (int i = 0; i < photones.Count; i++)
        {
            if (photones[i] == alma.idPhoton)
            {
                Debug.Log("Coinciden IDS en script controlador escenas");
            }
        }
    }
    [PunRPC]
    public void addKilltoPlayer(int id)
    {
        for (int i = 0; i < photones.Count; i++)
        {
            if (Almas[i].GetComponent<PhotonView>().ViewID == id)
            {
                Almas[i].GetComponent<Controlador>().kills++;
                Debug.Log("Ha matado el id : " + id);
                break;
            }
        }
    }
}