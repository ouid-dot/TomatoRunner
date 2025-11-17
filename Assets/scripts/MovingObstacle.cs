using UnityEngine;
using System.Collections;

public class MovingObstacle : MonoBehaviour
{
    public float speed;
    Vector3 targetPos;

    // for basic movement
    public GameObject ways;
    public Transform[] wayPoints;
    int pointIndex;
    int pointCount;
    int direction = 1;

    // for timed movement
    public float waitDuration;
    int speedMultiplier = 1;

    // for changing animation
    private Animator animator;

    private void Awake()
    {
        // set array size to the number of endpoints
        wayPoints = new Transform[ways.transform.childCount];

        // iterate to fill the array with the values
        for (int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        pointCount = wayPoints.Length;

        // set the initial target position that the enemy will move towards
        pointIndex = 1; 
        targetPos = wayPoints[pointIndex].transform.position;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var step = speedMultiplier * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        if (transform.position == targetPos)
        {
            NextPoint();
        }
    }

    void NextPoint()
    {
        // if enemy arrived to the last point
        if (pointIndex == pointCount - 1) {
            direction = -1;

            animator.SetBool("change", true);
        }

        // if enemy arrived back to first point
        if (pointIndex == 0)
        {
            direction = 1;
            animator.SetBool("change", false);
        }

        pointIndex += direction;
        targetPos = wayPoints[pointIndex].transform.position;
        StartCoroutine(WaitNextPoint());
    }

    IEnumerator WaitNextPoint()
    {
        speedMultiplier = 0;
        yield return new WaitForSeconds(waitDuration);
        speedMultiplier = 1;
    }
}
