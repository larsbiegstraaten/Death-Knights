using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon;


public class MenuInicial : MonoBehaviourPunCallbacks
{

    public GameObject CanvasInicial;
    public GameObject PanelInicial;
    public GameObject Minotauk;
    public GameObject Minotauk2;
    public GameObject Ganell;
    public GameObject Ganell2;
    public GameObject Maius;
    public GameObject Maius2;
    public GameObject Benedicto;
    public GameObject Benedicto2;
    public GameObject Tomb;
    public GameObject Tomb2;
    public GameObject Rowara;
    public GameObject Rowara2;
    public GameObject Pedro;
    public GameObject Pedro2;
    public GameObject Wolfkrat;
    public GameObject Wolfkrat2;
    public GameObject BaseInicial;
    public GameObject BaseCompanero1;
    public GameObject BaseCompanero2;
    public GameObject BaseMinotauk;
    public GameObject BaseGanell;
    public GameObject BaseMaius;
    public GameObject BaseBenedicto;
    public GameObject BaseTomb;
    public GameObject BaseRowara;
    public GameObject BasePedro;
    public GameObject BaseWolfKrat;
    public GameObject PanelPersonajes;
    public GameObject PanelAjustes;
    public GameObject PanelAmigos;
    public GameObject PanelTienda;
    public GameObject PanelMinotauk;
    public GameObject PanelGanell;
    public GameObject PanelMaius;
    public GameObject PanelBenedicto;
    public GameObject PanelTomb;
    public GameObject PanelRowara;
    public GameObject PanelPedro;
    public GameObject PanelWolfKrat;
    public GameObject PanelModos;
    public GameObject BotonSelecPersonajes;
    public GameObject Modo1;
    public TextMeshProUGUI TextoModoSeleccionado;
    public int PersonajeSeleccionado;
    public int ModoSeleccionado; 
    public int Amigo1;
    public int Amigo2;
    public GameObject[] Modelos;
    public TextMeshProUGUI textoNumJugadores;
    public TextMeshProUGUI textoMonedas;
    public GameObject imagenMonedas;



    // Start is called before the first frame update
    void Start()
    {
        if (ModoSeleccionado == 1)
        {
            textoNumJugadores.gameObject.SetActive(true);
        }
        PersonajeSeleccionado = PlayerPrefs.GetInt("Modelos");

        Minotauk.transform.localScale = new Vector3(70, 70, 70);

        Ganell.transform.position = new Vector3(510, 124, -20.993f);
        Ganell.transform.localScale = new Vector3(80.79f, 80.79f, 80.79f);

        Maius.transform.localScale = new Vector3(103.407f, 103.407f, 103.407f);
        Maius.transform.position = new Vector3(533.87f, 123.06f, 4);

        Benedicto.transform.localScale = new Vector3(93.2145f, 93.2145f, 93.2145f);
        Benedicto.transform.position = new Vector3(527, 124, 5);

        Tomb.transform.localScale = new Vector3(94.15271f, 94.15271f, 94.15271f);
        Tomb.transform.position = new Vector3(526, 123, -9); 
        
        Rowara.transform.localScale = new Vector3(94.15271f, 94.15271f, 94.15271f);
        Rowara.transform.position = new Vector3(526, 123, -9);
        
        Pedro.transform.localScale = new Vector3(94.15271f, 94.15271f, 94.15271f);
        Pedro.transform.position = new Vector3(526, 123, -9);

        Wolfkrat.transform.localScale = new Vector3(96.90599f, 98.40693f, 81.70673f);
    }

    // Update is called once per frame
    void Update()
    {
        if(ModoSeleccionado == 1)
        {
            textoNumJugadores.SetText(PhotonNetwork.CurrentRoom.PlayerCount.ToString() + " / " + PhotonNetwork.CurrentRoom.MaxPlayers.ToString()) ;
        }
        for (int i = 0; i < Modelos.Length; i++)
        {
            if (i != PersonajeSeleccionado)
            {
                Modelos[i].SetActive(false);
            }
        }
        switch (PersonajeSeleccionado)
        {
            case 0:
                Modelos[0].SetActive(true);
                break;
            case 1:
                Modelos[1].SetActive(true);
                break;
            case 2:
                Modelos[2].SetActive(true);
                break;
            case 3:
                Modelos[3].SetActive(true);
                break;
            case 4:
                Modelos[4].SetActive(true);
                break;
            case 5:
                Modelos[5].SetActive(true);
                break;
            case 6:
                Modelos[6].SetActive(true);
                break;
            case 7:
                Modelos[7].SetActive(true);
                break;
            case 8:
                Modelos[8].SetActive(true);
                break;
            default:
                break;

        }

        if (ModoSeleccionado == 1)
        {
            TextoModoSeleccionado.SetText("Modo Seleccionado: Zona Caliente 3vs3");
        }
        if (ModoSeleccionado == 2)
        {
            TextoModoSeleccionado.SetText("Modo Seleccionado: Supervivencia");
        }
        else
        {
            TextoModoSeleccionado.SetText("Modo Seleccionado:");
        }
    }

