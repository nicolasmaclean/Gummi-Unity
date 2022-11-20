using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Gummi.Utility;
using UnityEngine;

namespace Gummi.Play
{
    public class Breakable : MonoBehaviour
    {
        [SerializeField] GameObject _unbroken;
        [SerializeField] GameObject _broken;
        
        [SerializeField]
        BreakablePiece[] _pieces;
        
        [Header("Collision")]
        [SerializeField]
        FloatSquared _breakingForce;
        
        [SerializeField]
        bool _breakOnCollision = true;
        
        [SerializeField]
        [ShowIf(nameof(_breakOnCollision))]
        LayerMask _layerMask = ~0;

        [SerializeField]
        Rigidbody _rb;

        void Awake()
        {
            _broken.SetActive(false);
            _rb = GetComponent<Rigidbody>();
        }

        void OnCollisionEnter(Collision collision)
        {
            // exit, collision breaking is disabled
            if (!_breakOnCollision) return;
            
            // exit, other object is not on an included layer
            int otherLayer = collision.gameObject.layer;
            if (!_layerMask.Includes(otherLayer)) return;
            
            // exit, collision was too small
            if (collision.impulse.sqrMagnitude < _breakingForce.ValueSQ) return;
            
            Break();
        }

        public void Break()
        {
            // disable the current rigidbody
            Destroy(_rb);
            
            // swap versions
            _unbroken.SetActive(false);
            _broken.SetActive(true);
            
            if (_pieces.Length == 0)
            {
                Destroy(gameObject);
            }
            
            // TODO: destroy self
            // if all children of broken are pieces
            // and they have all been destroyed
        }

        #if UNITY_EDITOR
        void OnValidate()
        {
            _pieces = GetComponentsInChildren<BreakablePiece>();
        }
        #endif
    }
}