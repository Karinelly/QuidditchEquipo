using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipo_keeperMerodeadores : MonoBehaviour
{
    private Team_Merodeadores claseref;
    public Transform PosInicio;
    private Seek_Merodeadores seek;

    private bool llego;
    public bool EnPosPorteria = false;

    public bool EnMovIdle = false;
    public bool Intercept = false;
    public bool Detenido = false;
    public bool Lanzado = false;
    public bool Alto = false;

    public GameObject[] Paths;

    public GameObject QuaffleRef;
    public float ThrowStrength;
    public GameObject InicioPorteria;
    public GameObject DMedia;

    //referencia al n del team
    public int NTeam;
    public int NYo;
    public Transform myKeeperStartingPosition;

    // Start is called before the first frame update
    void Start()
    {


        claseref = GameObject.Find("Merodeadores").GetComponent<Team_Merodeadores>();
        seek = this.gameObject.GetComponent<Seek_Merodeadores>();

        QuaffleRef = GameObject.Find("Quaffle");
        InicioPorteria = GameObject.Find("path1");

        NYo = gameObject.GetComponent<Player_Merodeadores>().myNumberInTeam;


        if (NTeam == 1)
        {
            print(NTeam);
            Paths = new GameObject[3];
            Paths[0] = GameObject.Find("Team2Goal1");
            Paths[1] = GameObject.Find("Team2Goal2");
            Paths[2] = GameObject.Find("Team2Goal3");

            //mySeekerStartingPosition = GameObject.Find("Team1StartPosition (6)").GetComponent<Transform>();

            DMedia = GameObject.Find("path4_2");
        }
        else if (NTeam == 2)
        {
            print(NTeam);
            Paths = new GameObject[3];
            Paths[0] = GameObject.Find("Team1Goal1");
            Paths[1] = GameObject.Find("Team1Goal2");
            Paths[2] = GameObject.Find("Team1Goal3");

            //mySeekerStartingPosition = GameObject.Find("Team2StartPosition (6)").GetComponent<Transform>();

            DMedia = GameObject.Find("path4");
        }
        else
        {
            print("no hay team");
        }

    }

    public float PosQuaffle()
    {
        if (NTeam == 1)
        {
            DMedia = GameObject.Find("path4_2");
        }
        else if (NTeam == 2)
        {
            DMedia = GameObject.Find("path4");
        }
        //DMedia = GameObject.Find("path4");
        //float distQuaffle = Mathf.Abs((Mathf.Abs(Paths[0].GetComponent<Transform>().position.x)) - (Mathf.Abs(QuaffleRef.GetComponent<Transform>().position.x)));
        float distanciaQffle = Vector3.Distance(DMedia.GetComponent<Transform>().position, QuaffleRef.GetComponent<Transform>().position);
        //print(distanciaQffle);
        return distanciaQffle;
    }

    public GameObject VistaQuaffle()
    {
        //La quaffle está viendo a alguno de los aros?

        float dot = Vector3.Dot(Paths[0].transform.forward, (QuaffleRef.GetComponent<Transform>().position - Paths[0].transform.position).normalized);
        if (dot > 0.7f)
        {
            Debug.Log("Quite facing");
            return Paths[0];
        }
        dot = Vector3.Dot(Paths[1].transform.forward, (QuaffleRef.GetComponent<Transform>().position - Paths[1].transform.position).normalized);
        if (dot > 0.7f)
        {
            Debug.Log("Quite facing");
            return Paths[1];
        }
        dot = Vector3.Dot(Paths[2].transform.forward, (QuaffleRef.GetComponent<Transform>().position - Paths[2].transform.position).normalized);
        if (dot > 0.7f)
        {
            Debug.Log("Quite facing");
            return Paths[2];
        }

        Debug.Log("ninguna vista");
        return null;

    }



    // Update is called once per frame
    void Update()
    {
        if (llego == false && PosInicio != null)
        {
            Inicio();
            if (Vector3.Distance(
                    gameObject.transform.position,
                    gameObject.GetComponent<Seek_Merodeadores>().Target.position) < 6f)
            {
                seek.active = false;
                llego = true;
            }
        }
    }

    public bool TengoLaQuaffle()
    {
        Debug.Log("Entro");
        if (claseref.TeamQuafle() == true)
        {
            Debug.Log("Tengo Quaffle");
            return true;
        }
        else
        {
            Debug.Log("NO Tengo Quaffle");
            return false;
        }
    }

    public void LanzarQffl()
    {
        /*
        //QUIEN ES TARGET?
        float distcMin = float.MaxValue;
        Transform jugadorMasCercano = null;
        foreach(Transform jugador in claseref.MyTeam)
        {
            if(jugador.gameObject.GetComponent<Cazador_Clase>())
            {
                float dist = Vector3.Distance(gameObject.transform.position, jugador.position);
                if(dist < distcMin)
                {
                    distcMin = dist;
                    jugadorMasCercano = jugador;
                }

            }
        }

        seek.Target = jugadorMasCercano;
        print(jugadorMasCercano.gameObject);
        if (seek.Target != null)
        {
            //LANZAR
            GameManager.instancia.Quaffle.GetComponent<Quaffle>().
                    Throw(
                        seek.Target.position - gameObject.transform.position,
                        ThrowStrength
                        );
        }
        else
        {
            print("No compañero");
        }*/


        seek.Target = claseref.FindClosestTeammateToQuaffle();
        print(seek.Target.gameObject);
        if (seek.Target != null)
        {
            //LANZAR
            GameManager.instancia.Quaffle.GetComponent<Quaffle>().
                    Throw(
                        -(seek.Target.position + gameObject.transform.position),
                        ThrowStrength
                        );
        }
        else
        {
            print("No compañero");
        }


        Lanzado = false;
        Detenido = false;
        Intercept = false;
        EnPosPorteria = false;
        print("POSporteria" + EnPosPorteria);
    }

    public void EnPorteria(bool ahi)
    {
        EnPosPorteria = ahi;
    }

    public void MoverseInicial(bool enMov)
    {

        EnMovIdle = enMov;

    }

    public void MovInterceptar(bool enMov)
    {

        Intercept = enMov;

    }

    private void Inicio()
    {
        myKeeperStartingPosition = gameObject.GetComponent<Player_Merodeadores>().myStartingPosition;
        seek.active = true;

        if (myKeeperStartingPosition != null)
        {
            PosInicio = myKeeperStartingPosition;
            seek.Target = myKeeperStartingPosition;

        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball Quaffle"))
        {
            GameManager.instancia.FreeQuaffle();
            GameManager.instancia.ControlQuaffle(this.gameObject);
            print("collision");
            Detenido = true;
        }


    }
}
