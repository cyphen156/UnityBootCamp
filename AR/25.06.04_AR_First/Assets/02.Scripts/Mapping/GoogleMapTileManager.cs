using _25_06_04_AR_First.Mapping;
using _25_06_04_AR_First.Services.GoogleMaps;
using System;
using System.Collections;
using UnityEngine;

namespace _25_06_04_AR_First.Services.GPS
{
    /// <summary>
    /// Maptile 생성, 갱신, 제거 등의 관리
    /// GPS 데이터가 벗어날 때 타일맵 확장 및 반대방향 타일맵 삭제
    /// </summary>
    public class GoogleMapTileManager : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] GoogleStaticMapService _googleStaticMapService; // 구글 맵 정적 이미지 서비스
        [SerializeField] GPSLocationService _gpsLocationService; // GPS 위치 서비스
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
            // 중심 인덱스를 기준으로 모든 방향 타일 인덱스 계산
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
        /// 타일 인덱스로 게임 월드 포지션 산출
        /// </summary>
        /// <param name="coord"> 타일 인덱스</param>
        /// <returns> 월드 위치 </returns>
        Vector3 CalcWorldPosition(Vector2Int coord)
        {
            float spacing = CalcWorldPositionSpacing(_gpsLocationService.mapTileZoomLevel);
            return new Vector3(coord.x+spacing, 0f, coord.y + spacing); // Y축은 높이값으로 설정
        }

        /// <summary>
        /// 현재 GPS 위치를 기준으로 중심 타일 인덱스 계산
        /// 3X3 배열로 Mapile 생성
        /// </summary>
        /// <param name="center"></param>
        /// <returns></returns>
        Vector2Int CalcTileCoordinate(MapLocation center)
        {
            // 메르카토르 픽셀 좌표
            int pixelX21 = GoogleMapUtils.LonToX(center.longitude);
            int pixelY21 = GoogleMapUtils.LatToY(center.latitude);

            // Google map Zoomlevel 1당 2배씩 값이 증가하므로,
            // 줌 레벨 차이 만큼 오른쪽으로 Bit-Shift 연산을 통해 원하느 픽셀 좌표를 계산
            int shift = 21 - _mapTilePrefab.zoomLevel; // 줌 레벨에 따른 시프트 계산

            int pixelX = pixelX21 >> shift; // 줌 레벨에 따른 픽셀 X 좌표
            int pixelY = pixelY21 >> shift; // 줌 레벨에 따른 픽셀 Y 좌표

            // MapTile 당 픽셀수로 나누면 인덱스 구할 수 있음

            return new Vector2Int(
                Mathf.RoundToInt(pixelX / (float)_gpsLocationService.mapTileSizePixels), // 타일 크기로 나누어 타일 인덱스 계산
                Mathf.RoundToInt(pixelY / (float)_gpsLocationService.mapTileSizePixels));
        }
    }
}