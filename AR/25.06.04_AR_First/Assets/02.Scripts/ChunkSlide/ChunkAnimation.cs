using System.Collections;
using UnityEngine;

namespace _25_06_04_AR_First.ChunkSlide
{
    /// <summary>
    /// Chunk 애니메이션을 담당하는 클래스
    /// </summary>
    /// 
    public class ChunkAnimation : MonoBehaviour
    {
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        /// <summary>
        /// 스케일 다운 → 이동 → 스케일 업 애니메이션 실행
        /// </summary>
        public void PlayTransition(Vector2 targetAnchoredPos, float duration = 0.2f)
        {
            StopAllCoroutines();
            StartCoroutine(TransitionRoutine(targetAnchoredPos, duration));
        }

        private IEnumerator TransitionRoutine(Vector2 targetPos, float duration)
        {
            yield return StartCoroutine(Scale(Vector3.one, Vector3.zero, duration));

            _rectTransform.anchoredPosition = targetPos;

            yield return StartCoroutine(Scale(Vector3.zero, Vector3.one, duration));
        }

        private IEnumerator Scale(Vector3 start, Vector3 target, float duration)
        {
            float currentTime = 0f;
            _rectTransform.localScale = start;

            while (currentTime < duration)
            {
                _rectTransform.localScale = Vector3.Lerp(start, target, currentTime / duration);
                currentTime += Time.deltaTime;
                yield return null;
            }

            _rectTransform.localScale = target;
        }
    }
}

