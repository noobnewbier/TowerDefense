using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.ScriptableReference;

namespace Ui.Hud.Resource
{
    [CreateAssetMenu(menuName = "ScriptableFactory/FloatTextPresenter")]
    public class FloatTextPresenterFactory : ScriptableObject
    {
        [FormerlySerializedAs("resourceFloat")] [SerializeField]
        private RuntimeFloat runtimeFloat;

        public IFloatTextPresenter CreatePresenter(IFloatTextView view, string prefix)
        {
            return new FloatTextPresenter(view, runtimeFloat, prefix);
        }
    }
}