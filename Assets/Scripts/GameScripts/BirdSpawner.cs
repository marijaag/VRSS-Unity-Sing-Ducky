using UnityEngine;

public class PticaSpawner : MonoBehaviour
{
    public GameObject pticaPrefab;
    public float minInterval = 8f;
    public float maxInterval = 20f;
    private float timer;

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnPtica();
            ResetTimer();
        }
    }

    void SpawnPtica()
    {
        int smer = Random.Range(0, 2);
        Vector3 spawnPos;
        Vector3 targetPos;
        Quaternion rotacija;

       if (smer == 0)
    {
        // levo na desno
        spawnPos = new Vector3(-20f, Random.Range(7.5f, 8f), 1f);
        targetPos = new Vector3(20f, spawnPos.y, spawnPos.z);
        rotacija = Quaternion.Euler(0, 90, 0);
    }
    else
    {
        // desno na levo
        spawnPos = new Vector3(20f, Random.Range(7.5f, 8f), 1f);
        targetPos = new Vector3(-20f, spawnPos.y, spawnPos.z);
        rotacija = Quaternion.Euler(0, -90, 0);
    }

    GameObject ptica = Instantiate(pticaPrefab, spawnPos, rotacija);
    ptica.GetComponent<BirdController>().SetTarget(targetPos);
    }

    void ResetTimer()
    {
        timer = Random.Range(minInterval, maxInterval);
    }
}
