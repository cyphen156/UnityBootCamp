using _25_06_04_AR_First.Services.GPS;
using System;
using System.Collections;
using UnityEngine;

// Namespace: _25_06_04_AR_First.ChunkSlide
namespace _25_06_04_AR_First.ChunkSlide
{
    /// <summary>
    /// Chunk 슬라이더의 컨트롤러
    /// 각 컴포넌트간의 의존성을 관리
    /// </summary>
    public class ChunkSliderController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private MousePositionTracker _mousePositionTracker;
        [SerializeField] private ChunkManager _chunkManager;

        [Header("Mouse Info")]
        [SerializeField] private Vector2 _currentMousePosition;

        [Header("Chunk Info")]
        [SerializeField] private Vector2Int _currentCenterCoord;

        IEnumerator Start()
        {
            yield return new WaitUntil(() => _mousePositionTracker.isReady);

            yield return new WaitForSeconds(0.1f); // 초기화 안정성을 위한 딜레이

            // 마우스 포지션 트래커의 이벤트 구독
            _mousePositionTracker.OnMousePositionChanged += HandleMousePositionChanged;

            // 초기 마우스 포지션을 ChunkManager에 전달 및 생성
            _chunkManager.InitializeChunks(_currentCenterCoord);
            
        }

        private void HandleMousePositionChanged(Vector2 newMousePosition)
        {
            _currentMousePosition = newMousePosition;

            Vector2Int newCoord = GetChunkCoordFromPosition(_currentMousePosition);
            if (newCoord != _currentCenterCoord)
            {
                Vector2Int direction = newCoord - _currentCenterCoord;
                _chunkManager.SlideChunks(direction);
                _currentCenterCoord = newCoord;
            }
        }

        private Vector2Int GetChunkCoordFromPosition(Vector2 pos)
        {
            float size = _chunkManager.ChunkSize;
            int x = Mathf.FloorToInt(pos.x / size);
            int y = Mathf.FloorToInt(pos.y / size);
            return new Vector2Int(x, y);
        }
    }
}
