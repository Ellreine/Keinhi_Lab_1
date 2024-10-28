using Mirror;
using UnityEngine;

namespace MirrorProjectTPU.Lab1.Scripts
{
    public class RandomColor : NetworkBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        
        [SyncVar(hook = nameof(OnChangeColor))]
        private Color _color = Color.white;
        
        public override void OnStartClient()
        {
            base.OnStartClient();
            _color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }

        private void OnChangeColor(Color oldColor, Color newColor)
        {
            meshRenderer.material.color = newColor;
        }
    }
}
