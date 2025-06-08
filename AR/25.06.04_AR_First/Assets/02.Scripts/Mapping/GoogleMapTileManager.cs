using _25_06_04_AR_First.Mapping;
using _25_06_04_AR_First.Services.GoogleMaps;
using System;
using System.Collections;
using UnityEngine;

namespace _25_06_04_AR_First.Services.GPS
{
    /// <summary>
    /// Maptile ����, ����, ���� ���� ����
    /// GPS �����Ͱ� ��� �� Ÿ�ϸ� Ȯ�� �� �ݴ���� Ÿ�ϸ� ����
    /// </summary>
    public class GoogleMapTileManager : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] GoogleStaticMapService _googleStaticMapService; // ���� �� ���� �̹��� ����
        [SerializeField] GPSLocationService _gpsLocationService; // GPS ��ġ ����
        [SerializeField] GoogleMapTile _mapTilePrefab;
        [SerializeField] Transform _mapTileParent;

        [Header("Debug")]
        Vector2Int _currentCenterTile;

        [Header("")]
        GoogleMapTile[,] _mapTiles = new GoogleMapTile[3, 3];
        readonly int[] TILE_OFFSETS = new int[] { -1, 0, 1 };

        public bool isInitialized { get; private set; }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => _gpsLocationService.isReady);
            InitializeTiles();
            isInitialized = true;
        }

        private void InitializeTiles()
        {
            _currentCenterTile = CalcTileCoordinate(_gpsLocationService.mapCenter);
            CreateTiles(_currentCenterTile);
        }

        void CreateTiles(Vector2Int center)
        {
            // �߽� �ε����� �������� ��� ���� Ÿ�� �ε��� ���
            for (int i = 0; i < TILE_OFFSETS.Length; ++i)
            {
                for (int j = 0; j < TILE_OFFSETS.Length; ++j)
                {
                    Vector2Int coord = new Vector2Int(
                        center.x + TILE_OFFSETS[i],
                        center.y + TILE_OFFSETS[i]);

                    GoogleMapTile tile = Instantiate(_mapTilePrefab, _mapTileParent);
                    tile.tileOffset = coord;
                    tile.googleStaticMapService = _googleStaticMapService;
                    tile.gpsLocationService = _gpsLocationService;
                    tile.zoomLevel = _gpsLocationService.mapTileZoomLevel;
                    tile.name = $"MapTile_{coord.x}_{coord.y}";
                    tile.transform.position = CalcWorldPosition(coord);
                    tile.RefreshMapTile();
                    _mapTiles[i, j] = tile;
                }
            }
        }

        public Vector3 GetCenterTileWorldPosition()
        {
            return CalcWorldPosition(_currentCenterTile);
        }

        private float CalcWorldPositionSpacing(int zoomLevel)
        {
            float delta = zoomLevel - 15f;
            return 30f * Mathf.Pow(0.5f, delta);
        }

        /// <summary>
        /// Ÿ�� �ε����� ���� ���� ������ ����
        /// </summary>
        /// <param name="coord"> Ÿ�� �ε���</param>
        /// <returns> ���� ��ġ </returns>
        Vector3 CalcWorldPosition(Vector2Int coord)
        {
            float spacing = CalcWorldPositionSpacing(_gpsLocationService.mapTileZoomLevel);
            return new Vector3(coord.x+spacing, 0f, coord.y + spacing); // Y���� ���̰����� ����
        }

        /// <summary>
        /// ���� GPS ��ġ�� �������� �߽� Ÿ�� �ε��� ���
        /// 3X3 �迭�� Mapile ����
        /// </summary>
        /// <param name="center"></param>
        /// <returns></returns>
        Vector2Int CalcTileCoordinate(MapLocation center)
        {
            // �޸�ī�丣 �ȼ� ��ǥ
            int pixelX21 = GoogleMapUtils.LonToX(center.longitude);
            int pixelY21 = GoogleMapUtils.LatToY(center.latitude);

            // Google map Zoomlevel 1�� 2�辿 ���� �����ϹǷ�,
            // �� ���� ���� ��ŭ ���������� Bit-Shift ������ ���� ���ϴ� �ȼ� ��ǥ�� ���
            int shift = 21 - _mapTilePrefab.zoomLevel; // �� ������ ���� ����Ʈ ���

            int pixelX = pixelX21 >> shift; // �� ������ ���� �ȼ� X ��ǥ
            int pixelY = pixelY21 >> shift; // �� ������ ���� �ȼ� Y ��ǥ

            // MapTile �� �ȼ����� ������ �ε��� ���� �� ����

            return new Vector2Int(
                Mathf.RoundToInt(pixelX / (float)_gpsLocationService.mapTileSizePixels), // Ÿ�� ũ��� ������ Ÿ�� �ε��� ���
                Mathf.RoundToInt(pixelY / (float)_gpsLocationService.mapTileSizePixels));
        }
    }
}