using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    /// <summary>
    /// Verifixa si el juego ha comenzado 
    /// </summary>
    [Condition("CartasCondicion/DetuvisteQuaffle")]
    [Help("Verifica si el KEEPER collisiono con qffle")]
    public class Merodeadores_Detuviste : GOCondition
    {
        ///<value>Input Parameter para checar el numero del jugador.</value>
        //[InParam("QuienQuaffle")]
        //[Help("ver quien tiene quaffle")]
        //public bool TengoQuaffle;

        
        /// <summary>
        /// verifica el turno del juego y si consuerdas con este jugador
        /// </summary>
        /// <returns>True if el num de turno es del jugador.</returns>
        public override bool Check()
		{
            //ir a ver si hubo collision

            if(gameObject.GetComponent<Equipo_keeperMerodeadores>().Detenido == true)
            {
                Debug.Log("detenido");
                return true;
            }
            else
            {
                Debug.Log("no detenido");
                return false;
            }
            /*
            bool Listo = gameObject.GetComponent<Equipo_keeper>().EnPosPorteria;
            if (Listo)
            {
                bool LaTengo = gameObject.GetComponent<Equipo_keeper>().TengoLaQuaffle();
                if (TengoQuaffle == LaTengo)
                {
                    Debug.Log("hay quaffle");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }*/
            //int turnoActual = GameObject.Find("Game").GetComponent<Game>().GetTurn();
            //Debug.Log(turnoActual);
            //return numeroTurno == turnoActual;

		}
    }
}