    public void AbrirPanelPersonajes()
    {
        PanelPersonajes.gameObject.SetActive(true);
        PanelInicial.gameObject.SetActive(false);    
        BaseInicial.gameObject.SetActive(false);
        BaseCompanero1.gameObject.SetActive(false);
        BaseCompanero2.gameObject.SetActive(false);
        Modelos[PersonajeSeleccionado].SetActive(false);
        textoMonedas.enabled = false;
        imagenMonedas.gameObject.SetActive(false);

        if (PersonajeSeleccionado != 0)
        {
            PersonajeSeleccionado = 10;
        }
        else
        {
            PersonajeSeleccionado = 9;
        }
    }

    public void AbrirPanelMinotauk()
    {
        PanelMinotauk.gameObject.SetActive(true);
        Minotauk2.gameObject.SetActive(true);
        PanelPersonajes.gameObject.SetActive(false);
        Minotauk2.transform.localScale = new Vector3(81, 81, 81);
        Minotauk2.transform.position = new Vector3(533.5f, 133.6f, -13.76f);
        BaseMinotauk.SetActive(true);
        BaseMinotauk.transform.localScale = new Vector3(180.7184f, 7.052053f, 130.8706f);
        BaseMinotauk.transform.position = new Vector3(540.5f, 122.1f, 0.2f);
    }

    public void SeleccionarMinotauk()
    {
        PersonajeSeleccionado = 1;
        PanelMinotauk.gameObject.SetActive(false);
        Minotauk.transform.localScale = new Vector3(70,70,70);
        Minotauk2.gameObject.SetActive(false);
        BaseMinotauk.gameObject.SetActive(false);
        SeleccionarPersonaje(); 
        
    }

    public void CerrarPanelMinotauk() 
    { 
        PanelPersonajes.gameObject.SetActive(true);
        PanelMinotauk.gameObject.SetActive(false);
        Minotauk2.gameObject.SetActive(false);
        BaseMinotauk.gameObject.SetActive(false);
    }
    public void AbrirPanelGanell()
    {
        PanelGanell.gameObject.SetActive(true);
        Ganell2.gameObject.SetActive(true);
        Ganell2.transform.position = new Vector3(511, 120, -21.126f);
        Ganell2.transform.localScale = new Vector3(93, 93, 93);
        BaseGanell.gameObject.SetActive(true);
        BaseGanell.transform.position = new Vector3(530.3f, 113.5f, -11.2f);
        BaseGanell.transform.localScale = new Vector3(132.5522f, 4.221602f, 100.3884f);
        PanelPersonajes.gameObject.SetActive(false); 
    }

    public void SeleccionarGanell()
    {
        PersonajeSeleccionado = 2;
        PanelGanell.gameObject.SetActive(false);
        Ganell.transform.position = new Vector3(510, 124, -20.993f);
        Ganell.transform.localScale = new Vector3(80.79f, 80.79f, 80.79f);
        Ganell2.gameObject.SetActive(false);
        BaseGanell.gameObject.SetActive(false);
        SeleccionarPersonaje();
    }

    public void CerrarPanelGanel() 
    { 
        PanelPersonajes.gameObject.SetActive(true);
        PanelGanell.gameObject.SetActive(false);
        Ganell2.gameObject.SetActive(false);
        BaseGanell.gameObject.SetActive(false);
    } 
    public void AbrirPanelMaius()
    {
        PanelMaius.gameObject.SetActive(true);
        Maius2.gameObject.SetActive(true);
        Maius2.transform.localScale = new Vector3(115.56f, 115.56f, 115.56f);
        Maius2.transform.position = new Vector3(530, 118, -17.829f);
        BaseMaius.gameObject.SetActive(true);
        BaseMaius.transform.position = new Vector3(531.7f, 106.7f, -7.4f);
        BaseMaius.transform.localScale = new Vector3(167.9364f, 5.074357f, 127.1867f);
        PanelPersonajes.gameObject.SetActive(false); 
    }

