using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healsManager : MonoBehaviour
{
    public float radioRecoleccion = 3;
    public Controlador controlador;
    public GameObject player;
    public GameObject healPotionPrefab;
    public healSpawnManager manager;
    public PedroPiquero pedroPiquero;
    // Start is called before the first frame update
    void Start()
    {
        pedroPiquero = GameObject.Find("Jugador").GetComponent<PedroPiquero>();
        controlador = GameObject.FindGameObjectWithTag("Player").GetComponent<Controlador>();
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("healManager").GetComponent<healSpawnManager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        pickHeals();
    }

    public void pickHeals()
    {
            float d = Vector3.Distance(transform.position, player.transform.position);
            if (d <= radioRecoleccion && (pedroPiquero.currentVida < pedroPiquero.maxVida))
            {
            pedroPiquero.currentVida += 25;
            manager.RespawnHeal(transform.parent);
            Destroy(gameObject);
            } 
    }
}
