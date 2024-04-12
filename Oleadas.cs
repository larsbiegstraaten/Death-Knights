using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Oleadas : MonoBehaviour
{
    public GameObject BOT1;
    public GameObject BOT2;
    public Transform[] spawnPoints;
    public int oleadaNum = 1;
    public int killsRequiredForNextWave;
    public int currentKills = 0;
    public int killsRequired = 10;
    public int botsOleada = 15;
    public int maxBots = 20;
    public int activeBots;
    public TextMeshProUGUI textoFinOleada;
    public TextMeshProUGUI textoKillsOleada;
    public TextMeshProUGUI textOleadaActual;
    public Image infoArmaNueva;
    public GameObject iconoArma1;
    public GameObject BossSpwan;
    public GameObject Boss1Prefab;
    public Boss1 Boss1;
    public Controlador C;
    public Image barraVidaBoss1;
    public GameObject canvasBoss1;

    public bool arma1Unlock = false;
    public bool oleadaBOSS1 = false;
    // Start is called before the first frame update
    void Start()
    {
        textOleadaActual.SetText("Oleada " + oleadaNum);
        killsRequiredForNextWave = killsRequired;
        StartWave();
         
    }

    // Update is called once per frame
    void Update()
    {
        textoKillsOleada.SetText(currentKills + " / " + killsRequiredForNextWave);
        if (currentKills >= killsRequiredForNextWave)
        {
            currentKills = 0; 
            oleadaNum++;
            killsRequiredForNextWave += killsRequired / 2;
            botsOleada += 7;
            StartCoroutine(finOleada());

        }
        if (oleadaBOSS1)
        {
            barraVidaBoss1.fillAmount = Boss1.currentHP / Boss1.maxHP;
        }
    }

    void StartWave()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {

        for (int i = 0; i < botsOleada; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnEnemy()
    {
 
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        if (oleadaNum <= 5)
        {   
         Instantiate(BOT1, spawnPoint.position, spawnPoint.rotation);
        }
        if (oleadaNum >= 6 && oleadaNum <= 12)
        {
          Instantiate(BOT2, spawnPoint.position, spawnPoint.rotation);
        }
        else if(oleadaNum >= 13)
        {  // aquí podemos añadir más variantes de enemigos
            Instantiate(BOT2, spawnPoint.position, spawnPoint.rotation);
        }

    }

    public IEnumerator finOleada()
    {
        switch (oleadaNum)
        {
            default:
                textoFinOleada.SetText("Has derrotado la oleada " + (oleadaNum - 1));
                textoFinOleada.gameObject.SetActive(true);
                yield return new WaitForSecondsRealtime(1.5f);
                textoFinOleada.gameObject.SetActive(false);
                StartWave();
                textOleadaActual.SetText("Oleada " + oleadaNum);
                break;
            case 1:
                textoFinOleada.SetText("Has derrotado la oleada " + (oleadaNum - 1));
                textoFinOleada.gameObject.SetActive(true);
                yield return new WaitForSecondsRealtime(1.5f);
                textoFinOleada.gameObject.SetActive(false);
                StartWave();
                textOleadaActual.SetText("Oleada " + oleadaNum);
                break;
            case 2: 
                textoFinOleada.SetText("Has derrotado la oleada " + (oleadaNum - 1));
                textoFinOleada.gameObject.SetActive(true);
                yield return new WaitForSecondsRealtime(1f);
                Time.timeScale = 0;
                infoArmaNueva.gameObject.SetActive(true);
                yield return new WaitForSecondsRealtime(1f);
                textoFinOleada.gameObject.SetActive(false);
                yield return new WaitForSecondsRealtime(2f);
                Time.timeScale = 1;
                arma1Unlock = true;
                infoArmaNueva.gameObject.SetActive(false);
                iconoArma1.gameObject.SetActive(true);
                StartWave();
                textOleadaActual.SetText("Oleada " + oleadaNum);
                break;
            case 3:
                textoFinOleada.SetText("Has derrotado la oleada " + (oleadaNum - 1));
                textoFinOleada.gameObject.SetActive(true);
                yield return new WaitForSecondsRealtime(1.5f);
                textoFinOleada.gameObject.SetActive(false);
                StartWave();
                textOleadaActual.SetText("Oleada " + oleadaNum);
                break;
            case 6:
                textoFinOleada.SetText("Has derrotado la oleada " + (oleadaNum - 1));
                textoFinOleada.gameObject.SetActive(true);
                Instantiate(Boss1Prefab, BossSpwan.transform.position, Quaternion.identity);
                Boss1 = GameObject.FindGameObjectWithTag("BOSS1").GetComponent<Boss1>();
                oleadaBOSS1 = true;
                yield return new WaitForSecondsRealtime(1.5f);
                textoFinOleada.gameObject.SetActive(false);      
                StartWave();
                yield return new WaitForSecondsRealtime(3f);
                canvasBoss1.SetActive(true);
                textOleadaActual.SetText("Oleada " + oleadaNum);
                break;
            case 7:
                maxBots = 30;
                textoFinOleada.SetText("Has derrotado la oleada " + (oleadaNum - 1));
                textoFinOleada.gameObject.SetActive(true);
                oleadaBOSS1 = false;
                yield return new WaitForSecondsRealtime(1.5f);
                textoFinOleada.gameObject.SetActive(false);
                StartWave();
                textOleadaActual.SetText("Oleada " + oleadaNum);
                break;



        }
        
    }
}
