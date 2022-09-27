using UnityEngine;
using Random = UnityEngine.Random;

public class Tiles : MonoBehaviour
{
    [System.Serializable]
    public class ObstacleData
    {
        public GameObject obstacle;
        public float probability;
        public int scoreSpawn;
    }
    
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private ObstacleData[] obstacles;

    public Transform StartPoint => startPoint;
    public Transform EndPoint => endPoint;

    public void ActivateRandomObstacle(int score)
    {
        HideAllObstacles();
        
        GetObstacleData(score).obstacle.SetActive(true);
    }

    public void HideAllObstacles()
    {
        for(int i = 0; i < obstacles.Length; i++) 
            obstacles[i].obstacle.SetActive(false);
    }

    private ObstacleData GetObstacleData(int score)
    {
        float random = Random.value;
        float currentProb = 0;
        

        for (int i = 0; i < obstacles.Length; i++)
        {
            if(score < obstacles[i].scoreSpawn)
                continue;

            currentProb += obstacles[i].probability;

            if (random <= currentProb)
                return obstacles[i];
        }

        return obstacles[0];
    }
} 
