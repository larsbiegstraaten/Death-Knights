using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class upgrades_database : MonoBehaviour
{
    public string url = "https://deathknights.000webhostapp.com/PHP/get_upgrades.php";
    public Oleadas oleadas;
    public PedroPiquero pedroPiquero;
    int upgrade_tier;

    public GameObject canvasSubirNivel;

    public Button upgradeButton1;
    public Button upgradeButton2;
    public Button upgradeButton3;

    public List<string> upgradeTypes = new List<string>();
    public List<int> upgradeAmounts = new List<int>();
    public List<int> upgradeIDs = new List<int>();
    public List<int> selectedIndexes = new List<int>();

    public TextMeshProUGUI nombre1;
    public TextMeshProUGUI valor1;
    public TextMeshProUGUI nombre2;
    public TextMeshProUGUI valor2;
    public TextMeshProUGUI nombre3;
    public TextMeshProUGUI valor3;
    public int indice1;
    public int indice2;
    public int indice3;

    public int index;
    // Start is called before the first frame update
    void Start()
    {
        oleadas = gameObject.GetComponent<Oleadas>();
        pedroPiquero = GameObject.Find("Jugador").GetComponent<PedroPiquero>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator getUpgrades()
    {
        
        if(oleadas.oleadaNum <= 5)
        {
            upgrade_tier = 1;
        }
        if (oleadas.oleadaNum >= 6 && oleadas.oleadaNum < 13)
        {
            upgrade_tier = 2;
        }
        if (oleadas.oleadaNum >= 14)
        {
            upgrade_tier = 3;
        }
        UnityWebRequest r = new UnityWebRequest();
        WWWForm f = new WWWForm();
        f.AddField("upgrade_tier" , upgrade_tier);
        r = UnityWebRequest.Post(url, f);
        yield return r.SendWebRequest();

        if (r.result == UnityWebRequest.Result.ConnectionError || r.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + r.error);
        }
        else
        {
            Debug.Log("Server response: " + r.downloadHandler.text);
            string response = r.downloadHandler.text;
            parseResponse(response);
        }

    }

    public IEnumerator levelUp()
    {
        yield return StartCoroutine(getUpgrades());       
        while (selectedIndexes.Count < 3)
        {
            index = UnityEngine.Random.Range(0, upgradeTypes.Count);
            if (!selectedIndexes.Contains(index))
            {
                selectedIndexes.Add(index);
            }
        }
        Debug.Log(selectedIndexes[0].ToString() + " " + selectedIndexes[1].ToString() + " " + selectedIndexes[2].ToString());
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < selectedIndexes.Count; i++)
        {
            if (i == 0)
            {
                nombre1.SetText(upgradeTypes[selectedIndexes[i]]);
                valor1.SetText("+ " + upgradeAmounts[selectedIndexes[i]].ToString() + " %");
                indice1 = upgradeIDs[selectedIndexes[i]];
                Upgrade upgrade = new Upgrade(indice1, upgradeTypes[selectedIndexes[i]], upgradeAmounts[selectedIndexes[i]]);  
                if(upgrade.id == 10 || upgrade.id == 15 || upgrade.id == 16)
                {
                    valor1.SetText("+ " + upgradeAmounts[selectedIndexes[i]].ToString());
                }
                setButton1Action(upgrade);
            }
            if (i == 1)
            {
                nombre2.SetText(upgradeTypes[selectedIndexes[i]]);
                valor2.SetText("+ " + upgradeAmounts[selectedIndexes[i]].ToString() + " %");
                indice2 = upgradeIDs[selectedIndexes[i]];
                Upgrade upgrade1 = new Upgrade(indice2, upgradeTypes[selectedIndexes[i]], upgradeAmounts[selectedIndexes[i]]);
                if (upgrade1.id == 10 || upgrade1.id == 15 || upgrade1.id == 16)
                {
                    valor2.SetText("+ " + upgradeAmounts[selectedIndexes[i]].ToString());
                }
                setButton2Action(upgrade1);
        }
            if (i == 2)
            {
                nombre3.SetText(upgradeTypes[selectedIndexes[i]]);
                valor3.SetText("+ " + upgradeAmounts[selectedIndexes[i]].ToString() + " %");
                indice3 = upgradeIDs[selectedIndexes[i]];
                Upgrade upgrade2 = new Upgrade(indice2, upgradeTypes[selectedIndexes[i]], upgradeAmounts[selectedIndexes[i]]);
                if (upgrade2.id == 10 || upgrade2.id == 15 || upgrade2.id == 16)
                {
                    valor3.SetText("+ " + upgradeAmounts[selectedIndexes[i]].ToString());
                }
                setButton3Action(upgrade2);
            }
        }
        canvasSubirNivel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void lanzarLevelUp()
    {
        StartCoroutine(levelUp());
    }

    public void parseResponse(string g)
    {
        string[] lines = g.Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            string[] parts = line.Split(new string[] { ", Upgrade Type: ", ", Upgrade Amount: " }, StringSplitOptions.None);

            if (parts.Length == 3)
            {
                int upgradeId = int.Parse(parts[0].Replace("Upgrade ID: ", "").Trim());
                string upgradeType = parts[1].Trim();
                int upgradeAmount = int.Parse(parts[2].Trim());

                upgradeIDs.Add(upgradeId);
                upgradeTypes.Add(upgradeType);
                upgradeAmounts.Add(upgradeAmount);
            }
        }
    }

    public void setButton1Action(Upgrade upgrade)
    {
        
        upgradeButton1.onClick.RemoveAllListeners();

        upgradeButton1.onClick.AddListener(delegate { ExecuteUpgradeAction(upgrade); });

    }
    public void setButton2Action(Upgrade upgrade)
    {

        upgradeButton2.onClick.RemoveAllListeners();

        upgradeButton2.onClick.AddListener(delegate { ExecuteUpgradeAction(upgrade); });

    }
    public void setButton3Action(Upgrade upgrade)
    {

        upgradeButton3.onClick.RemoveAllListeners();

        upgradeButton3.onClick.AddListener(delegate { ExecuteUpgradeAction(upgrade); });

    }

    void ExecuteUpgradeAction(Upgrade upgrade)
    {
        if (upgrade != null)
        {
            Debug.Log("Tipo: " + upgrade.type + "Amount: " +  upgrade.amount);
            if (upgrade.id == 5)
            {
                pedroPiquero.multiDaño += ((float)upgrade.amount / 100) ;
            }
            if (upgrade.id == 3)
            {
                pedroPiquero.multiMovimiento += ((float)upgrade.amount / 100);
            }
            if (upgrade.id == 4)
            {
                pedroPiquero.multiBlindaje -= ((float)upgrade.amount / 100);
            }
            if (upgrade.id == 6)
            {
                pedroPiquero.multiRecargaDash -= ((float)upgrade.amount / 100);
            }
            if (upgrade.id == 7)
            {
                pedroPiquero.multiEXP += ((float)upgrade.amount / 100);
            }
            if (upgrade.id == 8)
            {
                pedroPiquero.multiDañoUlti += ((float)upgrade.amount / 100);
            }
            if (upgrade.id == 9)
            {
                pedroPiquero.multiDañoAtaqueEspecial += ((float)upgrade.amount / 100);
            }
           if (upgrade.id == 10)
            {
                pedroPiquero.maxVida += upgrade.amount;
                pedroPiquero.currentVida += upgrade.amount;
            }
           if (upgrade.id == 11)
            {
                pedroPiquero.multiArma1Daño += ((float)upgrade.amount / 100);
            }
            if (upgrade.id == 12)
            {
                pedroPiquero.multiRecargaArma1 += ((float)upgrade.amount / 100);
            }
            if (upgrade.id == 13)
            {
                pedroPiquero.multiRecargaUlti += ((float)upgrade.amount / 100);
            }
            if (upgrade.id == 14)
            {
                pedroPiquero.multiRecargaAtaqueEspecial += ((float)upgrade.amount / 100);
            }
            if (upgrade.id == 15)
            {
                pedroPiquero.maxVida += upgrade.amount;
                pedroPiquero.currentVida += upgrade.amount;
            }
            if (upgrade.id == 16)
            {
                pedroPiquero.maxVida += upgrade.amount;
                pedroPiquero.currentVida += upgrade.amount;
            }
            if (upgrade.id == 17)
            {
                pedroPiquero.multiMovimiento += ((float)upgrade.amount / 100);
            }
            if (upgrade.id == 18)
            {
                pedroPiquero.multiMovimiento += ((float)upgrade.amount / 100);
            }
            if (upgrade.id == 19)
            {
                pedroPiquero.multiBlindaje += ((float)upgrade.amount / 100);
            }
            if (upgrade.id == 20)
            {
                pedroPiquero.multiBlindaje += ((float)upgrade.amount / 100);
            }
            Time.timeScale = 1;
            upgradeIDs.Clear();
            upgradeAmounts.Clear();
            upgradeTypes.Clear();
            selectedIndexes.Clear();
            canvasSubirNivel.gameObject.SetActive(false);
        }
    }

    public class Upgrade
    {
        public int id { get; set; }
        public string type { get; set; }
        public int amount { get; set; }
        public Upgrade(int ID, string Type, int Amount) 
        {
            id = ID;
            type = Type;
            amount = Amount;
        }
    }

}

