using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mindGun : MonoBehaviour
{
    public float range; 
    public int damage; 
    public float cooldown; 
    public bool readyToFire;

    public GameObject bocacha;

    public GameObject trail;

    public GameObject impactoRayo;
    public GameObject salidaRayo;

    public Oleadas oleadas;

    public Controlador controlador;


    private void Start()
    {
        oleadas = GameObject.Find("Manager Oleada").GetComponent<Oleadas>();
        controlador = GetComponent<Controlador>();
        readyToFire = true;      
    }
    // Update is called once per frame
    void Update()
    {
        
         if (readyToFire && oleadas.arma1Unlock == true)
        {
            Shoot();
            StartCoroutine(startCooldown());
        }
    }

    void Shoot()
    {
        Debug.Log("Disparo");
        GameObject closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, closestEnemy.transform.position - transform.position, out hit, range))
            {

                if (hit.collider != null)
                {
                    closestEnemy.GetComponent<agentBOT>().takeDamage(damage * controlador.multiArma1Daño);
                    Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
                    //instanciar salida del rayo
                    Instantiate(salidaRayo, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), impactRotation, transform);
                    Instantiate(impactoRayo, hit.transform.position, Quaternion.identity, hit.transform);
                }
                else
                {
                    var endPosition = bocacha.transform.position + transform.up * range;

                }

            }
        }
    }
    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("BOTS");
        GameObject closest = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject enemy in enemies)
        {
            Vector3 directionToTarget = enemy.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closest = enemy;
            }
        }
        return closest;
    }

    public IEnumerator startCooldown()
    {
        readyToFire = false;
        yield return new WaitForSeconds(cooldown * controlador.multiRecargaArma1);
        readyToFire = true;
    }
}
