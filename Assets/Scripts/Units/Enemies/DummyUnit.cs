using Bullets;
using Common;
using Units.UnitCommon;
using UnityEngine;

namespace Units.Enemies
{
    public class DummyUnit : Unit
    {
        protected override void Dies()
        {
            Destroy(gameObject);
        }
    }
}