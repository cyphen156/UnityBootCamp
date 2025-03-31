using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ParticleType
/// 1. Explosion, 2. Fire, 3. Smoke
/// </summary>
public enum ParticleType
{
    Explosion,
    Fire,
    Smoke,

    None
}

public class ParticleManager : MonoBehaviour
{
    private static ParticleManager Instance;

    private Dictionary<ParticleType, GameObject> dicParticleSystem;
    private Dictionary<ParticleType, Queue<GameObject>> particlePools;

    public GameObject weaponExplosionParticle;
    public GameObject weaponFireParticle;
    public GameObject weaponSmokeParticle;

    public int poolSize = 50;

    public static ParticleManager GetInstance ()
    {
        return Instance;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            dicParticleSystem = new Dictionary<ParticleType, GameObject>();
            dicParticleSystem.Add(ParticleType.Explosion, weaponExplosionParticle);
            dicParticleSystem.Add(ParticleType.Fire, weaponFireParticle);
            dicParticleSystem.Add(ParticleType.Smoke, weaponSmokeParticle);

            particlePools = new Dictionary<ParticleType, Queue<GameObject>>();

            foreach (var type in dicParticleSystem.Keys)
            {
                Queue<GameObject> pool = new Queue<GameObject>();
                for (int i = 0; i < poolSize; i++)
                {
                    GameObject obj = Instantiate(dicParticleSystem[type]);
                    obj.gameObject.SetActive(false);
                    pool.Enqueue(obj);
                }
                particlePools.Add(type, pool);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool ParticlePlay(ParticleType particleType, Vector3 position)
    {
        if (!dicParticleSystem.ContainsKey(particleType))
        {
            Debug.LogWarning($"등록되지 않은 파티클 타입: {particleType}");
            return false;
        }

        if (!particlePools.ContainsKey(particleType) || particlePools[particleType].Count == 0)
        {
            Debug.LogWarning($"{particleType} 파티클 풀에 남은 오브젝트가 없습니다.");
            return false;
        }

        if (dicParticleSystem.ContainsKey(particleType))
        {
            GameObject particleObj = particlePools[particleType].Dequeue();

            if (particleObj != null)
            {
                particleObj.transform.position = position;
                ParticleSystem particleSystem = particleObj.GetComponentInChildren<ParticleSystem>();

                if(particleSystem.isPlaying)
                {
                    particleSystem.Stop();
                }

                particleObj.SetActive(true);
                particleSystem.Play();
                StartCoroutine(ParticleEnd(particleType, particleObj, particleSystem)); 
            }

            //ParticleSystem particle = Instantiate(dicParticleSystem[particleType], position, Quaternion.identity);
            //particle.Play();
            //Destroy(particle.gameObject, particle.main.duration);
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator ParticleEnd(ParticleType type, GameObject particleObj, ParticleSystem particleSystem)
    {
        while (particleSystem.isPlaying)
        {
            yield return null;
        }

        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        particleObj.SetActive(false);
        particlePools[type].Enqueue(particleObj);
    }
}
