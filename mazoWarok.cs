using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazoWarok : MonoBehaviour
{
    public GameObject glassMazo;
    public GameObject lugarParticula;
    public GameObject particula1;
    public GameObject mano;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(particula1, lugarParticula.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
