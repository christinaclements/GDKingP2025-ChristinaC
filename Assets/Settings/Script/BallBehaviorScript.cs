using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BallBehaviorScript : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float minX = -8.01f;
    public float minY = -4.16f;
    public float maxX = 7.8f;
    public float maxY = 4.34f;
    public float minSpeed;
    public float maxSpeed;
    public Vector2 targetPosition;
    public GameObject target;
    public float minLaunchSpeed;
    public float maxLaunchSpeed;
    public float minTimeToLaunch;
    public float maxTimeToLaunch;
    public float cooldown;
    public bool launching;
    public float launchDuration;
    public float timeLastLaunch;
    public int secondsToMaxSpeed;
    void Start()
    {
        //secondsToMaxSpeed = 30;
        //minSpeed = .001f;
        //maxSpeed = 2.0f;
        targetPosition = getRandomPosition();
    }

    // Update is called once per frame
    void Update(){
        Vector2 currentPos = gameObject.GetComponent<Transform>().position;
        float distance = Vector2.Distance((Vector2)transform.position, targetPosition);
        if (distance > 0.1f){
            float difficulty = getDifficultyPercentage();
            float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, difficulty);
            currentSpeed = currentSpeed * Time.deltaTime;
            Vector2 newPosition = Vector2.MoveTowards(currentPos, targetPosition, currentSpeed);
            transform.position = newPosition;
        }
        else {
            targetPosition = getRandomPosition();
        } 
    }
    public Vector2 getRandomPosition() {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector2 v = new Vector2(randomX, randomY);
        return v;

    }
    public float getDifficultyPercentage() {
        float difficulty = Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);
        return difficulty;

    }
    public void lunch() { 
    
    }
}
