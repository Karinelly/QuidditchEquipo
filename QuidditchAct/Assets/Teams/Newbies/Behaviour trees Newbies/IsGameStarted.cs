using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    /// <summary>
    /// It is a condition of perception to check if the objective is in view depending on a given range.
    /// </summary>
    [Condition("Quidditch/Perception/IsGameStarted")]
    [Help("Checks whether a target is in sight depending on a given angle")]
    public class IsGameStarted : GOCondition
    {
        

        /// <summary>
        /// Ya inició el juego?
        /// </summary>
        /// <returns>Regresa verdadero si sí.</returns>
        public override bool Check()
		{
            return GameManager.instancia.isGameStarted();
		}
    }
}