using System;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static ExitDirection siDoorNumber;

    public static void ChangeScene(string sceneName, ExitDirection doorNumber)
    {
        siDoorNumber = doorNumber;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] enters = GameObject.FindGameObjectsWithTag("Exit");


        for (int i = 0; i < enters.Length; ++i)
        {
            var door = enters[i];
            var exit = door.GetComponent<Exit>();

            if (siDoorNumber == exit.doorNumber)
            {
                float x = door.transform.position.x;
                float y = door.transform.position.y;

                // 방향에 따른 좌표 위치 설정
                switch(exit.doorNumber)
                {
                    case ExitDirection.up:
                        y += 1;
                        break;
                    case ExitDirection.down:
                        y = -1;
                        break;
                    case ExitDirection.left:
                        x -= 1;
                        break;
                    case ExitDirection.right:
                        x += 1;
                        break;
                    
                }

                //var player = GameObject;

            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
