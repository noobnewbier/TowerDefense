using System.Collections;
using UnityEngine;
using UnityTechRaw.KartAndFPS.Assets.Karting.Scripts.KartSystems.KartModifiers;
using UnityTechRaw.KartAndFPS.Assets.Karting.Scripts.KartSystems.KartMovement;

namespace UnityTechRaw.KartAndFPS.Assets.Mod_Assets.ModResources.Scripts.Kart
{
    public class KartSpeedPad : MonoBehaviour
    {

        public MultiplicativeKartModifier boostStats;

        [Range (0, 5)]
        public float duration = 1f;

        void OnTriggerEnter(Collider other){
            var rb = other.attachedRigidbody;
            if (rb == null) return;
            var kart = rb.GetComponent<KartMovement>();
            kart.StartCoroutine(KartModifier(kart, duration));
        }

        IEnumerator KartModifier(KartMovement kart, float lifetime){
            kart.AddKartModifier(boostStats);
            yield return new WaitForSeconds(lifetime);
            kart.RemoveKartModifier(boostStats);
        }

    }
}
