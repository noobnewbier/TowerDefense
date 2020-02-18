using Rules;
using UnityEngine;

namespace Elements.Turret.Upgrade
{
    public interface IUpgradable : IHasFact
    {
        Transform CurrentTransform { get; }
        void Destruct();
        void Construct();
    }
}