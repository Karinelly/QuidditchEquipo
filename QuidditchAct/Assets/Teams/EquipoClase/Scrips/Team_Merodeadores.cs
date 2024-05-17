using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team_Merodeadores : Team
{
    public static Team_Merodeadores instancia = null;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else if (instancia != this)
        {
            Destroy(gameObject);
        }
    }

    public string EquipoClaseName = "Merodeadores";

    private int EquipoMerodeadoresTeamNumber;    // Me lo asigna el game manager
    public int getTeamNumber()
    {
        return EquipoMerodeadoresTeamNumber;
    }

    public List<Transform> MyTeam;  // Yo quiero tener a mis jugadores en una lista
    public List<Transform> MyRivals;      // rivales

    private GameObject QuaffleOwner;
    private Transform ClosestTeammateToQuaffle;

    public List<Transform> rivalGoals;

    public Transform myGoals;
    public Transform[] myProtectGoal;

    public Transform PuntoDefenza1;
    public Transform PuntoDefenza2;
    public Vector3[] RondinPortero;

    public GameObject PuntosRef;


    public List<Transform> myStartingPositions; // Saber donde inician mis jugadores
    public Transform mySeekerStartingPosition;

    public Color myChidoColor;

    protected override void Start()
    {
        //*******************************Modificar PUNTOSREF
        //base.Start();
        //Instantiate(PuntosRef, new Vector3(0f, 0f, 0f), Quaternion.identity);
        PuntosRef = GameObject.Find("puntosReferencia");
        //PuntosRef.SetActive(true);
        PuntosRef.GetComponent<Transform>().position = new Vector3(0.0f, 0.0f, 0.0f);

        PuntoDefenza1 = GameObject.Find("porteria2").GetComponent<Transform>();
        PuntoDefenza2 = GameObject.Find("porteria2").GetComponent<Transform>();

        myProtectGoal = new Transform[3];


        //base.Start();

        // Voy a buscar a mis jugadores
        MyTeam = new List<Transform>();
        MyTeam.Add(transform.Find("Cazador1_Mero"));
        MyTeam.Add(transform.Find("Cazador2_Mero"));
        MyTeam.Add(transform.Find("Cazador3_Mero"));
        MyTeam.Add(transform.Find("Golpeador1_Mero"));
        MyTeam.Add(transform.Find("Golpeador2_Mero"));
        MyTeam.Add(transform.Find("keeper_Mero"));
        MyTeam.Add(transform.Find("Cazador7_Mero"));

        Teammates = MyTeam;

        // Le aviso al GameManager mi nombre de equipo y 
        // me regresa el número de equipo que me toca
        EquipoMerodeadoresTeamNumber =
            GameManager.instancia.SetTeamName(EquipoClaseName);

        // Ahora que sé el número de equipo
        if (EquipoMerodeadoresTeamNumber == 1)
        {
            //le puedo decir quienes son mis jugadores
            GameManager.instancia.team1Players = MyTeam;
            // Puedo saber hacia donde tiro
            rivalGoals = GameManager.instancia.team2Goals;

            GameObject.Find("keeper_Mero").GetComponent<Equipo_keeperMerodeadores>().NTeam = 1;

            PuntoDefenza1 = GameObject.Find("porteria1").GetComponent<Transform>();

        }
        else if (EquipoMerodeadoresTeamNumber == 2)
        {
            GameManager.instancia.team2Players = MyTeam;

            rivalGoals = GameManager.instancia.team1Goals;

            GameObject.Find("keeper_Mero").GetComponent<Equipo_keeperMerodeadores>().NTeam = 2;

            PuntoDefenza1 = GameObject.Find("porteria2").GetComponent<Transform>();
        }

        GameManager.instancia.SetTeamColor(EquipoMerodeadoresTeamNumber, myChidoColor);

        Invoke("FillLateData", 1f);
    }

    public Transform Porteria()
    {
        return PuntoDefenza1;
    }
    /// <summary>
    /// Hay información que puede no estar disponible en el Start pues no sabemos el orden en que se ejecutan
    /// los equipos, por lo que puede haber información no disponible.
    /// Este método llena la información después de cierto tiempo para tratar de asegurar que esté lista.
    /// </summary>
    void FillLateData()
    {
        if (getTeamNumber() == 1)
        {
            // Mis rivales
            MyRivals = GameManager.instancia.team2Players;
            base.Rivals = MyRivals;
            // Mis posiciones iniciales
            myStartingPositions = GameManager.instancia.Team1StartPositions;
            mySeekerStartingPosition = GameManager.instancia.Team1SeekerStartPosition;
        }
        else
        {
            MyRivals = GameManager.instancia.team1Players;
            base.Rivals = MyRivals;
            myStartingPositions = GameManager.instancia.Team2StartPositions;
            mySeekerStartingPosition = GameManager.instancia.Team2SeekerStartPosition;
        }

        for (int j = 0; j < 6; j++)
        {
            MyTeam[j].GetComponent<Player_Merodeadores>().myNumberInTeam = j;
            MyTeam[j].GetComponent<Player_Merodeadores>().myStartingPosition = myStartingPositions[j];
        }
        MyTeam[6].GetComponent<Player_Merodeadores>().myNumberInTeam = 6;
        MyTeam[6].GetComponent<Player_Merodeadores>().myStartingPosition = mySeekerStartingPosition;

    }

    public Transform SeekerPos()
    {

        return mySeekerStartingPosition;
    }

    // Update is called once per frame
    protected override void Update()
    {
        //base.Update();
    }

    public Transform FindClosestTeammateToQuaffle()
    {

        float less = float.MaxValue;
        float dist;

        foreach (Transform chido in MyTeam)
        {
            dist = Vector3.Distance(chido.position, GameManager.instancia.Quaffle.transform.position);
            if (dist < less)
            {
                less = dist;
                ClosestTeammateToQuaffle = chido;
            }
        }
        return ClosestTeammateToQuaffle;
    }

    public bool TeamQuafle()
    {
        if (QuaffleOwner != null)
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

    public void TengoQuaffle(GameObject mia)
    {
        QuaffleOwner = mia;
        Debug.Log("Lo tengo" + QuaffleOwner);
    }
}