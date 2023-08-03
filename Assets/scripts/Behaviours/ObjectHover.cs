using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReUpVirtualTwin
{
    [RequireComponent(typeof(IHighligher))]
    [RequireComponent(typeof(IObjectSelector))]
    [RequireComponent(typeof(IRayProvider))]
    public class ObjectHover : MonoBehaviour
    {
        private GameObject _currentSelection;
        private IHighligher _objHighligher;
        private IObjectSelector _objSelector;
        private IRayProvider _rayProvider;


        void Start()
        {
            _objHighligher = GetComponent<IHighligher>();
            _objSelector = GetComponent<IObjectSelector>();
            _rayProvider = GetComponent<IRayProvider>();
        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = _rayProvider.GetRay();
            //Debug.DrawRay(ray.origin, ray.direction, Color.green);
            GameObject selection = _objSelector.GetObject(ray);
            bool newObj = !GameObject.ReferenceEquals(selection, _currentSelection);
            if (newObj)
            {
                _objHighligher.RemoveSelection(_currentSelection);
                _objHighligher.AddSelection(selection);
                _currentSelection = selection;
            }
        }
    }
}

