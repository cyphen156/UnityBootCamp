using System.Collections;
using UnityEngine;

namespace _25_06_04_AR_First.ChunkSlide
{
    /// <summary>
    /// Chunk �ִϸ��̼��� ����ϴ� Ŭ����
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
        /// ������ �ٿ� �� �̵� �� ������ �� �ִϸ��̼� ����
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

