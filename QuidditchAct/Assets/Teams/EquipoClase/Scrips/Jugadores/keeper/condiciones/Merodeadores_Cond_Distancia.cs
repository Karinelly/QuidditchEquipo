using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    /// <summary>
    /// Verifixa si el juego ha comenzado 
    /// </summary>
    [Condition("CartasCondicion/ADistancia")]
    [Help("Verifica la distacia")]
    public class Merodeadores_Cond_Distancia : GOCondition
    {
        ///<value>Input Parameter para checar el numero del jugador.</value>
        [InParam("CondicionDistancia")]
        [Help("ver que quiere saber")]
        public bool CualDistancia;

        
        /// <summary>
        /// verifica el turno del juego y si consuerdas con este jugador
        /// </summary>
        /// <returns>True if el num de turno es del jugador.</returns>
        public override bool Check()
		{
            FuzzyCalcularDist_Clase controlador = gameObject.GetComponent<FuzzyCalcularDist_Clase>();
            Equipo_keeperMerodeadores Kepp = gameObject.GetComponent<Equipo_keeperMerodeadores>();

            controlador.SetDistancia(Kepp.PosQuaffle());

            if (CualDistancia == controlador.EstaCerca())
            {
                Debug.Log("Si");
                return true;
            }
            else
            {
                Debug.Log("No");
                return false;
            }
            //int turnoActual = GameObject.Find("Game").GetComponent<Game>().GetTurn();
            //Debug.Log(turnoActual);
            //return numeroTurno == turnoActual;

		}
    }
}