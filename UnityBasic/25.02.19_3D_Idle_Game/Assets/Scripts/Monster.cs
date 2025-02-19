using System;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : Character
{
    GameObject gOPlayer;
    [SerializeField] private float monsterSpeed;
    [SerializeField] private float accellateSpeed;
    [SerializeField] private Vector3 spacing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        monsterSpeed = 1f;
        gOPlayer = GameObject.FindGameObjectWithTag("Player");
        spacing = new Vector3(1, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition =  gOPlayer.transform.position;

        //transform.LookAt(playerPosition); 
        // ���߾����� �ü�����
        
        transform.position = Vector3.MoveTowards(transform.position, playerPosition - spacing, Time.deltaTime * monsterSpeed);
        // �÷��̾����� �̵�

        SetMotionChange("isMove", true);
    }

    
    internal void MonsterSetPosition()
    {
    }
}
