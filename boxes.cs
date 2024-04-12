using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxes : MonoBehaviour
{
    public Animator animator;
    public GameObject boxDentro;
    public Controlador controlador;
    public PedroPiquero pedroPiquero;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        pedroPiquero = GameObject.Find("Jugador").GetComponent<PedroPiquero>();
        controlador = GameObject.Find("Jugador").GetComponent<Controlador>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            animator.SetTrigger("rompeCaja");
            audioSource.Play();
            pedroPiquero.AddXP(15 * pedroPiquero.multiEXP);
            Destroy(boxDentro);
            StartCoroutine(delayedDestroy());
            
        }
    }

    public IEnumerator delayedDestroy()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
