using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public enum EnemyType
    {
        Goblin,
        Slime,
        Wolf
    }

    public Enemy Create(EnemyType type)
    {
        switch(type)
        {
            case EnemyType.Goblin:
                return new Goblin();
            case EnemyType.Slime:
                return new Slime();
            case EnemyType.Wolf:
                return new Wolf();
            default:
                Debug.Log("생성 실패");
                return null;

        }
    }
}
