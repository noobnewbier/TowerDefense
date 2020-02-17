using UnityEngine;

namespace Elements.Turret.Animation
{
    public class DissolveShaderController : MonoBehaviour
    {
        private static readonly int DissolveAmount = Shader.PropertyToID("_DissolveAmount");
        private MaterialPropertyBlock[] _propertyBlocks;
        [SerializeField] private Renderer controlledRenderer;

        private void OnEnable()
        {
            InitializePropertyBlocks();
        }

        private void GetCurrentPropertyBlocks()
        {
            var currentIndex = 0;
            for (var i = 0; i < controlledRenderer.materials.Length; i++)
                controlledRenderer.GetPropertyBlock(_propertyBlocks[currentIndex++], i);
        }

        private void InitializePropertyBlocks()
        {
            _propertyBlocks = new MaterialPropertyBlock[controlledRenderer.materials.Length];
            for (var i = 0; i < _propertyBlocks.Length; i++)
                _propertyBlocks[i] = new MaterialPropertyBlock();
        }

        public void SetPropertyBlockDissolveAmount(float dissolveAmount)
        {
            GetCurrentPropertyBlocks();
            
            dissolveAmount = Mathf.Clamp01(dissolveAmount);
            for (var i = 0; i < _propertyBlocks.Length; i++)
            {
                _propertyBlocks[i].SetFloat(DissolveAmount, dissolveAmount);
                controlledRenderer.SetPropertyBlock(_propertyBlocks[i], i);
            }            
        }
    }
}