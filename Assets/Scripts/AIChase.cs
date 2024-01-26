using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    [SerializeField] GameObject Ball;
    [SerializeField] float speed;
    [SerializeField] Transform goal; // Reference to the goal transform
    [SerializeField] float aimingStrength; // Adjust the aiming strength

    private float distance;

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, Ball.transform.position);
        Vector2 directionToBall = Ball.transform.position - transform.position;

        // Move towards the ball
        transform.position = Vector2.MoveTowards(transform.position, Ball.transform.position, speed * Time.deltaTime);

        // Calculate the direction towards the goal
        Vector2 directionToGoal = goal.position - transform.position;

        // Calculate the angle between the direction to the ball and the direction to the goal
        float angleToGoal = Vector2.SignedAngle(directionToBall, directionToGoal);


        float angleInRadians = angleToGoal * Mathf.Deg2Rad;
        transform.position += aimingStrength * Time.deltaTime * new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0);
    }
}
