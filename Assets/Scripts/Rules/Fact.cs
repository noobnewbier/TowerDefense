using UnityEngine;

namespace Rules
{
    [CreateAssetMenu(menuName = "Fact")]
    public class Fact : ScriptableObject
    {
        [SerializeField] private string description;
    }
}