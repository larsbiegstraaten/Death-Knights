using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.ParticleSystem;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;

public class Controlador : MonoBehaviour
{
    public bool bTest;
    public Animator animator;
    public bool atacar;
    public Rigidbody Rbody;
    public float Speed = 1;
    public GameObject aimPoint;
    public Camera cam;
    public GameObject cam1;
    public float lookingAngle;
    public GameObject body;
    public GameObject Test;
    public GameObject puntoMira;
    public Vector3 V3;
    public bool atacando;
    public bool bLanzarParticle;
    public bool bloqueado = false;
    public bool usandoPersonaje;
    [Header("Stats")]
    public int ID;
    public float maxVida;
    public float currentVida;
    public float MovSpeed;
    public float ptsAtaque;
    public float ptsDefensa;
    public int tipoAtaque;
    public float currentMana;
    public float maxMana;
    [Header("Condiciones")]
    public bool Estuneado = false;
    public bool Muerto = false;
    public bool dashing = false;
    public bool haciendoAccion = false;
    [Header("Particulas")]
    public GameObject ParticleHit;
    public MeshRenderer[] ObjetosOcultos;
    public GameObject particulaDash;
    public ParticleSystem[] particulas;

    [Header("Interfaz")]
    public Image Cortinilla;
    public Image CortinillaRoja;
    public Image barraVida;
    public Image imagenBajoVida;
    public Image cortinillaDash;
    public Image barraMana;
    public TextMeshProUGUI cantidadMana;
    public Image cortinillaHab1;
    public Image cortinillaUlti;


    [Header("Zonas Mapa")]
    public GameObject zonaRespawn1;
    public GameObject zonaRespawn2;
    public GameObject zonaFantasma;
    [Header("Otros")]
    public GameObject _currentObject;
    public float DashForce = 20;
    public float hab1CoolDown;
    public float hab1MaxCooldown;
    public float ultiCoolDown;
    public float ultiMaxCoolDown;
    public float dashCoolDown;
    public float dashMaxCoolDown;
    private TransparentObjects _fader;
    [Header("Niveles (EXP)")]
    public int currentLevel = 1;
    public float currentXP = 0;
    public float xpForNextLevel = 100;
    public float xpScaleFactor = 1.1f;
    public float xpTotal;
    public Image barraEXP;
    public TextMeshProUGUI levelText;
    [Header("Multiplicadores")]
    public float multiDaño = 1;
    public float multiBlindaje = 1;
    public float multiMovimiento = 1;
    public float multiRecargaDash = 1;
    public float multiArma1Daño = 1;
    public float multiRecargaArma1 = 1;
    public float multiEXP = 1;
    public float multiRecargaUlti = 1;
    public float multiRecargaAtaqueEspecial = 1;
    public float multiDañoUlti = 1;
    public float multiDañoAtaqueEspecial = 1;

    public Color miColor;
    [Range(0f, 1f)]

    public Material[] materiales;
    public GameObject objetoDetectado;  // Almacena el objeto actualmente detectado por el raycast



    public PhotonView photonView;
    public alma alma;

    public controladorEscenas controladorEscenas;
    public scoresManaging scoresManaging;
    public upgrades_database upgrades;

    public GameObject player;


    public int kills;
    public int deaths;

    public int team;

    public TextMeshProUGUI contKills;
    public TextMeshProUGUI ToleadasFinal;
    public TextMeshProUGUI TkillsFinal;
    public TextMeshProUGUI textoVida;
    public TextMeshProUGUI TnombreFinal;
    public TextMeshProUGUI Tganancias;

    public float rotationSpeed;

    public GameObject Cortinilla2;

    public Oleadas oleadas;

    public enum TipoJuego { online, offline }
    public TipoJuego tipo;

    public AudioClip[] clips;
    public AudioSource audioSource;

    private string urlPostHighScore = "https://deathknights.000webhostapp.com/PHP/postHighScore.php";
    private string urlGetHighScores = "https://deathknights.000webhostapp.com/PHP/getHighScores.php";

    public List<string> HSnicknames = new List<string>();
    public List<int> HSkills = new List<int>();
    public List<int> HSoleadas = new List<int>();

    public highScoresDisplay highScoresDisplay;

    public string playerNickname;
    public int playerCoinsTotal;
    public bool muerto = false;
    public TextMeshProUGUI nombreUI;
    public string nombreUsuario;
    public GameObject partCargaUlti;
    public GameObject menuInGame;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
          
