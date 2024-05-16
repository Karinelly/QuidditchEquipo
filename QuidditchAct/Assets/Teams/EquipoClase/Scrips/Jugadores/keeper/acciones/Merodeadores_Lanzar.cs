using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to activate a GameObject.
    /// </summary>
    [Action("Cartas de accion/Lanzar")]
    [Help("Lanza el balón a alguno de sus compañeros")]
    public class Merodeadores_Lanzar : GOAction
    {
        

        public override void OnStart()
        {
            Debug.Log("Entro A acc lanzar");

            Equipo_keeperMerodeadores Keppp = gameObject.GetComponent<Equipo_keeperMerodeadores>();

            //lanza el balon
            
            Keppp.LanzarQffl();

            //Keppp.EnMovIdle = true;
            //acceder a script player
            //Player playerInfo = gameObject.GetComponent<Player>();

            //llamar al codigo para que inicie el pathFollow

            //usamos la funcion de daño en el rival
            //playerInfo.GetEnemyInfo().Damage();
        }

        /// <summary>Method of Update of SetActive.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
