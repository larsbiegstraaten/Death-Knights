using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOTimpactos : MonoBehaviour
{
    public bool invulnerable = false;

    public Controlador controlador;
    public PedroPiquero pedroPiquero;
    // Start is called before the first frame update
    void Start()
    {
        controlador = GameObject.FindGameObjectWithTag("Player").GetComponent<Controlador>();
        pedroPiquero = GameObject.Find("Jugador").GetComponent<PedroPiquero>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !invulnerable)
        {
            pedroPiquero.currentVida -= 20 * (pedroPiquero.multiBlindaje);
            invulnerable = true;
            StartCoroutine(resetInvulnerabilidad());

        }
    }
    
    IEnumerator resetInvulnerabilidad()
    {
        pedroPiquero.CortinillaRoja.CrossFadeAlpha(1, 0.4f, true);
        yield return new WaitForSeconds(0.5f);
        pedroPiquero.CortinillaRoja.CrossFadeAlpha(0, 0.4f, true);
        yield return new WaitForSeconds(0.7f);
        invulnerable = false;
    }
}
