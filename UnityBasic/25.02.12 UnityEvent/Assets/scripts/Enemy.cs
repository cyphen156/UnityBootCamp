using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public interface Enemy
{
    void Action();
}

public class Goblin : Enemy
{
    public void Action()
    {
        Debug.Log("고블린이 몽둥이로 후드린다.");   
    }
}
public class Slime : Enemy
{
    public void Action()
    {
        Debug.Log("슬라임 녹는다...");
    }
}

public class Wolf : Enemy
{
    public void Action()
    {
        Debug.Log("쿠와아아아앙!하고 늑대가 울부짖었다.");
    }
}
