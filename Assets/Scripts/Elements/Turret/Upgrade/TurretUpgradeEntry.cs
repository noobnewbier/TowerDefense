using System.Collections.Generic;
using Rules;
using UnityEngine;

namespace Elements.Turret.Upgrade
{
    [CreateAssetMenu(menuName = "Data/TurretUpgradeEntry")]
    public class TurretUpgradeEntry : ScriptableObject
    {
        [SerializeField] private Rule[] rulesToUpgrade;
        [SerializeField] private TurretProvider turretProvider;

        public IEnumerable<Rule> RulesToUpgrade => rulesToUpgrade;
        public ITurretRepository TurretRepository => turretProvider.GetRepository();

        public TurretProvider TurretProvider => turretProvider;
    }
}