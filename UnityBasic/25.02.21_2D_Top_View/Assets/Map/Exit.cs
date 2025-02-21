using UnityEngine;

public enum ExitDirection
{
    right, 
    left, 
    down, 
    up
}

public class Exit : MonoBehaviour
{
    public string sceneName;
    public ExitDirection doorNumber;
    public ExitDirection direction = ExitDirection.down;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RoomManager.ChangeScene(sceneName, doorNumber);
    }
}
