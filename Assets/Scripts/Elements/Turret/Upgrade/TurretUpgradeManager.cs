using System.Collections.Generic;
using System.Linq;
using Rules;
using UnityEngine;

namespace Elements.Turret.Upgrade
{
    public class TurretUpgradeManager : ScriptableObject
    {
        [SerializeField] private TurretUpgradeEntry[] allUpgradableTurrets;

        public IEnumerable<TurretUpgradeEntry> GetUpgradables(IHasFact upgradingFrom)
        {
            return allUpgradableTurrets.Where(
                e => e.RulesToUpgrade.All(
                    r => r.AdhereToRule(upgradingFrom)
                )
            );
        }
    }
}