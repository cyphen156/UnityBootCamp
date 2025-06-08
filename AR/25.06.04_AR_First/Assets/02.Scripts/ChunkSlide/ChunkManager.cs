using _25_06_04_AR_First.ChunkSlide;
using System;
using System.Collections;
using UnityEngine;

namespace _25_06_04_AR_First.ChunkSlide
{
    /// <summary>
    /// Chunk 积己, 盎脚, 力芭 殿狼 包府
    /// </summary>
}
public class ChunkManager : MonoBehaviour
{
    [Header("Chunk")]
    [SerializeField] public GameObject _chunkPrefab;

    [Header("Configuration")]
    [SerializeField] public Transform _chunkParent;

    private GameObject[,] _chunks;
    [SerializeField] private int _chunkCount = 3;
    [SerializeField] private float _chunkSize = 100f;

    [SerializeField] private Vector2 _chunkOffset;

    public float ChunkSize => _chunkSize;   

    public void InitializeChunks(Vector2 centerPos)
    {
        _chunkOffset = centerPos;
        int offset = _chunkCount / 2;
        _chunks = new GameObject[_chunkCount, _chunkCount];

        for (int i = 0; i < _chunkCount; i++)
        {
            for (int j = 0; j < _chunkCount; j++)
            {
                int dx = j - offset;
                int dy = i - offset;

                GameObject chunk = Instantiate(_chunkPrefab, _chunkParent);
                RectTransform rect = chunk.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(dx * _chunkSize, dy * _chunkSize) + _chunkOffset;

                chunk.name = $"Chunk_{dy}_{dx}";
                _chunks[i, j] = chunk;
            }
        }
    }

    public void SlideChunks(Vector2Int direction)
    {
        
    }
}
