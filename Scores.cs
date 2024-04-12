using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scores : MonoBehaviour
{
    public TextMeshProUGUI team1;
    public TextMeshProUGUI team2;
    public controladorEscenas CE;
    // Start is called before the first frame update
    void Start()
    {
        CE = GameObject.FindGameObjectWithTag("mapa1").GetComponent<controladorEscenas>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoresRPC();

    }

    public void ScoresRPC()
    {
        team1.text = CE.KillsT1.ToString();
        team2.text = CE.KillsT2.ToString();
    }
}
