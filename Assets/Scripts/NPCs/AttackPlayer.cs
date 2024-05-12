using System;
using System.Collections;
using UnityEngine;

namespace NPCs
{
    public class AttackPlayer : MonoBehaviour
    {
        //Components
        private NPCAI _npcai;
        
        //Vars
        [SerializeField] private float attackFrequency = 1f;
        private Coroutine _attackCor;

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
        }

        public void StartAttack()
        {
            _npcai.NavMeshAgent.enabled = false;
            if (_attackCor == null)
            {
                _attackCor = StartCoroutine(AttackPlayerCor());
            }
        }

        public void StopAttack()
        {
            _npcai.NavMeshAgent.enabled = true;
            if (_attackCor != null)
            {
                StopCoroutine(_attackCor);
                _attackCor = null;
            }
        }

        private IEnumerator AttackPlayerCor()
        {
            while (true)
            {
                Debug.Log("Pif Paf!");
                yield return new WaitForSeconds(attackFrequency);
            }
        }
    }
}