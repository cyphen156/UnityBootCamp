using System.Collections;
using UnityEngine;
public class Monster : AbsGameObject
{
    private void Start()
    {
        // 몬스터는 기본적으로 코인을 가지고 있음
        hp = 10;
        coin = Random.Range(0, 10);
    }

    private void Update()
    {
        // 플레이어를 향하는 이동 구현예정
    }
    protected override void DropItem()
    {
        base.DropItem();

        Instantiate(gameObject, base.GenerateNewPosition(0) ,Quaternion.identity);
    }
}