    public void SeleccionarMaius()
    {
        PersonajeSeleccionado = 6;
        PanelMaius.gameObject.SetActive(false);
        Maius.transform.localScale = new Vector3(103.407f, 103.407f, 103.407f);
        Maius.transform.position = new Vector3(533.87f, 123.06f, 4);
        Maius2.gameObject.SetActive(false);
        BaseMaius.SetActive(false);
        SeleccionarPersonaje();
    }

    public void CerrarPanelMaius() 
    { 
        PanelPersonajes.gameObject.SetActive(true);
        PanelMaius.gameObject.SetActive(false);
        Maius2.gameObject.SetActive(false);
        BaseMaius.gameObject.SetActive(false);
    }
    
    public void AbrirPanelBenedicto()
    {
        PanelBenedicto.gameObject.SetActive(true);
        Benedicto2.gameObject.SetActive(true);
        Benedicto2.transform.localScale = new Vector3(106.3728f, 106.3728f, 106.3728f);
        Benedicto2.transform.position = new Vector3(527, 117, -21);
        BaseBenedicto.SetActive(true);
        BaseBenedicto.transform.position = new Vector3(528.4f, 98.5f, 18);
        BaseBenedicto.transform.localScale = new Vector3(162.4525f, 4.319056f, 123.0335f);
        PanelPersonajes.gameObject.SetActive(false); 
    }

    public void SeleccionarBenedicto()
    {
        PersonajeSeleccionado = 3;
        PanelBenedicto.gameObject.SetActive(false);
        Benedicto.transform.localScale = new Vector3(93.2145f, 93.2145f, 93.2145f);
        Benedicto.transform.position = new Vector3(527, 124, 5);
        Benedicto2.SetActive(false);
        BaseBenedicto.SetActive(false);
        SeleccionarPersonaje();
    }

    public void CerrarPanelBenedicto() 
    { 
        PanelPersonajes.gameObject.SetActive(true);
        PanelBenedicto.gameObject.SetActive(false);
        Benedicto2.gameObject.SetActive(false);
        BaseBenedicto.SetActive(false);
    }
    public void AbrirPanelTomb()
    {
        PanelTomb.gameObject.SetActive(true);
        Tomb2.gameObject.SetActive(true);
        Tomb2.transform.localScale = new Vector3(106.3728f, 106.3728f, 106.3728f);
        Tomb2.transform.position = new Vector3(529, 119, -26);
        BaseTomb.SetActive(true);
        BaseTomb.transform.position = new Vector3(528.7f, 101.3f, 7.3f);
        BaseTomb.transform.localScale = new Vector3(165.6151f, 5.058348f, 108.9209f);
        PanelPersonajes.gameObject.SetActive(false); 
    }

    public void SeleccionarTomb()
    {
        PersonajeSeleccionado = 7;
        PanelTomb.gameObject.SetActive(false);
        Tomb.transform.localScale = new Vector3(94.15271f, 94.15271f, 94.15271f);
        Tomb.transform.position = new Vector3(526, 123, -9);
        Tomb2.SetActive(false);
        BaseTomb.SetActive(false);
        SeleccionarPersonaje();
    }

    public void CerrarPanelTomb() 
    { 
        PanelPersonajes.gameObject.SetActive(true);
        PanelTomb.gameObject.SetActive(false);
        Tomb2.gameObject.SetActive(false);
        BaseTomb.SetActive(false);
    }
    public void AbrirPanelRowara()
    {
        PanelRowara.gameObject.SetActive(true);
        Rowara2.gameObject.SetActive(true);
        Rowara2.transform.localScale = new Vector3(106.3728f, 106.3728f, 106.3728f);
        Rowara2.transform.position = new Vector3(529, 119, -26);
        BaseRowara.SetActive(true);
        BaseRowara.transform.position = new Vector3(527.1f, 104.1f, -2.3f);
        BaseRowara.transform.localScale = new Vector3(170.0128f, 5.723261f, 128.7592f);
        PanelPersonajes.gameObject.SetActive(false); 
    }

    public void SeleccionarRowara()
    {
        PersonajeSeleccionado = 4;
        PanelRowara.gameObject.SetActive(false);
        Rowara.transform.localScale = new Vector3(94.15271f, 94.15271f, 94.15271f);
        Rowara.transform.position = new Vector3(526, 123, -9);
        Rowara2.SetActive(false);
        BaseRowara.SetActive(false);
        SeleccionarPersonaje();
    }

