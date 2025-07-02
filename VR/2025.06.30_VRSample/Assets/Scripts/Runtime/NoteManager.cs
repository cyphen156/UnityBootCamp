using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] GameObject _notePrefab;
    [SerializeField] Vector2 _spawnRange = new(1f, 1f);
    [SerializeField] Vector2 _spawnRangeOffset = new(0f, 1f);
    [SerializeField] float _spawnDelay = 5f;
    [SerializeField] float _deadlineZ = -1f;
    [SerializeField] NoteDissolveAnimation _noteDissolveAnimation;
    List<Transform> _cachedDeadNotes;
    List<float> _peaks;
    Dictionary<Transform, float> _noteTable;
    AudioSource _audioSource;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _noteTable = new Dictionary<Transform, float>(); // TODO : Reserving , 총 노트수랑 배속 고려해서 대충 몇개정도 노트가 맵에서 움직이고있을지 가늠해서 쓰면됨.
        _cachedDeadNotes = new List<Transform>();
    }


    private void Start()
    {
        _peaks = GameManager.gameSession.selectedSongSpec.peaks;
        Play();
    }

    public void Play()
    {
        StartCoroutine(C_PlayAudio());
        StartCoroutine(C_SpawnNotes());
        StartCoroutine(C_MoveNotes());
    }

    IEnumerator C_PlayAudio()
    {
        yield return new WaitForSeconds(_spawnDelay);
        _audioSource.Play();
    }

    IEnumerator C_SpawnNotes()
    {
        int index = 0;
        float playTime = _audioSource.clip.length;
        float playSpeed = GameManager.gameSession.playSpeed;

        while (index < _peaks.Count)
        {
            // 현재 프레임에서 생성할수있는 노트 다 생성
            while (index < _peaks.Count)
            {
                if (_audioSource.time >= _peaks[index])
                {
                    GameObject note = Instantiate(_notePrefab);
                    int x = Random.Range(-1, 2); // length 2 m
                    int y = Random.Range(-1, 2); // length 2 m
                    note.transform.position = new Vector3(x * _spawnRange.x / 2f + _spawnRangeOffset.x,
                                                          y * _spawnRange.y / 2f + _spawnRangeOffset.y,
                                                          _peaks[index] * playSpeed + _spawnDelay * playSpeed);
                    _noteTable.Add(note.transform, _peaks[index]);
                    index++;
                }
                else
                {
                    break;
                }
            }

            yield return null;
        }
    }

    IEnumerator C_MoveNotes()
    {
        float playSpeed = GameManager.gameSession.playSpeed;

        while (true)
        {
            _cachedDeadNotes.Clear();

            foreach (KeyValuePair<Transform, float> notePair in _noteTable)
            {
                Vector3 position = notePair.Key.position;
                position.z = (notePair.Value - _audioSource.time + _spawnDelay) * playSpeed;
                notePair.Key.position = position;

                if (position.z < _deadlineZ)
                {
                    // TODO : Dissolve effect coroutine
                    Renderer renderer = notePair.Key.GetComponent<Renderer>();
                    _noteDissolveAnimation.PlayDissolve(renderer,
                                                        () => Destroy(renderer.gameObject));
                    _cachedDeadNotes.Add(notePair.Key);
                }
            }

            for (int i = 0; i < _cachedDeadNotes.Count; i++)
            {
                _noteTable.Remove(_cachedDeadNotes[i]);
            }

            yield return null;
        }
    }
}
