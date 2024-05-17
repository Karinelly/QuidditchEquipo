using Pada1.BBCore;
using Pada1.BBCore.Tasks;

namespace BBUnity.Actions
{
    /// <summary>
    /// 
    /// </summary>
    [Action("Quidditch/Steering/BuscarBludger")]
    [Help("Seek")]
    public class BuscarBludger : GOAction
    {
        private Pursuit_Merodeadores pursuit;

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

            if (pursuit == null)
            {
                pursuit = gameObject.GetComponent<Pursuit_Merodeadores>();
            }
            pursuit.active = activo;
            pursuit.weigth = weight;
            //arrive.distR = distR;
            
            pursuit.Target = GolpeadorInfo.BludgerMasCercana();

            if (gameObject.GetComponent<Golpeador_Merodeadores>().isEnemyQuaffle() == false)
            {
                pursuit.Target = null;
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
