#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class GameSessionWindow : EditorWindow
{
    [MenuItem("Window/GameSession")]
    static void Open() => GetWindow<GameSessionWindow>("Game Session");

    GameSession _cachedTarget;
    SerializedObject _cachedTargetSerialized;


    private void OnEnable()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }


    void OnPlayModeStateChanged(PlayModeStateChange change)
    {
        _cachedTarget = GameManager.gameSession;
        _cachedTargetSerialized = new SerializedObject(_cachedTarget);
        _cachedTargetSerialized.Update();
        Repaint();
    }


    private void OnGUI()
    {
        // Play 모드에서만 감시가능
        if (!Application.isPlaying)
        {
            EditorGUILayout.HelpBox("Only can monitor game session on Play mode.", MessageType.Warning);
            return;
        }

        if (_cachedTarget == null)
        {
            if (GameManager.gameSession != null)
            {
                _cachedTarget = GameManager.gameSession;
                _cachedTargetSerialized = new SerializedObject(_cachedTarget);
            }
            else
            {
                EditorGUILayout.HelpBox("Game session is not instantiated yet.", MessageType.Warning);
                return;
            }
        }

        _cachedTargetSerialized.Update();
        EditorGUILayout.PropertyField(_cachedTargetSerialized.FindProperty("selectedSongSpec"));
        EditorGUILayout.PropertyField(_cachedTargetSerialized.FindProperty("playSpeed"));
        _cachedTargetSerialized.ApplyModifiedProperties();
    }
}
#endif