using UnityEngine;
using System.Collections;

public class CannonMovement : MonoBehaviour
{
    public Transform[] bubblesToShoot;
    public float moveSpeed = 20f;
    public float rotationSpeed = 200f;
    public float waitTime = 1.5f;
    public Vector3 startPosition = new Vector3(0, 275, -4);
    public Vector3 targetPosition = new Vector3(0, 190, -4);

    void Start()
    {
        transform.position = startPosition;
        StartCoroutine(MoveAndShoot());
    }

    IEnumerator MoveAndShoot()
    {
        // Move down into view
        yield return StartCoroutine(MoveToPosition(targetPosition));

        // Shoot each bubble
        foreach (Transform bubble in bubblesToShoot)
        {
            if (bubble != null)
            {
                // Get initial random shooting angle (clamped between -70 and 70 degrees)
                float randomAngle = Random.Range(-70f, 70f);

                // Convert angle to rotation
                Quaternion targetRotation = Quaternion.Euler(randomAngle, -90, 90);

                // Rotate cannon to target rotation
                yield return StartCoroutine(RotateCannonTo(targetRotation));

                // Calculate the actual shoot direction based on the cannon's final rotation
                
                //Vector2 shootDirection = CalculateShootDirectionFromRotation(); ???????????????????????????????//
                float shootDirection = randomAngle + 270;


                // Activate and shoot the bubble
                ShootBubble(bubble, shootDirection);

                yield return new WaitForSeconds(waitTime);
            }
        }

        // Move back up after shooting
        yield return StartCoroutine(MoveToPosition(startPosition));

        Destroy(gameObject);
    }

    IEnumerator MoveToPosition(Vector3 position)
    {
        while (Vector3.Distance(transform.position, position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator RotateCannonTo(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.5f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    Vector2 CalculateShootDirectionFromRotation()
    {
        // Extract the X rotation angle (in our setup, this is the relevant angle for shooting direction)
        float angleInDegrees = transform.rotation.eulerAngles.x;

        // Convert to range -180 to 180 for consistent calculations
        if (angleInDegrees > 180)
            angleInDegrees -= 360;

        // Convert angle to radians
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;

        // Calculate direction vector (the negation accounts for the specific rotation setup in this game)
        return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
    }

    void ShootBubble(Transform bubble, Vector2 direction)
    {
        BubbleMovement bubbleScript = bubble.GetComponent<BubbleMovement>();
        if (bubbleScript != null)
        {
            bubbleScript.SetShotByCannon(direction);
            bubble.gameObject.SetActive(true);
        }
    }
    void ShootBubble(Transform bubble, float direction)
    {
        BubbleMovement bubbleScript = bubble.GetComponent<BubbleMovement>();
        if (bubbleScript != null)
        {
            bubbleScript.SetShotByCannon(direction);
            bubble.gameObject.SetActive(true);
        }
    }
}