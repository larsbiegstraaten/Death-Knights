using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class PrimeraPantalla : MonoBehaviourPunCallbacks
{

    public GameObject BotonJugar;
    public GameObject BotonOKIniciar;
    public GameObject BotonOKRegistro;
    public GameObject BotonSeleccionIniciar;
    public GameObject BotonSeleccionRegistro;
    public GameObject PanelEleccion;
    public GameObject PanelRegistro;
    public GameObject PanelIniciarSesion;
    public GameObject PanelPrincipal;
    public GameObject PanelPrimera;
    public TextMeshProUGUI IndicadorUsuario;

    public TMP_InputField inputName;
    public TMP_InputField inputPassword;
    public TMP_InputField inputCorreo;

    public TMP_InputField loginInputName;
    public TMP_InputField loginInputPwd;

    public string URLregistrar = "https://deathknights.000webhostapp.com/PHP/registerUser.php";
    public string URLlogin = "https://deathknights.000webhostapp.com/PHP/loginUser.php";
    public string nickName;
    public string emailPlayer;
    public string pwdPlayer;

    public int myID;
    public controladorEscenas controladorEscenas;

    public GameObject basesChar;
    public GameObject canvasLobby;
    public alma MiAlma;
    public MenuInicial menuInicial;

    public GameObject popUpAviso;
    public TextMeshProUGUI textoAviso;

    public TextMeshProUGUI textoCoins;
    public int playerCoins;

    public TextMeshProUGUI tNickName;

    public GameObject infoSoloImg;
    public GameObject infoPVPImg;

    //Pruebas


    public void Start()
    { 
        Time.timeScale = 1;
        PhotonNetwork.AutomaticallySyncScene = true;
        playerCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
        IndicadorUsuario.SetText("");
        textoCoins.SetText(playerCoins.ToString());
        if (PlayerPrefs.HasKey("PlayerNickName"))
        {
            nickName = PlayerPrefs.GetString("PlayerNickName");
            PanelEleccion.SetActive(false);
            IndicadorUsuario.SetText("Sesi�n activa: " + nickName);
        }
    }

    public void onlineStart()
    {

            
        if (PlayerPrefs.HasKey("myID"))
        {
            myID = PlayerPrefs.GetInt("myID");
        }
        if (PlayerPrefs.HasKey("PlayerNickName"))
        {
            nickName = PlayerPrefs.GetString("PlayerNickName");
        }
        tNickName.SetText(nickName);
        menuInicial.ModoSeleccionado = 1;
        PhotonNetwork.ConnectUsingSettings();


    }
    public void offlineStart()
    {
        tNickName.SetText(nickName);
        canvasLobby.SetActive(true);
        basesChar.SetActive(true);
        menuInicial.ModoSeleccionado = 2;
        this.gameObject.SetActive(false);
    }


    public void ElegirRegistro()
    {
        PanelEleccion.SetActive(false);
        PanelRegistro.SetActive(true);
    }

    public void ElegirInicioSesion()
    {
        PanelEleccion.SetActive(false);
        PanelIniciarSesion.SetActive(true);
    }

    public void DeInicioSesionAEleccion()
    {
        PanelEleccion.SetActive(true);
        PanelIniciarSesion.SetActive(false);
    }
    public void DeRegistroAEleccion()
    {
        PanelEleccion.SetActive(true);
        PanelRegistro.SetActive(false);
    }

    public void Jugar()
    {
        PhotonNetwork.ConnectUsingSettings();


    }
    public override void OnConnectedToMaster()
    {

        Debug.Log("Conectado al master");
        PhotonNetwork.NickName = nickName + "*" + myID;

        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No hay sala");
        RoomOptions R = new RoomOptions();
        R.MaxPlayers = 6;
        R.IsOpen = true;
        R.IsVisible = true;
        R.PlayerTtl = 2000;
        R.EmptyRoomTtl = 1000;
        R.CleanupCacheOnLeave = true;
        PhotonNetwork.JoinOrCreateRoom("Sala"+nickName, R, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        PlayerPrefs.SetString("PlayerNickName", nickName);
        PlayerPrefs.SetInt("myID", myID);
        PlayerPrefs.Save();
        StartCoroutine(crearAlmas());
        PanelPrimera.SetActive(false);
        basesChar.SetActive(true);
        canvasLobby.SetActive(true);
    }
    public void LanzarPartida()
    {
        if (menuInicial.ModoSeleccionado == 1)
        {
            StartCoroutine(MiAlma.BuscarAlmas());
        }
        if (menuInicial.ModoSeleccionado == 2)
        {
            /*            PhotonNetwork.CurrentRoom.IsOpen = false;  
                        PhotonNetwork.CurrentRoom.IsVisible = false;
                        PhotonNetwork.LeaveRoom();*/  // Esto era para ver si podiamos salir de photon para jugar en local. Daba problemas con el audio y referencias.
                                                      // el problema al hacer esto es que se pierde el alma del jugador
                                                      // hacer instanciacion del personaje local seleccionado en escena 2
            SceneManager.LoadScene(2);
        }

    }
    public IEnumerator crearAlmas()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject g = PhotonNetwork.Instantiate("AlmaJugador", new Vector3(0, 0, 0), Quaternion.identity);
        g.GetComponent<alma>().idBd = myID;
        g.GetComponent<alma>().nickname = nickName;
        MiAlma = g.GetComponent<alma>();
    }
    [PunRPC]
    public void CargarMainScene()
    {
            
        if (PhotonNetwork.IsMasterClient) {
            Debug.Log("Entro en PrimeraPantalla");
            PhotonNetwork.LoadLevel("MainScene");
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
            
    }
    public void registrarJugador()
    {
        string userName = inputName.text;
        string password = inputPassword.text;
        string email = inputCorreo.text;



    }

    public void logIn()
    {
        string userName = loginInputName.text;
        string password = loginInputPwd.text;


        PanelIniciarSesion.SetActive(false);
        PanelRegistro.SetActive(false);
        PanelEleccion.SetActive(false);
    }

    public void cerrarSesion()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    public IEnumerator registerPlayer()
    {
        nickName = inputName.text;
        emailPlayer = inputCorreo.text;
        pwdPlayer = inputPassword.text;
        UnityWebRequest r = new UnityWebRequest();
        WWWForm f = new WWWForm();
        f.AddField("nickname", nickName);
        f.AddField("email", emailPlayer);
        f.AddField("password", pwdPlayer);

        r = UnityWebRequest.Post(URLregistrar, f);
        yield return r.SendWebRequest();

        if (r.result == UnityWebRequest.Result.ConnectionError || r.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + r.error);
            yield return StartCoroutine(montarPopUp(r.error));

        }
        else
        {
            Debug.Log("Server response: " + r.downloadHandler.text);
            IndicadorUsuario.SetText("Sesion activa: " + nickName);
            yield return StartCoroutine(montarPopUp(r.downloadHandler.text));
        }
        
    }

    public IEnumerator montarPopUp(string aviso)
    {
        aviso = aviso.Trim();
        textoAviso.SetText(aviso);
        popUpAviso.SetActive(true);
        yield return new WaitForSeconds(2);
        popUpAviso.SetActive(false);
        
        if(aviso.Equals("Registro completado!"))
        {
            PanelRegistro.SetActive(false);
            PanelIniciarSesion.SetActive(true);
        }
        if(aviso.Equals("Bienvenido/a!"))
        {
            PanelIniciarSesion.SetActive(false);
        }
    }
    public void empezarCorrutinaRegistro()
    {
        StartCoroutine(registerPlayer());
    }
    public void empezarCorrutinaLogIn()
    {
        StartCoroutine(logInPlayer());
    }

    public IEnumerator logInPlayer()
    {
        nickName = loginInputName.text;
        pwdPlayer = loginInputPwd.text;
        UnityWebRequest r = new UnityWebRequest();
        WWWForm f = new WWWForm();
        f.AddField("nickname", nickName);
        f.AddField("password", pwdPlayer);

        r = UnityWebRequest.Post(URLlogin, f);
        yield return r.SendWebRequest();

        if (r.result == UnityWebRequest.Result.ConnectionError || r.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + r.error);
            yield return StartCoroutine(montarPopUp(r.error));
        }
        else
        {
            Debug.Log("Server response: " + r.downloadHandler.text);
            PlayerPrefs.SetString("PlayerNickName", nickName);
            string[] response = (r.downloadHandler.text).Split("*");
            if(response.Length > 1)
            {
                myID = int.Parse(response[2]);
            }          
            IndicadorUsuario.SetText("Sesion activa: " + nickName);
            yield return StartCoroutine(montarPopUp(response[1]));
        }

    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        //MiAlma.GetComponent<alma>().buscartodasalmas();
        Debug.Log("Paso");
    }
    public void infoSolo()
    {
       
        infoSoloImg.gameObject.SetActive(true);
        
    }
    public void infoPVP()
    {
        infoPVPImg.gameObject.SetActive(true);
    }
    public void cerrarPanelInfo()
    {
        infoPVPImg.gameObject.SetActive(false);
        infoSoloImg.gameObject.SetActive(false);
    }
}
