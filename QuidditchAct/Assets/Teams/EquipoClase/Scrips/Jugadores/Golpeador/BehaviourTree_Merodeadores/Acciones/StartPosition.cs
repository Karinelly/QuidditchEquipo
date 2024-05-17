using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// 
    /// </summary>
    [Action("Quidditch/Steering/StarPosition")]
    [Help("Arrive")]
    public class StartPosition : GOAction
    {
        ///<value>Input Game object where the force is applied Parameter.</value>
        [InParam("SteeringMerodeadores")]
        [Help("Steering Component")]
        public Arrive_Merodeadores arrive;

        ///<value>Input Force to be applied Parameter.</value>
        [InParam("Active")]
        [Help("Está activo o no")]
        public bool activo;

        ///<value>Input Force to be applied Parameter.</value>
        [InParam("Weight")]
        [Help("Ponderación")]
        public float weight;


        /// <summary>Initialization Method of ApplyForce.</summary>
        /// <remarks>heckea the GameObject which we apply the force, look for the rigitbody component to add strength
        /// and if it does not exist, it adds rigitbody by default.</remarks>
        public override void OnStart()
        {
            Player_Merodeadores playerInfo = gameObject.GetComponent<Player_Merodeadores>();

            if (arrive == null)
                arrive = gameObject.GetComponent<Arrive_Merodeadores>();

            if(!GameManager.instancia.isGameStarted())
            {
                arrive.active = activo;
                arrive.weigth = weight;
                arrive.target = playerInfo.myStartingPosition;//myTarget.transform
            }
            else
            {
                arrive.active = false;
                arrive.weigth = 0;
                arrive.target = null;//myTarget.transform
            }

        }

        /// <summary>Abort method of ApplyForce.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
