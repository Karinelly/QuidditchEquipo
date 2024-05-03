using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team_Clase: Team
{
    public string EquipoClaseName = "EquipoClase";

    private int EquipoClaseTeamNumber;    // Me lo asigna el game manager
    public int getTeamNumber()
    {
        return EquipoClaseTeamNumber;
    }

    public List<Transform> MyTeam;  // Yo quiero tener a mis jugadores en una lista
    public List<Transform> MyRivals;      // rivales

    private GameObject QuaffleOwner;
    private Transform ClosestTeammateToQuaffle;

    public List<Transform> rivalGoals;

    public List<Transform> myStartingPositions; // Saber donde inician mis jugadores
    public Transform mySeekerStartingPosition;

    public Color myChidoColor;

    protected override void Start()
    {
        //base.Start();

        // Voy a buscar a mis jugadores
        MyTeam = new List<Transform>();
        MyTeam.Add(transform.Find("Cazador1"));
        MyTeam.Add(transform.Find("Cazador2"));
        MyTeam.Add(transform.Find("Cazador3"));
        MyTeam.Add(transform.Find("Cazador4"));
        MyTeam.Add(transform.Find("Cazador5"));
        MyTeam.Add(transform.Find("Cazador6"));
        MyTeam.Add(transform.Find("Cazador7"));

        Teammates = MyTeam;

        // Le aviso al GameManager mi nombre de equipo y 
        // me regresa el número de equipo que me toca
        EquipoClaseTeamNumber =
            GameManager.instancia.SetTeamName(EquipoClaseName);

        // Ahora que sé el número de equipo
        if (EquipoClaseTeamNumber == 1)
        {
            //le puedo decir quienes son mis jugadores
            GameManager.instancia.team1Players = MyTeam;
            // Puedo saber hacia donde tiro
            rivalGoals = GameManager.instancia.team2Goals;
        }
        else if (EquipoClaseTeamNumber == 2)
        {
            GameManager.instancia.team2Players = MyTeam;

            rivalGoals = GameManager.instancia.team1Goals;
        }

        GameManager.instancia.SetTeamColor(EquipoClaseTeamNumber, myChidoColor);

        Invoke("FillLateData", 1f);
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
            MyTeam[j].GetComponent<Player>().myNumberInTeam = j;
            MyTeam[j].GetComponent<Player>().myStartingPosition = myStartingPositions[j];
        }
        MyTeam[6].GetComponent<Player>().myNumberInTeam = 6;
        MyTeam[6].GetComponent<Player>().myStartingPosition = mySeekerStartingPosition;

    }

    // Update is called once per frame
    protected override void Update()
    {
        //base.Update();
    }

    public void FindClosestTeammateToQuaffle()
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
    }
}
