using Unity.VisualScripting;
using UnityEngine;


public class BubbleMovement : MonoBehaviour
{
    const float ratio = 1.3421053632f; // קבוע מסוג float
    float speed = 3800f;  // מהירות הבועה
    float minX = -396.4f;
    float maxX = 396.418f;
    float minY = -222.887f;
    float maxY = 222.8f;
    float angle;
    float radios;
    public GameObject newBubbelPrefab;
    public int damage;
    private bool shotByCannon = false; // NEW: Flag to track if the cannon shot this bubble
    private Vector2 movementDirection;






    void Start()
    {

        radios = transform.localScale.x * ratio;

        speed = speed / transform.localScale.x;
        if (speed > 300)
        {
            speed = 300;
        }
        damage = (int) radios/2; // אתה יכול לשנות את 10 לפי הכמות שאתה רוצה שה-bubble תוריד
        if (!shotByCannon)
        {
            angle = setNewAngle(0, 360);
            movementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }
    }

    void Update()
    {

        // עדכון המיקום של הבועה בכיוון הרנדומלי
        transform.Translate(movementDirection * speed * Time.deltaTime);

        CheckAndChangeDirection();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            collision.gameObject.SetActive(false);

            if (CompareTag("Magenta"))
            {
                Destroy(gameObject);
            }
            else
            {
                GameObject newBubbel2 = Instantiate(newBubbelPrefab, transform.position, Quaternion.identity, null);
                GameObject newBubbel3 = Instantiate(newBubbelPrefab, transform.position, Quaternion.identity, null);
                Destroy(gameObject);
            }
        }

        if (collision.CompareTag("Rock"))
        {

            angle += RandomAngleOffset(110, -80);
            movementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        }
    }



    void CheckAndChangeDirection()
    {
        if (transform.position.x <= minX + radios)
        {
            // Debug.Log("X = " + transform.position.x + "\n");

            angle = setNewAngle(270, 450);
            movementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }
        if (transform.position.x >= maxX - radios)
        {
            angle = setNewAngle(90, 270);
            movementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        }

        if (transform.position.y <= minY + radios)
        {
            // Debug.Log("Y = " + transform.position.y + "\n");

            angle = setNewAngle(0, 180);
            movementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }
        if (transform.position.y >= maxY - radios)
        {
            angle = setNewAngle(180, 360);
            movementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        }

    }

    float setNewAngle(float minAngle, float maxAngle)
    {
        return Random.Range(minAngle, maxAngle) * Mathf.Deg2Rad;
    }
    public void SetShotByCannon(Vector2 direction)
    {
        shotByCannon = true;
        movementDirection = direction;
    }

    public void SetShotByCannon(float direction)
    {
        shotByCannon = true;
        angle = direction;
        movementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

    }

    float RandomAngleOffset(float val1, float val2)
    {
        return Random.Range(0, 2) == 0 ? val1 : val2;
    }


}

