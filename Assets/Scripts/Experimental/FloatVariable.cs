﻿// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;

namespace Experimental
{
    [CreateAssetMenu(menuName = "FloatVariable")]
    public class FloatVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline] [SerializeField] private string developerDescription = "Self Descriptive Name";
#endif
        [SerializeField] private float value;

        public float Value => value;

        public static implicit operator float(FloatVariable variable)
        {
            return variable.value;
        }
    }
}