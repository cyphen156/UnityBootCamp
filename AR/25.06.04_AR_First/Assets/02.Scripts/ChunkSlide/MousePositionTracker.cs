using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _25_06_04_AR_First.ChunkSlide
{
    /// <summary>
    /// 마우스 포지션을 프레임마다 갱신하고 구독자에게 전달하는 컴포넌트
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
            // 초기 마우스 포지션 설정
            _currentMousePosition = mousePositionAction.ReadValue<Vector2>();
            _oldMousePosition = _currentMousePosition;

            // 활성화 상태로 설정
            isReady = true;
            OnMousePositionChanged?.Invoke(_currentMousePosition);
        }
        private void Update()
        {
            _currentMousePosition = mousePositionAction.ReadValue<Vector2>();

            // 마우스 포지션이 변경되지 않았다면 아무것도 하지 않음
            if (_oldMousePosition == _currentMousePosition)
            {
                return;
            }

            _oldMousePosition = _currentMousePosition;
            OnMousePositionChanged?.Invoke(_currentMousePosition);
        }
    }
}
