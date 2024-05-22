using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEnemy : MonoBehaviour
{
    public GameObject particula;
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
        if(other.tag == "Weapon")
        {
            Instantiate(particula, other.transform.position, Quaternion.identity);
        }
    }

}
