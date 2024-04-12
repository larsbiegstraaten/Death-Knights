using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ataqueBoss1 : MonoBehaviour
{
    public bool invulnerable = false;

    public GameObject particulaImpaco;
    public PedroPiquero pedroPiquero;

    private void Start()
    {
        pedroPiquero = GameObject.Find("Jugador").GetComponent<PedroPiquero>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !invulnerable)
        {
            
            other.GetComponent<Controlador>().currentVida -= 65;
            invulnerable = true;
            Instantiate(particulaImpaco, other.transform.position, Quaternion.identity);
            StartCoroutine(resetInvulnerabilidad());
        }
    }
    IEnumerator resetInvulnerabilidad()
    {
        yield return new WaitForSeconds(0.2f);
        pedroPiquero.CortinillaRoja.CrossFadeAlpha(1, 0.3f, true);
        yield return new WaitForSeconds(0.3f);
        pedroPiquero.CortinillaRoja.CrossFadeAlpha(0, 0.3f, true);
        yield return new WaitForSeconds(0.7f);
        invulnerable = false;
    }
}
