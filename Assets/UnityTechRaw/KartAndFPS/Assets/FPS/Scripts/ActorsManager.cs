using System.Collections.Generic;
using UnityEngine;

namespace UnityTechRaw.KartAndFPS.Assets.FPS.Scripts
{
    public class ActorsManager : MonoBehaviour
    {
        public List<Actor> actors { get; private set; }

        private void Awake()
        {
            actors = new List<Actor>();
        }
    }
}