    public void CerrarPanelRowara() 
    { 
        PanelPersonajes.gameObject.SetActive(true);
        PanelRowara.gameObject.SetActive(false);
        Rowara2.gameObject.SetActive(false);
        BaseRowara.SetActive(false);
    }
    public void AbrirPanelPedro()
    {
        PanelPedro.gameObject.SetActive(true);
        Pedro2.gameObject.SetActive(true);
        Pedro2.transform.localScale = new Vector3(106.3728f, 106.3728f, 106.3728f);
        Pedro2.transform.position = new Vector3(529, 119, -26);
        BasePedro.SetActive(true);
        BasePedro.transform.position = new Vector3(527.3f, 104.5f, 7.8f);
        BasePedro.transform.localScale = new Vector3(177.2861f, 4.953603f, 134.2677f);
        PanelPersonajes.gameObject.SetActive(false); 
    }

    public void SeleccionarPedro()
    {
        PersonajeSeleccionado = 8;
        PanelPedro.gameObject.SetActive(false);
        Pedro.transform.localScale = new Vector3(94.15271f, 94.15271f, 94.15271f);
        Pedro.transform.position = new Vector3(526, 123, -9);
        Pedro2.SetActive(false);
        BasePedro.SetActive(false);
        SeleccionarPersonaje();
    }

    public void CerrarPanelPedro() 
    { 
        PanelPersonajes.gameObject.SetActive(true);
        PanelPedro.gameObject.SetActive(false);
        Pedro2.gameObject.SetActive(false);
        BasePedro.SetActive(false);
    }
    public void AbrirPanelWolfKrat()
    {
        PanelPersonajes.SetActive(false);
        PanelWolfKrat.gameObject.SetActive(true);
        Wolfkrat2.gameObject.SetActive(true);
        Wolfkrat2.transform.localScale = new Vector3(68.95303f, 68.95303f, 68.95303f);
        Wolfkrat2.transform.position = new Vector3(528, 165.9f, -118.4f);
        BaseWolfKrat.SetActive(true);
        BaseWolfKrat.transform.position = new Vector3(529, 146, -88);
        BaseWolfKrat.transform.localScale = new Vector3(145.406f, 4.849745f, 99.93149f);
        PanelPersonajes.gameObject.SetActive(false); 
    }

    public void SeleccionarWolfKrat()
    {
        PersonajeSeleccionado = 5;
        PanelWolfKrat.gameObject.SetActive(false);
        Wolfkrat.transform.localScale = new Vector3(96.90599f, 98.40693f, 81.70673f);
        Wolfkrat.transform.position = new Vector3(531, 123, 9);
        Wolfkrat2.SetActive(false);
        BaseWolfKrat.SetActive(false);
        SeleccionarPersonaje();
    }

    public void CerrarPanelWolfKrat() 
    { 
        PanelPersonajes.gameObject.SetActive(true);
        PanelWolfKrat.gameObject.SetActive(false);
        Wolfkrat2.gameObject.SetActive(false);
        BaseWolfKrat.SetActive(false);
    }


    public void DePersonajesAMenuInicial()
    {
        PanelPersonajes.gameObject.SetActive(false);
        PanelInicial.gameObject.SetActive(true);
        BaseInicial.gameObject.SetActive(true);
        BaseCompanero1.gameObject.SetActive(true);
        BaseCompanero2.gameObject.SetActive(true);
        textoMonedas.enabled = true;
        imagenMonedas.gameObject.SetActive(true);
        if (PersonajeSeleccionado == 9)
        {
            PersonajeSeleccionado = 0;
        }

        PersonajeSeleccionado = PlayerPrefs.GetInt("Modelos");
        Debug.Log(PlayerPrefs.GetInt("Modelos"));
        
    }

    public void AbrirPanelAjustes()
    {
        PanelAjustes.gameObject.SetActive(true);
        PanelInicial.gameObject.SetActive(false);
        BaseInicial.gameObject.SetActive(false);
        BaseCompanero1.gameObject.SetActive(false);
        BaseCompanero2.gameObject.SetActive(false);
        textoMonedas.enabled = false;
        imagenMonedas.gameObject.SetActive(false);
        PersonajeSeleccionado = 9;
    }

