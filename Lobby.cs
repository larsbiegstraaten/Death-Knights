using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Lobby : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI TextoInfo;
    public GameObject BotonStart;
    public string NickName;


    public enum Team { Team1, Team2 };
    public List<Photon.Realtime.Player> team1List = new List<Photon.Realtime.Player>();
    public List<Photon.Realtime.Player> team2List = new List<Photon.Realtime.Player>();
    public List<string> jugadores;

    public 



    // Start is called before the first frame update
    void Start()
    {
        /*        PhotonNetwork.AutomaticallySyncScene = true;
                var auth = new Supabase.Gotrue.Client(new ClientOptions<Session>
                {
                    Url = "https://njzxqarrcvbabhhtdkri.supabase.co/auth/v1",
                    Headers = new Dictionary<string, string>
                    {
                        { "apikey", APIKey }
                    }
                });*/

    //    {
    //        // Verificar si el jugador está conectado a la red
    //        if (!PhotonNetwork.IsConnected)
    //        {
    //            Debug.LogError("No estás conectado a la red de Photon.");
    //            return;
    //        }

    //        // Verificar si el jugador está en una sala
    //        if (PhotonNetwork.CurrentRoom == null)
    //        {
    //            Debug.LogError("No estás en una sala.");
    //            return;
    //        }

    //        // Obtener la cantidad de jugadores en la sala
    //        int totalJugadores = PhotonNetwork.CurrentRoom.PlayerCount;

    //        // Calcular el número de jugadores por equipo
    //        int jugadoresPorEquipo = totalJugadores / 2;
    //        int jugadoresExtras = totalJugadores % 2; // Para manejar el caso donde la división no es exacta

    //        // Variables para contar jugadores asignados a cada equipo
    //        int jugadoresEquipo1 = 0;
    //        int jugadoresEquipo2 = 0;

    //        // Asignar jugadores a equipos de manera equilibrada
    //        for (int i = 0; i < totalJugadores; i++)
    //        {
    //            // Determinar a qué equipo asignar el jugador
    //            int equipo = (jugadoresEquipo1 < jugadoresPorEquipo) ? 1 : 2;

    //            // Asignar al equipo adecuado
    //            AsignarJugadorAEquipo(i, equipo);

    //            // Actualizar el conteo de jugadores por equipo
    //            if (equipo == 1)
    //            {
    //                jugadoresEquipo1++;
    //            }
    //            else
    //            {
    //                jugadoresEquipo2++;
    //            }
    //        }
    //    }
    //}
    //void AsignarJugadorAEquipo(int jugadorIndex, int equipo)
    //{
    //    // Identificar al jugador por su índice (suponiendo que los índices comienzan desde 0)
    //    Photon.Realtime.Player[] jugadores = PhotonNetwork.PlayerList;
    //    Photon.Realtime.Player jugador = jugadores[jugadorIndex];

    //    // Marcar al jugador con su equipo asignado
    //    jugador.CustomProperties["Equipo"] = equipo;

    //    // Puedes también enviar mensajes a los jugadores o actualizar la interfaz de usuario según su equipo, etc.
    //    Debug.Log("Jugador " + jugador.NickName + " asignado al Equipo " + equipo);
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.InRoom)
            {
              //  TextoInfo.text = "Sala: " + PhotonNetwork.CurrentRoom.Name + " - Players: " + PhotonNetwork.CurrentRoom.PlayerCount + " - Region: " + PhotonNetwork.CloudRegion;
                if (PhotonNetwork.IsMasterClient)
                {
                    BotonStart.SetActive(true);
                }
                else {
                    BotonStart.SetActive(false);
                }
            }
            else {
               // TextoInfo.text = "No estas en sala";
            }
            
        }
        else {
          //  TextoInfo.text = "No Conectado";
        }
    }
    public override void OnConnectedToMaster(){

        Debug.Log("Conectado al master");

        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No hay sala");
        RoomOptions R = new RoomOptions();
        R.MaxPlayers = 20;
        R.IsOpen = true;
        R.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom("SalaGuay", R, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Unido a Sala");


        // aquí dividir en equipos
        //aqui rellenar lista de jugadores con "Players" de photon
        // ObtenerJugadoresConectados();



    }
    public void Jugar()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public void StartGame() {
        Debug.Log("Empezar Partida");

       // DistribuirJugadores(jugadores);
        Debug.Log("El equipo 1 es: " + Team.Team1.ToString() + "    Y el equipo 2 es: " + Team.Team2.ToString());
        PhotonNetwork.LoadLevel("MainScene");
      //  PhotonNetwork.CurrentRoom.IsOpen = false;

    }
    public void empezarJuegoPrueba()
    {
        SceneManager.LoadScene(2);
    }

   /* public void AddPlayerToTeam(Team team, Photon.Realtime.Player player)
    {
        if (team == Team.Team1)
        {

        }
        if (team == Team.Team2)
        {

        }

    }*/
/*    public Dictionary<Team, List<string>> DistribuirJugadores(List<string> jugadores)
    {
        Dictionary<Team, List<string>> equipos = new Dictionary<Team, List<string>>();
        equipos.Add(Team.Team1, new List<string>());
        equipos.Add(Team.Team2, new List<string>());

        int numJugadores = jugadores.Count;
        int equipoActual = 0;

        // Ordena los jugadores de manera aleatoria
        jugadores.Sort();

        for (int i = 0; i < numJugadores; i++)
        {
            // Asigna el jugador al equipo actual
            equipos[(Team)equipoActual].Add(jugadores[i]);

            // Cambia al siguiente equipo
            equipoActual = (equipoActual + 1) % 2;
        }

        return equipos;
       
    }

    void ObtenerJugadoresConectados()
    {
        // Verificamos que estemos en una sala
        if (PhotonNetwork.InRoom)
        {
            // Iteramos sobre todos los jugadores en la sala
            foreach (Photon.Realtime.Player jugador in PhotonNetwork.PlayerList)
            {
                // Agregamos el nombre del jugador a la lista de jugadores
                jugadores.Add(jugador.UserId);
            }
        }
    }*/

}
