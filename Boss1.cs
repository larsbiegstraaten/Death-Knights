using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public agentBOT agentBOT;
    public float currentHP;
    public float maxHP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHP = agentBOT.healthBOT;
        maxHP = agentBOT.BOSS1maxHealth;
    }
}
