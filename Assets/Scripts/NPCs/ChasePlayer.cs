using System;
using System.Collections;
using UnityEngine;

namespace NPCs
{
    public class ChasePlayer : MonoBehaviour
    {
        //Components
        private NPCAI _npcai;
        private PlayerTracker _playerTracker;
        
        //Vars
        private Coroutine _chaseCor;

        private void Start()
        {
            PrepareData();
        }

        private void PrepareData()
        {
            if (TryGetComponent(out NPCAI npcai))
            {
                _npcai = npcai;
            }

            if (TryGetComponent(out PlayerTracker playerTracker))
            {
                _playerTracker = playerTracker;
            }
        }

        public void StartChase()
        {
            if (_chaseCor == null)
            {
                _chaseCor = StartCoroutine(ChasePlayerCor());
            }
        }
        
        public void StopChase()
        {
            if (_chaseCor != null)
            {
                StopCoroutine(_chaseCor);
                _chaseCor = null;
            }
        }

        private IEnumerator ChasePlayerCor()
        {
            while (true)
            {
                _npcai.NavMeshAgent.destination = _playerTracker.PlayerTransform.position;
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}