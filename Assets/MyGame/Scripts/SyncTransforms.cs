using UnityEngine;

public class SyncTransforms : MonoBehaviour
{
    public Transform source;   
    public Transform target;


    // Update is called once per frame
    void Update()
    {
        if (source != null && target != null) {
            target.position = source.position + new Vector3(0, +0.7f, 0); 
            target.rotation = source.rotation * Quaternion.Euler(0, 180, 0);
        }
    }
}
