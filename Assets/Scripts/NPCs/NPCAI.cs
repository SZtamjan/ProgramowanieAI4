using UnityEngine;
using UnityEngine.AI;

namespace NPCs
{
    public class NPCAI : MonoBehaviour
    {
        //Components
        private PatrolArea _patrolArea;
        private NavMeshAgent _navMeshAgent;
        
        //Vars
        [SerializeField] private float chaseDistance = 2f;
        [SerializeField] private float attackDistance = 1f;
        private NPCStates _currentState;
        
        //Properties
        public NPCStates CurrentState => _currentState;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public float ChaseDistance => chaseDistance;

        public float AttackDistance => attackDistance;
        private void Start()
        {
            PrepareData();
            StartNPC();
        }

        private void StartNPC()
        {
            _currentState = NPCStates.attack;
            ChangeState(NPCStates.patrol);
        }
        
        private void PrepareData()
        {
            if (TryGetComponent(out PatrolArea patrolArea))
            {
                _patrolArea = patrolArea;
            }

            if (TryGetComponent(out NavMeshAgent navMeshAgent))
            {
                _navMeshAgent = navMeshAgent;
            }
        }

        public void ChangeState(NPCStates newState)
        {
            if (newState == _currentState) return;
            
            switch (newState)
            {
                case NPCStates.patrol:
                    _currentState = NPCStates.patrol;
                    
                    _patrolArea.StartPatrol();
                    break;
                case NPCStates.chase:
                    _currentState = NPCStates.chase;
                    _patrolArea.StopPatrol();
                    
                    break;
                case NPCStates.attack:
                    _currentState = NPCStates.attack;
                    _patrolArea.StopPatrol();
                    
                    break;
            }
        }
        
    }

    public enum NPCStates
    {
        patrol,
        chase,
        attack
    }
}