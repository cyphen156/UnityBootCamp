using System;
using UnityEngine;

namespace _25_06_04_AR_First.Services.GPS
{
    /// <summary>
    /// Target의 Transform을 기반으로 
    /// Location을 갱신해서 
    /// 시뮬레이션용 데이터를 
    /// 제공하는 클래스입니다.
    /// </summary>
    internal class SimulatedLocationProvider : MonoBehaviour, ILocationProvider
    {
        #region Fields


        /// <summary>
        /// Latitude
        /// Longitude
        /// altitude
        /// Accuracy
        /// TimeStamp
        /// </summary>
        public event Action<double, double, double, float, double> OnLocationChanged;

        private double _metersPerDegreeLatitude = 111320;   // 약 111.32 km (1도 당 단위)
        private float _updateLocationInterval = 0.1f; // 위치 갱신 간격 (0.1초 단위)
        private float _updatedTimeMark; // 마지막으로 갱신된 시간
        private bool _isRunning = false;    // 갱신 동작 여부
        private Vector3 _prevTargetPosition; // 이전 프레임 타겟 위치
        const float MIN_MOVE_DISTANCE = 0.01f;   // 최소 이동 거리 (미터 단위)

        #endregion

        #region Properties
        public Transform target { get; set; }
        public MapLocation startLocation { get; set; }
        public double latitude { get; private set; }
        public double longitude { get; private set; }
        public double altitude { get; private set; }

        #endregion

    #region Methods
        #region Unity Methods
        
        private void Update()
        {
            // 갱신 동작이 비활성화되었다면 루프 종료
            if (!_isRunning)
            {
                return;
            }

            // 갱신 간격이 지나지 않았다면 루프 종료
            if (Time.time - _updatedTimeMark < _updateLocationInterval)
            {
                return;
            }

            // 만약 최소 거리이상 움직였다면 위치를 갱신
            if (Vector3.Distance(_prevTargetPosition, target.position) > MIN_MOVE_DISTANCE)
            {
                UpdateLocation();
                _prevTargetPosition = target.position;
            }
        
            _updatedTimeMark = Time.time; // 갱신 시간 업데이트
        }

        #endregion

        #region Custom Methods
        public void StartService()
        {
            if (target == null)
            {
                Debug.LogError("GPS 시뮬레이션 대상이 없음");
                Debug.Assert(true);
            }

            _isRunning = true;
            _prevTargetPosition = target.position;
            _updatedTimeMark = Time.time;
            UpdateLocation();
            Debug.Log("GPS 시뮬레이션 시작");

        }

        public void StopService()
        {
            _isRunning = false;

            Debug.Log("GPS 시뮬레이션 종료");
        }
        /// <summary>
        /// Target의 위치를 기반으로
        /// 위도, 경도를 갱신합니다.
        /// </summary>
        private void UpdateLocation()
        {
            if (target == null)
            {
                return;
            }
           
            Vector3 currentPosition = target.position;

            double meterTiDegreeLattitude = 1f / _metersPerDegreeLatitude;
            double meterTiDegreeLongitude = 1f / (_metersPerDegreeLatitude * Math.Cos(
                                                startLocation.latitude * Mathf.Deg2Rad));
            
            double deltaLatitude =  currentPosition.z * meterTiDegreeLattitude;
            double deltaLongitude = currentPosition.x * meterTiDegreeLongitude;
        
            double newLatitude = startLocation.latitude + deltaLatitude;
            double newLongitude = startLocation.longitude + deltaLongitude;

            OnLocationChanged?.Invoke(
                newLatitude,
                newLongitude,
                0f,                         // Altitude는 시뮬레이션이므로 0으로 설정
                1f,                         // Accuracy는 시뮬레이션이므로 1으로 설정
                DateTime.Now.Ticks          // Timestamp는 현재 시간으로 설정
            );
            Debug.Log("GPS 시뮬레이션 갱신됨");
        }
        #endregion
        #endregion
    }
}