    public void CerrarPanelAjustes()
    {
        PanelAjustes.gameObject.SetActive(false);
        PanelInicial.gameObject.SetActive(true);
        BaseInicial.gameObject.SetActive(true);
        BaseCompanero1.gameObject.SetActive(true);
        BaseCompanero2.gameObject.SetActive(true);
        PersonajeSeleccionado = PlayerPrefs.GetInt("Modelos");
        textoMonedas.enabled = true;
        imagenMonedas.gameObject.SetActive(true);
    }

    public void AbrirPanelAmigos()
    {
        PanelAmigos.gameObject.SetActive(true);
        PanelInicial.gameObject.SetActive(false);
        BaseInicial.gameObject.SetActive(false);
        BaseCompanero1.gameObject.SetActive(false);
        BaseCompanero2.gameObject.SetActive(false);
        textoMonedas.enabled = false;
        imagenMonedas.gameObject.SetActive(false);
        PersonajeSeleccionado = 9;
    }
    public void CerrarPanelAmigos()
    {
        PanelAmigos.gameObject.SetActive(false);
        PanelInicial.gameObject.SetActive(true);
        BaseInicial.gameObject.SetActive(true);
        BaseCompanero1.gameObject.SetActive(true);
        BaseCompanero2.gameObject.SetActive(true);
        PersonajeSeleccionado = PlayerPrefs.GetInt("Modelos");
        textoMonedas.enabled = true;
        imagenMonedas.gameObject.SetActive(true);
    }
    public void AbrirPanelTienda()
    {
        PanelTienda.gameObject.SetActive(true);
        PanelInicial.gameObject.SetActive(false);
        BaseInicial.gameObject.SetActive(false);
        BaseCompanero1.gameObject.SetActive(false);
        BaseCompanero2.gameObject.SetActive(false);
        textoMonedas.enabled = false;
        imagenMonedas.gameObject.SetActive(false);
        PersonajeSeleccionado = 9;
    }
    public void CerrarPanelTienda()
    {
        PanelTienda.gameObject.SetActive(false);
        PanelInicial.gameObject.SetActive(true);
        BaseInicial.gameObject.SetActive(true);
        BaseCompanero1.gameObject.SetActive(true);
        BaseCompanero2.gameObject.SetActive(true);
        PersonajeSeleccionado = PlayerPrefs.GetInt("Modelos");
        textoMonedas.enabled = true;
        imagenMonedas.gameObject.SetActive(true);
    }
    public void AbrirPanelModos()
    {
        if(ModoSeleccionado == 1)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }
        else if(ModoSeleccionado == 2)
        {
            SceneManager.LoadScene(0);
        }
       
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void CerrarPanelModos()
    {
        PanelModos.gameObject.SetActive(false);
        PanelInicial.gameObject.SetActive(true);
        BaseInicial.gameObject.SetActive(true);
        BaseCompanero1.gameObject.SetActive(true);
        BaseCompanero2.gameObject.SetActive(true);
        PersonajeSeleccionado = PlayerPrefs.GetInt("Modelos");
    }

    public void SeleccionarModo1()
    {
        PanelModos.gameObject.SetActive(false);
        PanelInicial.gameObject.SetActive(true);
        BaseInicial.gameObject.SetActive(true);
        BaseCompanero1.gameObject.SetActive(true);
        BaseCompanero2.gameObject.SetActive(true);
        PersonajeSeleccionado = PlayerPrefs.GetInt("Modelos");
        ModoSeleccionado = 1;
    }
    public void SeleccionarModo2()
    {
        PanelModos.gameObject.SetActive(false);
        PanelInicial.gameObject.SetActive(true);
        BaseInicial.gameObject.SetActive(true);
        BaseCompanero1.gameObject.SetActive(true);
        BaseCompanero2.gameObject.SetActive(true);
        PersonajeSeleccionado = PlayerPrefs.GetInt("Modelos");
        ModoSeleccionado = 2;
    }

    public void SeleccionarPersonaje()
    {
        BaseInicial.gameObject.SetActive(true);
        BaseCompanero2.gameObject.SetActive(true);
        BaseCompanero1.gameObject.SetActive(true);
        PanelInicial.gameObject.SetActive(true);
        BotonSelecPersonajes.SetActive(false);
        textoMonedas.enabled = true;
        imagenMonedas.gameObject.SetActive(true);

        PlayerPrefs.SetInt("Modelos", PersonajeSeleccionado);
    }










}
