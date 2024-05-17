using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    /// <summary>
    /// It is a condition of perception to check if the objective is in view depending on a given range.
    /// </summary>
    [Condition("Quidditch/Conditions/isTeamMateAtack")]
    [Help("Checa si algun compañero es target de una bludger")]
    public class isTeamMateAtack : GOCondition
    {
        /// <summary>
        /// Ya inició el juego?
        /// </summary>
        /// <returns>Regresa verdadero si sí.</returns>
        public override bool Check()
		{
		
            return gameObject.GetComponent<Golpeador_Merodeadores>().isTeamMateTarget();
		}
    }
}