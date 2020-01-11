using System.Collections.Generic;
using Rules;
using Shop;
using UnityEngine;

namespace Elements.Turret.Upgrade
{
    public class TurretUpgradeEntry : ScriptableObject
    {
        [SerializeField] private TurretShopEntry shopEntry;

        public TurretShopEntry ShopEntry => shopEntry;

        [SerializeField] private Rule[] rulesToUpgrade;
        public IEnumerable<Rule> RulesToUpgrade => rulesToUpgrade;
    }
}