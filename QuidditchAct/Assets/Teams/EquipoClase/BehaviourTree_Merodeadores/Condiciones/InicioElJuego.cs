using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    /// <summary>
    /// It is a condition of perception to check if the objective is in view depending on a given range.
    /// </summary>
    [Condition("Quidditch/Conditions/InicioElJuego")]
    [Help("Checa si el juego ya inicio")]
    public class InicioElJuego : GOCondition
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