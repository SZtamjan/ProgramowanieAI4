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
        private NPCStates _currentState;
        
        //Properties
        public NPCStates CurrentState => _currentState;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        
        private void Start()
        {
            PrepareData();
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

        private void ChangeState(NPCStates newState)
        {
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