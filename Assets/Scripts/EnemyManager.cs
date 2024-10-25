using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokeParticlesPrefab;
    [SerializeField] private float timeToDestroy = 1f; //CAMBIAR

    private void Awake()
    {
        Enemy.onEnemyDeath += Enemy_onEnemyDie;
    }

    private void OnDestroy()
    {
        Enemy.onEnemyDeath -= Enemy_onEnemyDie;
    }

    private void Enemy_onEnemyDie(Vector3 pos)
    {
        ParticleSystem smokeParticles = Instantiate(smokeParticlesPrefab, pos, Quaternion.identity);
        smokeParticles.Play();
        StartCoroutine(DestroyParticle(smokeParticles));
    }

    private IEnumerator DestroyParticle(ParticleSystem smokeParticles)
    {
        float currentTime = 0;

        while(currentTime < timeToDestroy)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        Destroy(smokeParticles);
    }
}