        if (tipo == TipoJuego.offline)
        {
            _currentObject = null;
            player = this.gameObject;
            oleadas = GameObject.Find("Manager Oleada").GetComponent<Oleadas>();
            CortinillaRoja.CrossFadeAlpha(0, 0.02f, true);
            materiales = GetComponent<MeshRenderer>().materials;
            playerCoinsTotal = PlayerPrefs.GetInt("PlayerCoins");
            //   alma = GameObject.FindGameObjectWithTag("Alma").GetComponent<alma>();
            UpdateLevelUI();
            UpdateXPUI();
        }

        if (tipo == TipoJuego.online)
        {
            photonView.RPC("Referencias", RpcTarget.All);
            _currentObject = null;
            player = this.gameObject;
            if (Cortinilla != null)
            {
                Debug.Log("entro al if de la cortinilla");
                Cortinilla.CrossFadeAlpha(0, 0.01f, false);
            }


            gameObject.name = "Personaje " + photonView.Controller.NickName;

            controladorEscenas = GameObject.Find("Mapa1").GetComponent<controladorEscenas>();
            alma = GameObject.Find("Alma " + photonView.Controller.NickName).GetComponent<alma>();
            int x = alma.team;
            photonView.RPC("Nombres", RpcTarget.All);
            photonView.RPC("ColorNicknames", RpcTarget.All, x);
            if (photonView.IsMine)
            {
                ID = alma.idBd;
                team = alma.team;
                photonView.RPC("asignTeam", RpcTarget.All, ID, team);
            }

        }
        if (PlayerPrefs.HasKey("PlayerNickName"))
        {
            playerNickname = PlayerPrefs.GetString("PlayerNickName");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (tipo == TipoJuego.offline)
        {
            contLocal();
            cam.gameObject.SetActive(true);
            cam1.gameObject.SetActive(true);
            UpdateXPUI();
            if (!bloqueado && usandoPersonaje)
            {
                // OJO CUIDAO: HAY QUE ACORDARSE DE QUITAR EL AUTO ATAQUE EN LA J
                movCamarayPersonaje();
                habilidadesYacciones();
                fadeObjects();
            }
        }
        if (tipo == TipoJuego.online)
        {
            if (photonView.IsMine)
            {
               
                cam.gameObject.SetActive(true);
                cam1.gameObject.SetActive(true);
                if (!bloqueado && usandoPersonaje)
                {
                    // OJO CUIDAO: HAY QUE ACORDARSE DE QUITAR EL AUTO ATAQUE EN LA J
                    movCamarayPersonaje();
                    habilidadesYacciones();
                    fadeObjects();
                }
               
            }
            
        }


    }
    
    
    public void contLocal()
    {
        barraVida.fillAmount = currentVida / maxVida;
        if (currentVida > maxVida)
        {
            currentVida = maxVida;
        }
        textoVida.SetText(((float)Math.Ceiling(currentVida)).ToString() + " / " + maxVida.ToString());
        contKills.SetText(kills.ToString());
        if (currentVida <= 0 && !Muerto)
        {
            StartCoroutine(muerteLocal());
            // mueres      
        }

    }
    public IEnumerator muerteLocal()
    {
        Muerto = true;
        TkillsFinal.SetText("Kills: " + kills);
        ToleadasFinal.SetText("Oleadas: " + oleadas.oleadaNum);
        TnombreFinal.SetText(playerNickname);
        Tganancias.SetText(" + " + calcularGanancia());
        playerCoinsTotal += calcularGanancia();
        PlayerPrefs.SetInt("PlayerCoins", playerCoinsTotal);
        PlayerPrefs.Save();
        animator.SetTrigger("muereLocal");
        yield return StartCoroutine(postHighScore());
        yield return StartCoroutine(getHighScore());
        Cortinilla2.SetActive(true);
        Time.timeScale = 0;
    }

