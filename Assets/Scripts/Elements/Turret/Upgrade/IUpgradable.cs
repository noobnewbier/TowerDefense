using Rules;
using UnityEngine;

namespace Elements.Turret.Upgrade
{
    public interface IUpgradable :IHasFact
    {
        void UpgradeFrom(GameObject newTurret);
    }
}