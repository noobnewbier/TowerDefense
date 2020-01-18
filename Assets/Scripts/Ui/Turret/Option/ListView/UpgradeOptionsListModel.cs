using System.Collections.Generic;
using System.Linq;
using Elements.Turret.Upgrade;
using Ui.Turret.Option.Entry;
using UnityEngine;

namespace Ui.Turret.Option.ListView
{
    public interface IUpgradeOptionListModel
    {
        void SetUpgradableEntries(IEnumerable<TurretUpgradeEntry> upgradeEntries);
    }

    [CreateAssetMenu(menuName = "ScriptableModel/UpgradeOptionList")]
    public class UpgradeOptionsListModel : ScriptableObject, IUpgradeOptionListModel
    {
        [SerializeField] private UpgradeOptionModel[] upgradeOptionModels;

        public void SetUpgradableEntries(IEnumerable<TurretUpgradeEntry> upgradeEntries)
        {
            var upgradeEntriesArray = upgradeEntries.ToArray();
            for (var i = 0; i < upgradeOptionModels.Length; i++)
            {
                upgradeOptionModels[i].TurretUpgradeEntry = i < upgradeEntriesArray.Length ? upgradeEntriesArray[i] : null;
            }
        }
    }
}