using UnityEngine;

public class PatrolingScript : MonoBehaviour
{
    [Header("Patrol points")]
    public Transform[] points;

    public float speed = 2f;
    public float waitTime = 1f;
    public bool loop = true;

    private int currentIndex = 0;
    private bool waiting = false;
    private float waitTimer = 0f;

    private bool isSpriteReversable = true;

    private void Start()
    {
        if (points.Length == 0)
        {
            Debug.LogWarning("EnemyPatrol: не назначены точки патруля!");
            enabled = false;
        }
    }

    private void Update()
    {
        if (waiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
                waiting = false;
            return;
        }

        Transform targetPoint = points[currentIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.05f)
        {
            waitTimer = waitTime;
            waiting = true;

            if (currentIndex + 1 < points.Length)
                currentIndex++;
            else if (loop)
                currentIndex = 0;
        }

        if (isSpriteReversable)
        {
            Vector3 scale = transform.localScale;
            if (targetPoint.position.x > transform.position.x)
                scale.x = Mathf.Abs(scale.x);
            else
                scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    /// <summary>
    /// Визуализация пути через гизмо
    /// </summary>
    private void OnDrawGizmos()
    {
        if (points == null || points.Length == 0)
            return;

        Gizmos.color = Color.red;

        // Рисуем точки
        foreach (Transform point in points)
        {
            if (point != null)
                Gizmos.DrawSphere(point.position, 0.1f);
        }

        Gizmos.color = Color.yellow;
        for (int i = 0; i < points.Length - 1; i++)
        {
            if (points[i] != null && points[i + 1] != null)
                Gizmos.DrawLine(points[i].position, points[i + 1].position);
        }

        if (loop && points.Length > 1 && points[0] != null && points[^1] != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(points[^1].position, points[0].position);
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.15f);
    }
}