using System;
using System.Collections.Generic;
using System.Linq;
using Common.Class;
using Common.Class.ObjectDrawer;
using Common.Enum;
using UnityEngine;
using UnityEngine.Serialization;

namespace AgentAi.Suicidal
{
    [CreateAssetMenu(menuName = "AIConfig/EnvironmentDrawConfig")]
    public class EnvironmentDrawConfig : ScriptableObject
    {
        [SerializeField] private CategoryAndConfig[] categoryAndConfig;
        [SerializeField] private bool grayScale;

        public IDictionary<InterestCategory, Color> CategoryAndColors { get; private set; }
        public IDictionary<InterestCategory, int> CategoryAndPriority { get; private set; }
        public IDictionary<InterestCategory, Drawer> CategoryAndDrawer { get; private set; }
        public bool GrayScale => grayScale;

        private void OnEnable()
        {
            CategoryAndColors = categoryAndConfig.ToDictionary(i => i.Category, i => i.Color);
            CategoryAndPriority = categoryAndConfig.ToDictionary(
                i => i.Category,
                i => categoryAndConfig.Length - Array.IndexOf(categoryAndConfig, i)
            );
            CategoryAndDrawer = categoryAndConfig.ToDictionary(i => i.Category, i => i.Drawer);
        }

        [Serializable]
        private struct CategoryAndConfig
        {
            [SerializeField] private InterestCategory category;
            [SerializeField] private Color color;
            [SerializeField] private Drawer drawer;
            public InterestCategory Category => category;
            public Drawer Drawer => drawer;
            public Color Color => color;
        }
    }
}