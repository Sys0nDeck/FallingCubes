using UnityEngine;

namespace FallingCubes
{
    public class ColorSwitcher : MonoBehaviour
    {
        [SerializeField] private PlatformDetector _detector;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _hittedColor;

        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void Start()
        {
            _renderer.material.color = _defaultColor;
        }

        public void OnEnable()
        {
            _renderer.material.color = _defaultColor;
            _detector.Hitted += Switch;
        }

        public void OnDisable()
        {
            _detector.Hitted -= Switch;
        }

        private void Switch()
        {
            _renderer.material.color = _hittedColor;
        }
    }
}
