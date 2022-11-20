using System;
using System.Collections;
using System.Collections.Generic;
using Gummi.Utility;
using UnityEngine;

namespace Gummi.Play
{
    [RequireComponent(typeof(Rigidbody))]
    public class BreakablePiece : MonoBehaviour
    {
        [SerializeField] float _minimumTimeAlive = 30f;
        [SerializeField] bool _waitTillDoneMoving = true;
        
        [SerializeField, ShowIf(nameof(_waitTillDoneMoving))]
        float _waitInterval = 5f;

        [SerializeField]
        Rigidbody _rb;

        float _elapsedTime;
        bool _performedInitialWait;
        
        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        
        void Update()
        {
            _elapsedTime += Time.deltaTime;
            
            // perform initial wait
            if (!_performedInitialWait)
            {
                if (_elapsedTime > _minimumTimeAlive)
                {
                    _performedInitialWait = true;
                }
                else
                {
                    return;
                }
            }
            
            if (_waitTillDoneMoving)
            {
                bool isMoving = !_rb.IsSleeping();
                bool tooSoon = _elapsedTime < _waitInterval;
                if (isMoving || tooSoon)
                {
                    _elapsedTime = isMoving ? 0 : _elapsedTime + Time.deltaTime;
                    return;
                }
            }
            
            Destroy(gameObject);
        }
    }
}