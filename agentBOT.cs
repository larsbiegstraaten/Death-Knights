using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agentBOT : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public float healthBOT;
    public float distance;
    public float BOSS1maxHealth = 600;

    public bool invulnerable;
    public float invulnerableTiempo;

    public enum Estados { perseguir, atacar}
    public Estados estadoBOT;

    public enum Enemigo { BOT1, BOSS1 }
    public Enemigo tipoEnemigo;

    public Animator animator;

    public GameObject particulaBOTS;
    public GameObject particulaImpactoE;
    public GameObject particulaImpactoQ;

    public int killsPlayer;

    public Oleadas oleadas;

    public GameObject areaDamage;
    public Controlador C;

    public AudioSource audiosource;
    public AudioClip[] clips;

    public GameObject partMuereBOSS1;
    public GameObject partGiroBoss;

    public bool atacnado = false;
    public bool isFirstAttack = true;

    public event Action OnBotDestroyed;
    public PedroPiquero pedroPiquero;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Jugador").transform;
        pedroPiquero = GameObject.Find("Jugador").GetComponent<PedroPiquero>();
        C = target.GetComponent<Controlador>();
        oleadas = GameObject.Find("Manager Oleada").GetComponent<Oleadas>();
        agent.isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = (target.position - gameObject.transform.position).magnitude;
        Vector3 directionToTarget = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        if (invulnerable)
        {
            invulnerableTiempo += Time.deltaTime;
            if (invulnerableTiempo >= 0.5f)
            {
                invulnerable = false;
                invulnerableTiempo = 0;
            }
        }

        if(estadoBOT == Estados.perseguir && healthBOT > 0)
        {
            if (tipoEnemigo == Enemigo.BOT1)
            {
                agent.SetDestination(target.position);
                animator.SetBool("BOTrun", true);
                if (distance <= 3)
                {
                    estadoBOT = Estados.atacar;
                }
            }
            if (tipoEnemigo == Enemigo.BOSS1)
            {

                if (distance > 5.3f)
                {
                    animator.SetInteger("BOSSattack", 0);
                }

                agent.SetDestination(target.position);
                animator.SetBool("BOTrun", true);
                if (distance <= 4.2f)
                {
                    estadoBOT = Estados.atacar;
                }
            }
        }

        if (estadoBOT == Estados.atacar)
        {
            animator.SetBool("BOTrun", false);
            if (tipoEnemigo == Enemigo.BOT1)
            {
                if (distance <= 2.5f && healthBOT > 0)
                {
                    agent.SetDestination(transform.position);
                    animator.SetTrigger("BOTattack");
                    if (!audiosource.isPlaying)
                    {
                        audiosource.PlayOneShot(clips[2]);
                    }
                }
                else
                {
                    estadoBOT = Estados.perseguir;
                }
            }
                
            if (distance <= 2.5f && healthBOT > 0)
            {
                if (tipoEnemigo == Enemigo.BOSS1 && atacnado == false)
                {
                    if (!audiosource.isPlaying)
                    {
                        audiosource.PlayOneShot(clips[0]);
                    }
                    if (isFirstAttack)
                    {
                        int x = UnityEngine.Random.Range(1, 4);
                        animator.SetInteger("BOSSattack", x);
                        isFirstAttack = false;
                    }
                    StartCoroutine(bossAttack());
                    agent.SetDestination(transform.position);
                 //   audiosource.PlayOneShot(clips[2]);
                }

            } else if(distance >= 2.6f)
            {
                estadoBOT = Estados.perseguir;
            }
            if (distance > 3 && tipoEnemigo == Enemigo.BOSS1)
            {
                animator.SetInteger("BOSSattack", 0);
                animator.SetBool("BOTrun", true);
                estadoBOT = Estados.perseguir;

            }
        }

        if(healthBOT <= 0)
        {
            if (tipoEnemigo == Enemigo.BOT1)
            {
                animator.SetBool("BOTrun", false);
                agent.SetDestination(agent.transform.position);
                animator.SetTrigger("BOTdeath");
                if (!audiosource.isPlaying)
                {
                    audiosource.PlayOneShot(clips[1]);  
                }
            }

            if(tipoEnemigo == Enemigo.BOSS1)
            {
                animator.SetBool("BOTrun", false);
                agent.SetDestination(agent.transform.position);
                animator.SetTrigger("BOTdeath");
                oleadas.canvasBoss1.SetActive(false);
            }
            
        }
    }
    void OnDestroy()
    {
        OnBotDestroyed?.Invoke();
    }
    public void killBOT()
    {
      //  audiosource.PlayOneShot(clips[1]);
        target.GetComponent<Controlador>().kills++;
        target.GetComponent<Controlador>().AddXP(25 * (target.GetComponent<Controlador>().multiEXP));
        oleadas.currentKills++;
        Destroy(agent.gameObject);
        
    }
    public void killBOSS1()
    {
        target.GetComponent<Controlador>().kills++;
        target.GetComponent<Controlador>().AddXP(120 * (target.GetComponent<Controlador>().multiEXP));
        oleadas.currentKills++;
        Destroy(agent.gameObject);

    }
    public void instanciarPartMuerteBoss()
    {
        Instantiate(partMuereBOSS1, new Vector3(agent.transform.position.x, agent.transform.position.y + 1.5f, agent.transform.position.z), Quaternion.Euler(-90, 0 ,0), agent.transform);
    }
    public IEnumerator bossAttack()
    {
        atacnado = true;
        yield return new WaitForSeconds(2f); 
        int attackType = UnityEngine.Random.Range(1, 4); 
        animator.SetInteger("BOSSattack", attackType);
        atacnado = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tipoEnemigo == Enemigo.BOT1)
        {
            if (other.gameObject.CompareTag("Weapon1"))
            {
                if (!invulnerable)
                {
                    invulnerable = true;
                    animator.SetTrigger("BOTgethit");
                    Instantiate(particulaBOTS, other.transform.position, Quaternion.identity);
                    if (!audiosource.isPlaying)
                    {
                        audiosource.PlayOneShot(clips[0]);  // MIRAR COMO MANTENER LAS REFERENCIAS DE LOS AUDIOCLIPS
                    }
                    
                    takeDamage(20);
                }
            }
            if (other.gameObject.CompareTag("WeaponQ"))
            {
                if (!invulnerable)
                {
                    invulnerable = true;
                    animator.SetTrigger("BOTgethit");
                    Instantiate(particulaImpactoQ, other.transform.position, Quaternion.identity);
                    Instantiate(areaDamage, other.transform.position, Quaternion.identity);
                    takeDamage(50);
                }
            }
            if (other.gameObject.CompareTag("WeaponE"))
            {
                if (!invulnerable)
                {
                    invulnerable = true;
                    animator.SetTrigger("BOTgethit");
                    Instantiate(particulaImpactoE, other.transform.position, Quaternion.identity);
                    Instantiate(areaDamage, other.transform.position, Quaternion.identity);
                    takeDamage(50);
                }
            }
        }
           
        if(tipoEnemigo == Enemigo.BOSS1)
        {
            if (other.gameObject.CompareTag("Weapon1"))
            {
                if (!invulnerable)
                {
                    invulnerable = true;
               
                    Instantiate(particulaBOTS, other.transform.position, Quaternion.identity);
                    // audiosource.PlayOneShot(clips[0]);
                    takeDamage(20);
                }
            }
            if (other.gameObject.CompareTag("WeaponQ"))
            {
                if (!invulnerable)
                {
                    invulnerable = true;
                    
                    Instantiate(particulaImpactoQ, other.transform.position, Quaternion.identity);
                    Instantiate(areaDamage, other.transform.position, Quaternion.identity);
                    takeDamage(50);
                }
            }
            if (other.gameObject.CompareTag("WeaponE"))
            {
                if (!invulnerable)
                {
                    invulnerable = true;
                    
                    Instantiate(particulaImpactoE, other.transform.position, Quaternion.identity);
                    Instantiate(areaDamage, other.transform.position, Quaternion.identity);
                    takeDamage(50);
                }
            }
            
        }
    }

    public void instanciarParticula()
    {
        Instantiate(partGiroBoss, transform.position, Quaternion.identity, transform);
    }

    public void takeDamage(float damage)
    {
        healthBOT -= damage * (pedroPiquero.multiDaño);
    }

}
