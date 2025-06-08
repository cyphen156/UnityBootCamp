using _25_06_04_AR_First.Services.GPS;
using System;
using System.Collections;
using UnityEngine;

// Namespace: _25_06_04_AR_First.ChunkSlide
namespace _25_06_04_AR_First.ChunkSlide
{
    /// <summary>
    /// Chunk �����̴��� ��Ʈ�ѷ�
    /// �� ������Ʈ���� �������� ����
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

            yield return new WaitForSeconds(0.1f); // �ʱ�ȭ �������� ���� ������

            // ���콺 ������ Ʈ��Ŀ�� �̺�Ʈ ����
            _mousePositionTracker.OnMousePositionChanged += HandleMousePositionChanged;

            // �ʱ� ���콺 �������� ChunkManager�� ���� �� ����
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
