using _25_06_04_AR_First.Database;
using _25_06_04_AR_First.Mapping;
using _25_06_04_AR_First.Services;
using UnityEngine;

namespace _25_06_04_AR_First.Controllers
{
    public class MonsterController : MonoBehaviour
    {
        public MapLocation location;
        public MonsterService monsterService;
        public Monster monsterDataObject;
        public Animator animator;
        public float animatorSpeed;
        public float monsterWarmRate = .0001f;

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            animator.speed = animatorSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            if (animatorSpeed == 0)
            {
                //monster is frozen solid
                animatorSpeed = 0;
                return;
            }
            //if monster is moving they will warm up a bit
            animatorSpeed = Mathf.Clamp01(animatorSpeed + monsterWarmRate);
            animator.speed = animatorSpeed;
        }

    }
}
