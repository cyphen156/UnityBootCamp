using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _25_06_04_AR_First.ChunkSlide
{
    /// <summary>
    /// ���콺 �������� �����Ӹ��� �����ϰ� �����ڿ��� �����ϴ� ������Ʈ
    /// </summary>
    public class MousePositionTracker : MonoBehaviour
    {
        public InputAction mousePositionAction;

        private Vector2 _currentMousePosition;
        private Vector2 _oldMousePosition;

        public bool isReady = false;

        public event Action<Vector2> OnMousePositionChanged;

        private void OnEnable()
        {
            mousePositionAction.Enable();
        }

        private void OnDisable()
        {
            mousePositionAction.Disable();
        }

        public void Start()
        {
            // �ʱ� ���콺 ������ ����
            _currentMousePosition = mousePositionAction.ReadValue<Vector2>();
            _oldMousePosition = _currentMousePosition;

            // Ȱ��ȭ ���·� ����
            isReady = true;
            OnMousePositionChanged?.Invoke(_currentMousePosition);
        }
        private void Update()
        {
            _currentMousePosition = mousePositionAction.ReadValue<Vector2>();

            // ���콺 �������� ������� �ʾҴٸ� �ƹ��͵� ���� ����
            if (_oldMousePosition == _currentMousePosition)
            {
                return;
            }

            _oldMousePosition = _currentMousePosition;
            OnMousePositionChanged?.Invoke(_currentMousePosition);
        }
    }
}
