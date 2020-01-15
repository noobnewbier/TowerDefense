using Experimental;
using UnityEngine;

namespace Ui.Hud.Resource
{
    [CreateAssetMenu(menuName = "ScriptableFactory/ResourcePresenter")]
    public class ResourcePresenterFactory : ScriptableObject
    {
        [SerializeField] private RuntimeFloat resourceFloat;

        public IResourcePresenter CreatePresenter(IResourceView view)
        {
            return  new ResourcePresenter(view, resourceFloat);
        }
    }
}