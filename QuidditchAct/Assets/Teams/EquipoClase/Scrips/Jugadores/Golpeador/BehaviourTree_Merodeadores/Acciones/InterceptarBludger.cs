using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// 
    /// </summary>
    [Action("Quidditch/Steering/InterceptarBludger")]
    [Help("arrive")]
    public class InterceptarBludger : GOAction
    {
        private Interpose_Merodeadores interpose;


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

            Golpeador_Merodeadores GolpeadorInfo = gameObject.GetComponent<Golpeador_Merodeadores>();
            if (interpose == null)
            {
                interpose = gameObject.GetComponent<Interpose_Merodeadores>();
                interpose.active = activo;

                interpose.weigth = weight;
            }
            
            interpose.AgenteA = gameObject.transform;
            interpose.AgenteB = GolpeadorInfo.DetectedBludger();

            if(gameObject.GetComponent<Golpeador_Merodeadores>().isTeamMateTarget() == false)
            { 
                interpose.active = false;

                interpose.weigth = 0f;

                interpose.AgenteA = null;
                interpose.AgenteB = null;
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
