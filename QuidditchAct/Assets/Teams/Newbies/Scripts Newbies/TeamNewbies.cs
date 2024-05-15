using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamNewbies : Team
{
    public string TeamName = "Newbies";

    /*private int MyTeamNumber;   // Me lo asigna el game manager
    public int GetTeamNumber()
    {
        return MyTeamNumber;
    }*/

    // Agregar variables que necesiten para administrar su equipo

    // Guardaremos hacia donde ataco
    public List<Transform> rivalGoals;
    private List<Transform> ownGoals;

    // Para saber donde formar a mis jugadores al inicio
    public List<Transform> MyStartingPositions;
    public Transform MySeekerStartingPosition;

    public Color MyTeamColor;

    // Start is called before the first frame update
    protected override void Start()
    {
        // si queremos ejecutar el Start de la clase base
        // base.Start();

        // voy a buscar a mis jugadores
        Teammates = new List<Transform>();

        Teammates.Add(transform.Find("Primer cazador"));
        Teammates.Add(transform.Find("Segundo cazador"));
        Teammates.Add(transform.Find("Tercer cazador"));
        Teammates.Add(transform.Find("Guardian"));
        Teammates.Add(transform.Find("Golpeador Uno"));
        Teammates.Add(transform.Find("Golpeador Dos"));
        Teammates.Add(transform.Find("Buscador"));

        // Le avisamos al game manager mi nombre de equipo y
        // me regresará el número de equipo que me toca
        teamNumber = GameManager.instancia.SetTeamName(TeamName);

        // Ahora que ya sabemos el número de equipo
        if(teamNumber == 1)
        {
            // Le digo al game manager quienes son mis jugadores
            GameManager.instancia.team1Players = Teammates;
            // Ahora puedo saber a que aros tengo que tirar
            rivalGoals = GameManager.instancia.team2Goals;
            ownGoals = GameManager.instancia.team1Goals;
        }
        else
        {
            GameManager.instancia.team2Players = Teammates;

            rivalGoals = GameManager.instancia.team1Goals;
            ownGoals = GameManager.instancia.team2Goals;
        }

        GameManager.instancia.SetTeamColor(teamNumber, MyTeamColor);

        Invoke("FillLateData", 1f);
    }

    
    private void FillLateData()
    {
        if(GetTeamNumber() == 1)
        {
            // Mis posiciones iniciales
            MyStartingPositions = GameManager.instancia.Team1StartPositions;
            MySeekerStartingPosition = GameManager.instancia.Team1SeekerStartPosition;
        }
        else
        {
            MyStartingPositions = GameManager.instancia.Team2StartPositions;
            MySeekerStartingPosition = GameManager.instancia.Team2SeekerStartPosition;
        }

        // Le asignamos una posición inicial a cada uno de los jugadores
        for(int jugador = 0; jugador < 6; jugador++)
        {
            Teammates[jugador].GetComponent<Player>().myStartingPosition =
                 MyStartingPositions[jugador];
        }
        Teammates[6].GetComponent<Player>().myStartingPosition =
            MySeekerStartingPosition;
    }
}
