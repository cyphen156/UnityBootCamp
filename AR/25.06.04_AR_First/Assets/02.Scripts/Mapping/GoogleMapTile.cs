using _25_06_04_AR_First.Services.GoogleMaps;
using _25_06_04_AR_First.Services.GPS;
using UnityEngine;

namespace _25_06_04_AR_First.Mapping
{
    public class GoogleMapTile : MonoBehaviour
    {
        [Header("Map Settings")]
        [Tooltip("줌 레벨")]
        [Range(1, 20)]
        public int zoomLevel = 15;

        [Tooltip("맵 텍스쳐 사이즈")]
        [Range(64, 1024)]
        public int size = 640;

        [Tooltip("월드 맵 원점")]
        public MapLocation worldCenterLocation;

        [Header("Tile Settings")]
        [Tooltip("타일링을 위한 오프셋")]
        public Vector2Int tileOffset;

        [Tooltip("오프셋 적용한 맵의 중심 위치")]
        public MapLocation tileCenterLocation;

        [Header("Map Services")]
        public GoogleStaticMapService googleStaticMapService;

        [Header("GPS Services")]
        public GPSLocationService gpsLocationService
        {
            get => _gpsLocationService;
            set
            {
                if (value == _gpsLocationService)
                    return;

                if (value != null)
                {
                    if (_gpsLocationService != null)
                    {
                        _gpsLocationService.onMapRedraw -= RefreshMapTile;
                    }

                    value.onMapRedraw += RefreshMapTile;
                }

                _gpsLocationService = value;
            }
        }

        private GPSLocationService _gpsLocationService;

        private Renderer _renderer;


        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        public void RefreshMapTile()
        {
            // 오프셋에따른 중심위치 계산
            tileCenterLocation.latitude = GoogleMapUtils.AdjustLatByPixels(
                worldCenterLocation.latitude,
                (int)(size * tileOffset.y),
                zoomLevel);

            tileCenterLocation.longitude = GoogleMapUtils.AdjustLonByPixels(
                worldCenterLocation.longitude,
                (int)(size * tileOffset.x),
                zoomLevel);

            // 맵 텍스쳐 요청
            googleStaticMapService.LoadMap(tileCenterLocation.latitude,
                                           tileCenterLocation.longitude,
                                           zoomLevel,
                                           new Vector2(size, size),
                                           OnMapLoaded);
        }

        private void OnMapLoaded(Texture2D texture)
        {
            if (_renderer.material.mainTexture != null)
                Destroy(_renderer.material.mainTexture);

            _renderer.material.mainTexture = texture;
        }
    }
}
