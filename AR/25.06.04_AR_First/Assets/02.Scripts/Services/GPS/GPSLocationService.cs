using _25_06_04_AR_First.Mapping;
using System;
using UnityEngine;

/// there is no Lambda expression in this file, so we can use the old namespace
namespace _25_06_04_AR_First.Services.GPS
{ 
    public class GPSLocationService : MonoBehaviour
    {
        #region Fields
        private ILocationProvider _locationProvider;

        [Header("Simulation Settings (Editor Only)")]
        [SerializeField] bool _isSimulated;
        [SerializeField] Transform _simulationTarget;
        [SerializeField] MapLocation _simulationStartLocation = new MapLocation(37.4946, 127.0276056);

        public MapLocation mapCenter;
        public MapEnvelope mapEnvelope;
        public Vector3 mapWorldCenter;
        public Vector2 mapScale;

        [Header("Map Tile Settings")]
        [Tooltip("맵 타일 스케일")]
        [SerializeField]
        public int mapTilScale { get; private set; } = 1;

        [Tooltip("맵 타일 크기 (픽셀)")]
        [SerializeField]
        public int mapTileSizePixels { get; private set; } = 640;
        
        [Tooltip("맵 타일 Zoom 레벨 (1 ~ 20)")]
        [Range(1, 20)]
        [SerializeField]
        public int mapTileZoomLevel { get; private set; } = 15;

        #endregion

        #region Properties
        public double latitude { get; private set; }
        public double longitude { get; private set; }
        public double altitude { get; private set; }
        public float accuracy { get; private set; }
        public double timestamp { get; private set; }
        public bool isReady { get; private set; }


        #endregion

        #region Events
        public event Action onMapRedraw;

        #endregion

    #region All Methods
        #region Unity Methods
        private void Awake()
        {
#if UNITY_EDITOR
            SimulatedLocationProvider simulatedLocationProvider = gameObject.AddComponent<SimulatedLocationProvider>();
            simulatedLocationProvider.target = _simulationTarget;
            simulatedLocationProvider.startLocation = _simulationStartLocation; 
            _locationProvider = simulatedLocationProvider;
            isReady = true;
#else
            _locationProvider = gameObject.AddComponent<DeviceLocationProvider>();
#endif
        }

        private void OnEnable()
        {
            _locationProvider.OnLocationChanged += OnLocationChanged;
            _locationProvider.StartService();
        }

        private void OnDisable()
        {
            _locationProvider.OnLocationChanged -= OnLocationChanged;
            _locationProvider.StopService();
        }

        #endregion

        #region Custom Methods
        private void OnLocationChanged(double newLatitude
            , double newLongitude
            , double newAltitude
            , float newAccuracy
            , double newTimestamp)
        {
            latitude = newLatitude;
            longitude = newLongitude;
            altitude = newAltitude;
            accuracy = newAccuracy;
            timestamp = newTimestamp;

            if (mapEnvelope.Contains(new MapLocation(latitude, longitude)) == false)
            {
                CenterMap();
            }
        }

        private void CenterMap()
        {
            mapCenter.latitude = latitude;
            mapCenter.longitude = longitude;
            mapWorldCenter.x = GoogleMapUtils.LonToX(mapCenter.longitude);
            mapWorldCenter.y = GoogleMapUtils.LatToY(mapCenter.latitude);

            mapScale.x = (float)GoogleMapUtils.CalculateScaleX(latitude, mapTileSizePixels, mapTilScale, mapTileZoomLevel);
            mapScale.y = (float)GoogleMapUtils.CalculateScaleY(longitude, mapTileSizePixels, mapTilScale, mapTileZoomLevel);

            float lon1 = (float)GoogleMapUtils.AdjustLonByPixels(longitude, -mapTileSizePixels / 2, mapTileZoomLevel);
            float lat1 = (float)GoogleMapUtils.AdjustLatByPixels(latitude, mapTileSizePixels / 2, mapTileZoomLevel);
            float lon2 = (float)GoogleMapUtils.AdjustLonByPixels(longitude, mapTileSizePixels / 2, mapTileZoomLevel);
            float lat2 = (float)GoogleMapUtils.AdjustLatByPixels(latitude, -mapTileSizePixels / 2, mapTileZoomLevel);

            mapEnvelope = new MapEnvelope(lon1, lat1, lon2, lat2);
            onMapRedraw?.Invoke();
        }
        #endregion
        #endregion
    }
}
