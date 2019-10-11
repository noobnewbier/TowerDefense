using UnityEngine;

namespace Enemies
{
    public interface IEnemy
    {
        UnitData UnitData { get; }
        Transform Transform { get; }
    }
}