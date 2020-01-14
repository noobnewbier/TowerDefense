using System.Collections.Generic;
using System.Linq;
using Rules;
using UnityEngine;

namespace Elements.Turret.Upgrade
{
    public interface ITurretUpgradableQueriesService
    {
        IEnumerable<TurretUpgradeEntry> GetUpgradables(IUpgradable upgradingFrom);
    }

    [CreateAssetMenu(menuName ="ScriptableService/TurretUpgradableQueries" )]
    public class TurretUpgradableQueriesService : ScriptableObject, ITurretUpgradableQueriesService
    {
        [SerializeField] private TurretUpgradeEntry[] allUpgradableTurrets;

        public IEnumerable<TurretUpgradeEntry> GetUpgradables(IUpgradable upgradingFrom)
        {
            return allUpgradableTurrets.Where(
                e => e.RulesToUpgrade.All(
                    r => r.AdhereToRule(upgradingFrom)
                )
            );
        }
    }
}