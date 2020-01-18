using UnityEditor;
using UnityTechRaw.KartAndFPS.Assets.PostProcessing.Editor.Attributes;
using UnityTechRaw.KartAndFPS.Assets.PostProcessing.Editor.Utils;
using UnityTechRaw.KartAndFPS.Assets.PostProcessing.Runtime.Models;

namespace UnityTechRaw.KartAndFPS.Assets.PostProcessing.Editor.Models
{
    using Settings = FogModel.Settings;

    [PostProcessingModelEditor(typeof(FogModel), alwaysEnabled: true)]
    public class FogModelEditor : PostProcessingModelEditor
    {
        SerializedProperty m_ExcludeSkybox;

        public override void OnEnable()
        {
            m_ExcludeSkybox = FindSetting((Settings x) => x.excludeSkybox);
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("This effect adds fog compatibility to the deferred rendering path; actual fog settings should be set in the Lighting panel.", MessageType.Info);
            EditorGUILayout.PropertyField(m_ExcludeSkybox, EditorGUIHelper.GetContent("Exclude Skybox (deferred only)"));
            EditorGUI.indentLevel--;
        }
    }
}
