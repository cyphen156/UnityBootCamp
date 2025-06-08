using _25_06_04_AR_First.Services.GoogleMaps;
using _25_06_04_AR_First.Services.GPS;
using System;
using UnityEngine;

namespace _25_06_04_AR_First.Mapping
{
    public class GoogleMapTile : MonoBehaviour
    {
        [Header("Map Settings")]
        [Range(1, 20)] //즘레벨 설정
        public int zoomLevel = 15; // Zoom level of the map tile

        [Range(64, 1024)]
        [Tooltip("타일 크기")]
        public int size = 640; // Size of the tile in pixels

        [Tooltip("월드 맵 원점")]
        public MapLocation worldCenterLocation;

        [Header("Tile Settings")]
        [Tooltip("타일의 오프셋")]
        public Vector2 tileOffset;

        [Tooltip("오프셋을 적용한 맵의 중심 위치")]
        public MapLocation tileCenterLocation;

        [Header("Map Services")]
        public GoogleStaticMapService googleStaticMapService;

        [Header("GPS Services")]
        public GPSLocationService gpsLocationService
        {
            get => gpsLocationService;
            set
            {
                if (value != null)
                {
                    if (gpsLocationService != null)
                    {
                        gpsLocationService.onMapRedraw -= RefreshMapTile;
                    }
                    value.onMapRedraw += RefreshMapTile;
                }
            }
        }

        private MeshRenderer _renderer;

        public void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public void Start()
        {
            RefreshMapTile();
        }

        public void RefreshMapTile()
        {
            tileCenterLocation.latitude = GoogleMapUtils.AdjustLatByPixels(
                worldCenterLocation.latitude,
                (int)(size * tileOffset.y),
                zoomLevel);
            tileCenterLocation.longitude = GoogleMapUtils.AdjustLatByPixels(
                worldCenterLocation.longitude,
                (int)(size * tileOffset.x),
                zoomLevel);

            googleStaticMapService.LoadMap(
                tileCenterLocation.latitude, 
                tileCenterLocation.longitude, 
                zoomLevel, 
                new Vector2(size, size), 
                OnMapLoaded);
        }

        private void OnMapLoaded(Texture2D texture)
        {
            if (_renderer.material.mainTexture != null)
            {
                Destroy(_renderer.material.mainTexture);
            }
         
            _renderer.material.mainTexture = texture;
        }
    }
}
