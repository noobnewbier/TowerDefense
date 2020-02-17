using UnityEngine;

namespace Elements.Turret.Animation
{
    [RequireComponent(typeof(Renderer))]
    public class DissolveShaderController : MonoBehaviour
    {
        private static readonly int DissolveAmount = Shader.PropertyToID("_DissolveAmount");
        private MaterialPropertyBlock[] _propertyBlocks;
        private Renderer _controlledRenderer;

        private void OnEnable()
        {
            _controlledRenderer = GetComponent<Renderer>();
            InitializePropertyBlocks();
        }

        private void GetCurrentPropertyBlocks()
        {
            var currentIndex = 0;
            for (var i = 0; i < _controlledRenderer.materials.Length; i++)
                _controlledRenderer.GetPropertyBlock(_propertyBlocks[currentIndex++], i);
        }

        private void InitializePropertyBlocks()
        {
            _propertyBlocks = new MaterialPropertyBlock[_controlledRenderer.materials.Length];
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
                _controlledRenderer.SetPropertyBlock(_propertyBlocks[i], i);
            }            
        }
    }
}