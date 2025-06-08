using System;
using UnityEngine;

namespace _25_06_04_AR_First.Services.GPS
{
    /// <summary>
    /// Interface for location provider services.
    /// </summary>
    public interface ILocationProvider
    {
        /// <summary>
        /// ����
        /// </summary>
        double latitude { get; }
        
        /// <summary>
        /// �浵
        /// </summary>
        double longitude { get; }

        /// <summary>
        /// ��
        /// </summary>
        double altitude { get; }

        /// <summary>
        /// Latitude
        /// Longitude
        /// Altitude
        /// Accuracy
        /// Timestamp
        /// </summary>
        event Action<double, double, double, float, double> OnLocationChanged;

        void StartService();
        void StopService();
    }
}