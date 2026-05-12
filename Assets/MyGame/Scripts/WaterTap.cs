using UnityEngine;

public class WaterTap : MonoBehaviour {
    public Transform spawnPos;
    public GameObject waterDropPrefab;
    public Transform waterContainer;
    public float spawnRate;

    bool isActive;
    float _timer;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) isActive = !isActive;

        _timer += Time.deltaTime;
        if (_timer < 1f / spawnRate || !isActive) return;
        _timer = 0f;

        SpawnDrop();
    }

    void SpawnDrop() {
        GameObject instance = Instantiate(waterDropPrefab, spawnPos.position, Quaternion.identity, waterContainer);
        instance.GetComponent<Rigidbody>().AddForce(Vector3.down * 2f, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("blueBall")) {
            Destroy(collision.gameObject);
        }
    }
}
