using UnityEngine;

public class Spwaner : MonoBehaviour
{
    public GameObject prefab;
    
    public float spawnRate = 1f;

    public float minHeight = -1f;

    public float maxHeight = 1f;

    private void OnEnable() {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable() {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        GameObject Pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        Pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);    
    }
}
