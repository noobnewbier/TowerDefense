using UnityEditor;
using UnityTechRaw.KartAndFPS.Assets.PostProcessing.Editor.Attributes;
using UnityTechRaw.KartAndFPS.Assets.PostProcessing.Runtime.Models;

namespace UnityTechRaw.KartAndFPS.Assets.PostProcessing.Editor.Models
{
    [PostProcessingModelEditor(typeof(DitheringModel))]
    public class DitheringModelEditor : PostProcessingModelEditor
    {
        public override void OnInspectorGUI()
        {
            if (profile.grain.enabled && target.enabled)
                EditorGUILayout.HelpBox("Grain is enabled, you probably don't need dithering !", MessageType.Warning);
            else
                EditorGUILayout.HelpBox("Nothing to configure !", MessageType.Info);
        }
    }
}
