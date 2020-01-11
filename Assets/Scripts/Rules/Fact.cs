using UnityEngine;

namespace Rules
{
    [CreateAssetMenu(menuName = "Information")]
    public class Fact : ScriptableObject
    {
        [SerializeField] private string description;
    }
}