using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    /// <summary>
    /// It is a condition of perception to check if the objective is in view depending on a given range.
    /// </summary>
    [Condition("Quidditch/Conditions/QuaffleEnemiga")]
    [Help("Checa si la Quaffle esta libre")]
    public class QuaffleEnemiga : GOCondition
    {
        bool check;
        /// <summary>
        /// Ya inició el juego?
        /// </summary>
        /// <returns>Regresa verdadero si sí.</returns>
        public override bool Check()
		{
            return gameObject.GetComponent<Golpeador_Merodeadores>().isEnemyQuaffle();
		}
    }
}