using UnityEngine;

namespace Effects.Modifiers
{
    public abstract class Modifier : ScriptableObject
    {
        public abstract float ModifyValue(float value);
        public abstract float RevertValue(float value);
    }
}