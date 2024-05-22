using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    public float areaDamage;
    public void Start()
    {
        StartCoroutine(destroy());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BOTS") || other.CompareTag("BOSS1")) 
        {
            if (!other.GetComponent<agentBOT>().invulnerable)
            {
                other.gameObject.GetComponent<agentBOT>().healthBOT -= areaDamage;
            }
            
        }
    }

    public IEnumerator destroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
