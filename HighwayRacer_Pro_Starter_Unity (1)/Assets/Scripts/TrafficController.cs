using UnityEngine;

public class TrafficController : MonoBehaviour
{
    [System.Serializable]
    public class TrafficCar{
        public Transform root;
        public int laneIndex = 1;
        public float z;
        public float speedKmh = 20f; // per request, constant 20 km/h
    }

    public TrafficCar[] cars;
    public float[] lanes = new float[]{ -4.2f, -1.4f, 1.4f, 4.2f };
    public float spawnZMin = -600f;
    public float spawnZMax = -60f;
    public float resetZ = 60f;

    void ResetCar(TrafficCar c){
        c.laneIndex = Random.Range(0, lanes.Length);
        c.z = Random.Range(spawnZMin, spawnZMax);
        if (c.root) c.root.position = new Vector3(lanes[c.laneIndex], 0f, c.z);
    }

    void Start(){
        foreach (var c in cars){
            c.speedKmh = 20f;
            ResetCar(c);
        }
    }

    void Update(){
        foreach (var c in cars){
            float v = c.speedKmh / 3.6f;
            c.z += v * Time.deltaTime;
            if (c.root){
                c.root.position = new Vector3(lanes[c.laneIndex], 0f, c.z);
            }
            if (c.z > resetZ){
                ResetCar(c);
            }
        }
    }
}
