using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCs
{
    public class PatrolArea : MonoBehaviour
    {
        //Components
        private NPCAI _npcai;
        
        //Vars
        private Coroutine _patrolCoroutine;
        
        [SerializeField] private List<Transform> checkPoints;
        private int _currentIteration = 0;
        private Transform _currentPoint;

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

        public void StartPatrol()
        {
            if (_patrolCoroutine == null)
            {
                _patrolCoroutine = StartCoroutine(PatrolAreaCor());
            }
        }
        
        public void StopPatrol()
        {
            if (_patrolCoroutine != null)
            {
                StopCoroutine(_patrolCoroutine);
                _patrolCoroutine = null;
            }

            _currentPoint = null;
        }

        private IEnumerator PatrolAreaCor()
        {
            if (_currentPoint == null)
            {
                SetDestanation(checkPoints[_currentIteration]);
            }
            
            while (true)
            {
                CheckDestanation();
                
                yield return null;
            }
        }

        private void CheckDestanation()
        {
            if (Vector3.Distance(transform.position, _currentPoint.position) < .1f)
            {
                if (++_currentIteration >= checkPoints.Count)
                {
                    _currentIteration = 0;
                }
                SetDestanation(checkPoints[_currentIteration]);
            }
        }

        private void SetDestanation(Transform newDestanation)
        {
            _currentPoint = newDestanation;
            _npcai.NavMeshAgent.destination = _currentPoint.position;
        }
    }
}