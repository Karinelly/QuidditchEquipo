using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(SteeringBlender_Merodeadores))]



public class Golpeador_Merodeadores : MonoBehaviour
{
    private FSM_Merodeadores fms_Merodeadores;
    private Animator animatior_clase;

    private bool InterceptarBludger = false;
    private bool isQuaffleEnemy = false;
    public bool TocandoBludger = false;

    public float ThrowStrength;
    public float distanceToShoot;

    public LayerMask capaTeamMates;
    public float radio = 10f;

    public List<Transform> MyTeam;
    public List<Transform> MyRivals;
    public Transform bludgerTarget = null;

    float masCercana = Mathf.Infinity;

    void Start()
    {
        fms_Merodeadores = new FSM_Merodeadores(this);
       
        Invoke("FillLateData", 1.5f);
    }

    void FillLateData()
    {
        MyTeam = Team_Merodeadores.instancia.MyTeam;
        MyRivals = Team_Merodeadores.instancia.MyRivals;

    }

    // Update is called once per frame
    void Update()
    {

        //fms_Merodeadores.UpdateFSM();

       
    }
    private void FixedUpdate()
    {
        //creamos un detector esferico 
        Collider[] agentesCercanos = Physics.OverlapSphere(transform.position, radio, capaTeamMates);
    }

    public Transform DetectedBludger()
    {
        bludgerTarget = null;


        foreach (GameObject bludger in GameManager.instancia.Bludger)
        {
            if (bludger.GetComponent<Bludger>().GetTarget() == null)
            {
                InterceptarBludger = false;

                return null;
            }


            for (int j = 0; j < 6; j++)
            {

                if (bludger.GetComponent<Bludger>().GetTarget().gameObject == MyTeam[j].gameObject)
                {
                    bludgerTarget = bludger.transform;
                    InterceptarBludger = true;
                    return bludgerTarget;
                }
            }
        }
        InterceptarBludger = false;

        return bludgerTarget;
    }
    
    public bool isTeamMateTarget()
    {
        DetectedBludger();
        return InterceptarBludger;
    }

    public Transform EnemyQuaffle()
    {
        isQuaffleEnemy = false;
        foreach (Transform enemy in Team_Merodeadores.instancia.MyRivals)
        {
            if (enemy.gameObject == GameManager.instancia.QuaffleControllingPlayer)
            {
                
                isQuaffleEnemy = true;
                return enemy;
            }
        }

        Debug.Log(isQuaffleEnemy);
        return null;
    }

    public bool isEnemyQuaffle()
    {
        EnemyQuaffle();;
        Debug.Log(isQuaffleEnemy);
        return isQuaffleEnemy;
    }
    
    

    public Transform BludgerMasCercana()
    {
        bludgerTarget = null;
        TocandoBludger = false;

        foreach (GameObject bludger in GameManager.instancia.Bludger)
        {
            float dist = (bludger.transform.position - transform.position).magnitude;

            if (dist < masCercana)
            {
                masCercana = dist;
                bludgerTarget = bludger.transform;
                TocandoBludger = true;

            }

        }

        return bludgerTarget;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si es un jugador lo agrego a mi lista 
        if (collision.gameObject.CompareTag("Ball Bludger"))
        {
            if(isEnemyQuaffle())
            {
                collision.gameObject.GetComponent<Bludger>().BeaterIntervention(EnemyQuaffle().gameObject);

            }
            else
            {
                int playerNum = Random.Range(0, 7);

                collision.gameObject.GetComponent<Bludger>().BeaterIntervention(MyRivals[playerNum].gameObject);
            }
        }
    }

    /*
    void OnTriggerExit(Collider col)
    {
        // Deja de estar en mi rango de sensor de vecinos
        if (NearPlayers.Contains(col.gameObject))
            NearPlayers.Remove(col.gameObject);

        if (player != null)
        {
            if (NearTeammates.Contains(col.gameObject))
                NearTeammates.Remove(col.gameObject);

            if (NearRivals.Contains(col.gameObject))
                NearRivals.Remove(col.gameObject);
        }
    }
    */
}
