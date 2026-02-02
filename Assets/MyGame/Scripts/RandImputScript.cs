using UnityEngine;

public class RandImputScript : MonoBehaviour
{
    public int valueA;
    public int valueB;

    void Update()
    {
        valueA += Random.value < 0.5f ? 1 : -1;
        valueB += Random.value < 0.5f ? 1 : -1;
    }
}
