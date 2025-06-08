using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _25_06_04_AR_First.Services.GPS
{
    public class PlayerController : MonoBehaviour
    {
        // 뉴 인풋 시스템을 사용하여 플레이어의 이동을 제어합니다.
        [SerializeField] InputActionReference _moveInputAction;
        [SerializeField] GoogleMapTileManager _mapTileManager;

#if UNITY_EDITOR
        public Vector3 velocity;
        public Vector3 direction; 
        public float speed = 5f;
#endif

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => _mapTileManager.isInitialized);
            transform.position = _mapTileManager.GetCenterTileWorldPosition();
        }


        public void OnEnable()
        {
            _moveInputAction.action.performed += OnMovePerformed;
            _moveInputAction.action.canceled += OnMoveCancled;
            _moveInputAction.action.Enable();
        }

        public void OnDisable()
        {
            _moveInputAction.action.performed -= OnMovePerformed;
            _moveInputAction.action.canceled -= OnMoveCancled;
            _moveInputAction.action.Disable();
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            direction = new Vector3(input.x, 0, input.y).normalized;
        }

        private void OnMoveCancled(InputAction.CallbackContext context)
        {
            velocity = Vector3.zero;
        }

        private void FixedUpdate()
        {
            if (velocity.sqrMagnitude > speed)
            {
                velocity = direction * speed;
                transform.Translate(velocity * Time.fixedDeltaTime, Space.World);
            }
        }
    }
}