    public int calcularGanancia()
    {
        int ganancia = Mathf.RoundToInt(xpTotal * kills) / 50;
        return ganancia;
    }
    public IEnumerator postHighScore()
    {
        UnityWebRequest r = new UnityWebRequest();
        WWWForm f = new WWWForm();
        f.AddField("nickname", playerNickname);
        f.AddField("oleadas", oleadas.oleadaNum);
        f.AddField("kills", kills);

        r = UnityWebRequest.Post(urlPostHighScore, f);
        yield return r.SendWebRequest();

        if (r.result == UnityWebRequest.Result.ConnectionError || r.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + r.error);
        }
        else
        {
            Debug.Log("Server response: " + r.downloadHandler.text);
        }
    }

    public IEnumerator getHighScore()
    {
        UnityWebRequest r = new UnityWebRequest();

        r = UnityWebRequest.Get(urlGetHighScores);
        yield return r.SendWebRequest();

        if (r.result == UnityWebRequest.Result.ConnectionError || r.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + r.error);
        }
        else
        {
            Debug.Log("Server response: " + r.downloadHandler.text);
        }
        string x = r.downloadHandler.text;
        ProcessHighScores(x);
    }

    void ProcessHighScores(string response)
    {
        HSnicknames.Clear();
        HSkills.Clear();
        HSoleadas.Clear();

        string[] lines = response.Split(new[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            // Extract nickname
            string nickname = line.Substring("Nickname: ".Length, line.IndexOf(" - Kills: ") - "Nickname: ".Length);
            // Extract kills
            string killsStr = line.Substring(line.IndexOf("Kills: ") + "Kills: ".Length, line.IndexOf(" - Oleadas: ") - (line.IndexOf("Kills: ") + "Kills: ".Length));
            // Extract oleadas
            string oleadasStr = line.Substring(line.IndexOf("Oleadas: ") + "Oleadas: ".Length);

            if (!string.IsNullOrEmpty(nickname) && !string.IsNullOrEmpty(killsStr) && !string.IsNullOrEmpty(oleadasStr))
            {
                HSnicknames.Add(nickname);
                HSkills.Add(int.Parse(killsStr)); // Assuming kills is always a valid integer
                HSoleadas.Add(int.Parse(oleadasStr)); // Assuming oleadas is also a valid integer
            }
        }
        highScoresDisplay.DisplayHighScores(HSnicknames, HSkills, HSoleadas);
    }
    public void reiniciarModoOleadas()
    {
        SceneManager.LoadScene(2);
    }

    public void salirAlMenu()
    {
        SceneManager.LoadScene(0);
    }
    public IEnumerator Dash()
    {
        bloqueado = true;
        dashing = true;
        animator.SetTrigger("dash");
        cortinillaDash.fillAmount = 1;
        Vector3 originalVelocity = Rbody.velocity;
        Vector3 dashDirection = animator.gameObject.transform.forward;
        if (Rbody.velocity.magnitude <= 0)
        {
            Rbody.velocity = dashDirection * DashForce;
        }
        else
        {
            Vector3 newVelocity = originalVelocity.normalized * DashForce;
            Rbody.velocity = newVelocity;
        }
        yield return new WaitForSeconds(0.4f);
        bloqueado = false;
        haciendoAccion = false;
        Rbody.velocity = originalVelocity;
    }
    [PunRPC]
    public void recibirdañorpc(int d, string tipo, bool bStun, int id)
    {
        RecibirDaño(d, tipo, bStun, id);
        Debug.Log("    1   +    " + id);

    }
    [PunRPC]
    public virtual void RecibirDaño(int d, string tipo, bool bStun, int id)
    {

        currentVida -= d;
        barraVida.fillAmount = currentVida / maxVida;
        if (currentVida <= 0 && muerto == false)
        {
            currentVida = 0;
            deaths++;
            muerto = true;
            if (photonView.IsMine) 
            {
                Debug.LogError("Entro al IsMineeeeeeeeeeeeeeeeeeeeeeee");
                controladorEscenas.IdAgresor(id);
            }
            photonView.RPC("MuerteAnim", RpcTarget.All);
        }
    }
    [PunRPC]
    public void SumarKills()
    {
        kills++;
        Debug.LogError("Kills: "+ kills);
        controladorEscenas.CalcularTotalKills();
    }
    public void Impactar()
    {


    }
    public virtual void AtaqueSimple()
    {
        animator.SetTrigger("ataque1");
    }
    public virtual void AtaqueEspecial()
    {
        animator.SetTrigger("ataque2");
    }
    public virtual void AtaqueUlti()
    {
        
        photonView.RPC("particulaCargaUlti", RpcTarget.All);
    }
    public void muertePersonaje()
    {
        StartCoroutine(MuerteYRespawn());
    }
    [PunRPC]
    public IEnumerator MuerteYRespawn()
    {
        if (photonView.IsMine && usandoPersonaje)
        {
            
            
                bloqueado = true;
                Cortinilla.CrossFadeAlpha(1, 1, false);
                yield return new WaitForSeconds(0.5f);
                gameObject.transform.position = zonaFantasma.transform.position;
                yield return new WaitForSeconds(4);
                // respawnear en zona spawn
                // como hacer tema de equipos y que se distingan
                if (alma.team == 1)
                {
                    gameObject.transform.position = zonaRespawn1.transform.position;
                }
                else if (alma.team == 2)
                {
                    gameObject.transform.position = zonaRespawn2.transform.position;
                }
                photonView.RPC("restaurarvida", RpcTarget.All);
                Cortinilla.CrossFadeAlpha(0, 1, false);
                yield return new WaitForSeconds(1);
                bloqueado = false;
                muerto = false;
                haciendoAccion = false;
                
            
        }


    }
    
    [PunRPC]
    public void restaurarvida()
    {
        currentVida = maxVida;
        barraVida.fillAmount = currentVida / maxVida;
    }
    public void restart()
    {
        SceneManager.LoadScene(0);
    }

    public void restartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    [PunRPC]
    public void rotacion(Vector3 x)
    {
        body.transform.rotation = Quaternion.Slerp(body.transform.rotation, Quaternion.LookRotation(x), Time.deltaTime * rotationSpeed);
    }
    public void movCamarayPersonaje()
    {
        V3 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        Vector3 V4 = V3 - cam.transform.position;
        Debug.DrawRay(cam.transform.position, V4 * 1000, Color.red, 0);
        RaycastHit hit;
        LayerMask l = LayerMask.GetMask("Floor");
        Vector3 Vcam = cam.transform.position - transform.position;
        RaycastHit[] r = Physics.RaycastAll(gameObject.transform.position, Vcam, Vcam.magnitude);
        // ya no va esto, hace algo raro, mirar con dani
        if (Physics.Raycast(cam.transform.position, V4, out hit, 1000, l))
        {
            Vector3 v2 = (hit.point - body.transform.position);

            if (v2.magnitude > 1.5f)
            {
                Test.transform.position = hit.point;
            }

        }
        puntoMira.transform.position = new Vector3(Test.transform.position.x, Test.transform.position.y + 1.6f, Test.transform.position.z);
        Vector3 V5 = puntoMira.transform.position - transform.position;
        Vector3 direction = Vector3.RotateTowards(transform.forward, V5, 1000 * Time.deltaTime, 0.0f);
        if(tipo == TipoJuego.online)
        {
            photonView.RPC("rotacion", RpcTarget.All, direction);
        }
        if(tipo == TipoJuego.offline)
        {
            body.transform.rotation = Quaternion.Slerp(body.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        }
    
        

        // Parte del movimiento de personaje

        if (Input.GetKey(KeyCode.W))
        {
            Rbody.velocity = direction * Speed * multiMovimiento;
            animator.SetBool("corre", true);
        }
        else
        {
            animator.SetBool("corre", false);
            Rbody.velocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 oppDirection = new Vector3(direction.x * -1, direction.y * -1, direction.z * -1);
            Rbody.velocity = oppDirection * Speed * multiMovimiento;
            animator.SetBool("andarAtras", true);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 leftDirection = Quaternion.Euler(0, -90, 0) * direction;
            Rbody.velocity = leftDirection * Speed * multiMovimiento;
            animator.SetBool("andarAtras", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Vector3 rightDirection = Quaternion.Euler(0, 90, 0) * direction;
            Rbody.velocity = rightDirection * Speed * multiMovimiento;
            animator.SetBool("andarAtras", true);
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("andarAtras", false);
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Rbody.velocity = Vector3.zero;
        }
    }
    public void habilidadesYacciones()
    {
        if(tipo == TipoJuego.offline)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                abrirMenuIngame();
            }
        }
        if (!haciendoAccion)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !dashing)
            {
                StartCoroutine(Dash());
                dashCoolDown = dashMaxCoolDown;
                haciendoAccion = true;
                //Rbody.AddForce(animator.gameObject.transform.forward * 10000);
            }
            if (Input.GetButtonDown("Fire1"))
            {
                haciendoAccion = true;
                if (tipo == TipoJuego.offline)
                {
                    audioSource.clip = clips[0];
                    audioSource.PlayDelayed(0.2f);
                }   
                AtaqueSimple();
            }
            if (Input.GetKeyDown(KeyCode.Q) && hab1CoolDown <= 0)
            {
                hab1CoolDown = hab1MaxCooldown;
                haciendoAccion = true;
                AtaqueEspecial();
            }


            if (Input.GetKeyDown(KeyCode.E) && ultiCoolDown <= 0)
            {
                ultiCoolDown = ultiMaxCoolDown;
                haciendoAccion = true;
                if (tipo == TipoJuego.offline)
                {
                    audioSource.clip = clips[1];
                    audioSource.Play();
                }
                AtaqueUlti();
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                recibirdañorpc(95, "guay", false, photonView.ViewID);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                restart();
            }
        }
        if (hab1CoolDown > 0)
        {
            hab1CoolDown -= Time.deltaTime * multiRecargaAtaqueEspecial;
            cortinillaHab1.fillAmount = hab1CoolDown / hab1MaxCooldown;
            if (hab1CoolDown <= 0)
            {
                hab1CoolDown = 0;
            }
        }
        if (ultiCoolDown > 0)
        {
            ultiCoolDown -= Time.deltaTime * multiRecargaUlti;
            cortinillaUlti.fillAmount = ultiCoolDown / ultiMaxCoolDown;
            if (ultiCoolDown <= 0)
            {
                ultiCoolDown = 0;
            }
        }
        if (dashCoolDown > 0)
        {
            dashCoolDown -= Time.deltaTime * multiRecargaDash;
            cortinillaDash.fillAmount = dashCoolDown / dashMaxCoolDown;
            if (dashCoolDown <= 0)
            {
                dashCoolDown = 0;
                dashing = false;
            }
        }
    }
    public void fadeObjects()
    {
        if (player != null)
        {
            Ray ray = new Ray(cam.transform.position, player.transform.position - cam.transform.position);
            Debug.DrawRay(cam.transform.position, player.transform.position - cam.transform.position, Color.blue);
            RaycastHit trans;
            if (Physics.Raycast(ray, out trans))
            {
                if (trans.collider != null)
                {
                    if (trans.collider.gameObject == player)
                    {
                        // No hay nada entre la cámara y el jugador
                        if (_fader != null)
                        {
                            _fader.doFade = false;
                        }
                    }
                    else
                    {
                        if (_currentObject != null && _currentObject != trans.collider.gameObject)
                        {
                            // Restaurar la transparencia del objeto anterior si cambia
                            TransparentObjects previousFader = _currentObject.GetComponent<TransparentObjects>();
                            if (previousFader != null)
                            {
                                previousFader.doFade = false;
                            }
                        }

                        _fader = trans.collider.gameObject.GetComponent<TransparentObjects>();
                        if (_fader != null)
                        {
                            _fader.doFade = true;
                            _currentObject = trans.collider.gameObject; // Actualizamos el objeto actual
                        }
                    }
                }
            }

        }
    }
    public void AddXP(float xpToAdd)
    {
        currentXP += xpToAdd;

        if (currentXP >= xpForNextLevel)
        {
            xpTotal += currentXP;
            currentXP -= xpForNextLevel;
            upgrades.lanzarLevelUp();
            currentLevel++;
            xpForNextLevel = Mathf.RoundToInt(xpForNextLevel * xpScaleFactor);
            UpdateLevelUI();

        }


    }
    void UpdateLevelUI()
    {
        levelText.text = "Nivel " + currentLevel.ToString();
        
    }

    void UpdateXPUI()
    {
        barraEXP.fillAmount = currentXP / xpForNextLevel;
    }
    [PunRPC]
    public void particulaCargaUlti()
    {
        animator.SetTrigger("ataque3");
        Instantiate(partCargaUlti, body.transform.position, Quaternion.identity, body.transform);
        
    }
    [PunRPC]
    public void asignTeam(int id, int Team)
    {
        ID = id;
        team = Team;

    }
    [PunRPC]
    public IEnumerator Referencias()
    {

        yield return new WaitForSeconds(0.5f);

        zonaRespawn1 = GameObject.FindGameObjectWithTag("zonaRespawn1");
        zonaRespawn2 = GameObject.FindGameObjectWithTag("zonaRespawn2");
        zonaFantasma = GameObject.Find("zonaFantasma");
        materiales = GetComponent<MeshRenderer>().materials;
    }
    [PunRPC]
    public IEnumerator Nombres()
    {
        yield return new WaitForSeconds(1);
        nombreUsuario = photonView.Controller.NickName.Split("*")[0];
        nombreUI.SetText(nombreUsuario);
    }
    [PunRPC]
    public void MuerteAnim()
    {
        animator.SetTrigger("morir");
    }
    [PunRPC]
    public void ColorNicknames(int t)
    {
        if (t == 1)
        {
            nombreUI.color = Color.blue;
        }
        else if (t == 2)
        {
            nombreUI.color = Color.red;
        }
    }

    public void abrirMenuIngame()
    {
        menuInGame.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void cerrarMenuIngame()
    {
        menuInGame.SetActive(false);
        Time.timeScale = 1;
    }

}
