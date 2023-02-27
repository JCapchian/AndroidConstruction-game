using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class SpawnerManagers : MonoBehaviour
{
    [Header ("Componentes")]
    private Collider2D triggerZone;

    [Header ("Spawners en control")]
    [SerializeField]
    List<Spawner> spawners;

    [Header ("Clips de audio")]
    AudioSource audioSource;
    [SerializeField]
    AudioClip spawnClip;
    [SerializeField]
    AudioClip turnDoorClip;
    [SerializeField]
    AudioClip finishWaveClip;

    [Header ("Propiedades")]
    [SerializeField]
    GameObject finalDrop;
    [SerializeField]
    int wavesAmount;
    [SerializeField]
    public bool lastWave;

    [SerializeField]
    float timeBetweenWaves;

    [SerializeField]
    GameObject keyRef;

    public GameObject[] doorsPoints;

    private void Awake()
    {
        triggerZone = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < transform.GetChild(1).childCount ; i++)
        {
            if(transform.GetChild(1).GetChild(i).GetComponent<Spawner>())
            {
                spawners.Add(transform.GetChild(1).GetChild(i).GetComponent<Spawner>());
                Debug.Log(spawners[i]);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerManager>())
        {
            Debug.Log("Entro");
            TurnDoors(true);

            audioSource.PlayOneShot(turnDoorClip);

            ActivateSpawners();
            Destroy(triggerZone);
        }
    }

    private void ActivateSpawners()
    {
        // Pregunto si la cantidad de rondas es mayor a 0
        if(wavesAmount == 0)
        {
            Debug.Log("Ultima ronda");
            lastWave = true;
        }

        Debug.Log(wavesAmount);

        // Paso por cada spawner para activarlo
        for (int i = 0; i < spawners.Count; i++)
        {
            StartCoroutine(spawners[i].StartWave(timeBetweenWaves));
        }

        wavesAmount--;
    }

    public void CheckSpawners()
    {
        var op = true;
        for (int i = 0; i < spawners.Count; i++)
        {
            if(!spawners[i].isEnemyDeath)
                return;
        }

        if(op)
        {
            if(lastWave)
            {
                StartCoroutine(EndSpawner());
                return;
            }
            ActivateSpawners();
        }
    }

    IEnumerator EndSpawner()
    {
        // Spawneo el ultimo objeto
        audioSource.PlayOneShot(finishWaveClip);
        if(keyRef)
            keyRef.SetActive(true);
        else
            Instantiate(finalDrop, transform.position, transform.rotation);

        yield return new WaitForSeconds(0.8f);
        TurnDoors(false);

        Destroy(this.gameObject, 1f);
    }

    // private IEnumerator StartWave()
    // {
    //     // Inicio el tiempo de espera
    //     yield return new WaitForSeconds(timeBetweenWaves);

    //     if(activeKey)
    //         keyRef.SetActive(activeKey);
    //     else
    //         Instantiate(finalDrop, transform.position, transform.rotation);

    //     yield return new WaitForSeconds(0.4f);
    //     TurnDoors(true);

    //     Destroy(this.gameObject);
    // }

    private void TurnDoors(bool state)
    {
        audioSource.PlayOneShot(turnDoorClip);
        for (var i = 0; i < doorsPoints.Length; i++)
        {
            doorsPoints[i].SetActive(state);
        }
    }
}
