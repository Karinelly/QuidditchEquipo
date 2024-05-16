using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;

[RequireComponent(typeof(SteeringBlender_Merodeadores))]

public class Player_Merodeadores : MonoBehaviour
{
    public SteeringBlender_Merodeadores steering;

    // Variables del jugador
    private bool hitted = false;

    public Team myTeam;

    public float resistence = 0.5f;    // Players resistence to hits

    public int myNumberInTeam;
    public Transform myStartingPosition = null;

    // Que posicion tiene este jugador
    public enum PlayerPosition
    {
        Keeper,
        Beater,
        Chaser,
        Seeker
    };
    public PlayerPosition playerPosition;

    // Use this for initialization
    protected virtual void Start()
    {
        // Referencia a mi equipo para conocer a mis compañeros, mi cancha y los aros rivales
        myTeam = transform.parent.GetComponent<Team_Merodeadores>();

        // Asignar el steering
        steering = GetComponent<SteeringBlender_Merodeadores>();
    }

    protected virtual void Update()
    {
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        // Si me pega un rival o una pelota
        if (myTeam.isRival(collision.gameObject) || collision.gameObject.tag.Equals("Ball Bludger"))
        {
            // Me pegaron con suficiente fuerza
            if (collision.relativeVelocity.magnitude > 2) //calibrar
            {
                hitted = true;
            }
        }
    }

    protected bool isHitted()
    {
        return hitted;
    }
}
