using System;
using System.Collections;
using Player;
using UnityEngine;

namespace NPCs
{
    public class PlayerTracker : MonoBehaviour
    {
        //Components
        private Transform _playerTransform;
        private NPCAI _npcai;
        
        //Vars
        private float chaseDistance = 2f;
        private float attackDistance = 1f;
        
        //Properties
        public Transform PlayerTransform => _playerTransform;
        private void Start()
        {
            PrepareData();
            StartCoroutine(TrackPlayer());
        }

        private void PrepareData()
        {
            _playerTransform = PlayerScript.Instance.transform;
            if (TryGetComponent(out NPCAI npcai))
            {
                _npcai = npcai;
            }

            chaseDistance = _npcai.ChaseDistance;
            attackDistance = _npcai.AttackDistance;
        }

        private IEnumerator TrackPlayer()
        {
            while (true)
            {
                yield return null;
                float distance = Vector3.Distance(transform.position, _playerTransform.position);

                if (distance < attackDistance)
                {
                    if (_npcai.CurrentState != NPCStates.attack) _npcai.ChangeState(NPCStates.attack);
                    continue;
                }

                if (distance < chaseDistance)
                {
                    if (_npcai.CurrentState != NPCStates.chase) _npcai.ChangeState(NPCStates.chase);
                    continue;
                }
                
                if (distance > chaseDistance)
                {
                    if (_npcai.CurrentState != NPCStates.patrol) _npcai.ChangeState(NPCStates.patrol);
                    continue;
                }
            }
        }
    }
}