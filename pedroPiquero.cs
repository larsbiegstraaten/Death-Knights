using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedroPiquero : MonoBehaviour
{
    public GameObject particula;
    public GameObject Partfinal;
    public Controlador Cont; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    

private void OnTriggerEnter(Collider other)
    {

            Instantiate(particula, this.transform.position, Quaternion.identity);
                   
    }
    
    
}
