

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace _25_06_04_AR_First.Services.GoogleMaps
{
    public class GoogleStaticMapService : MonoBehaviour
    {
        private const string BASE_URL = "https://maps.googleapis.com/maps/api/staticmap?";
        private const string API_KEY = "AIzaSyB__gBIDgXhalA4O0MeZRSG_lw0sXt1bMo";
        private Texture2D _cachedTexture;

        public void LoadMap(double latitude, double longitude, float zoom, Vector2 size,
            Action<Texture2D> onComplete)
        {
            StartCoroutine(C_LoadMap(latitude, longitude, zoom, size, onComplete));
        }

        IEnumerator C_LoadMap(double latitude, double longitude, float zoom, Vector2 size,
            Action<Texture2D> onComplete)
        {
            string url = 
                BASE_URL + 
                "center=" + latitude + "," + longitude + 
                "&Zoom=" + zoom + 
                "&size=" + size.x + "x" + size.y + 
                "&key=" + API_KEY;

            Debug.Log($"[{nameof(GoogleStaticMapService)}] :: Request Map Texture ... {url}");

            url = UnityWebRequest.UnEscapeURL(url);

            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

            yield return request.SendWebRequest();

            _cachedTexture =  DownloadHandlerTexture.GetContent(request);

            onComplete?.Invoke(_cachedTexture);
        }
    }
}
