using UnityEngine;

public class ScrollingBackgroundSprites : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private Transform[] backgroundParts;
    [SerializeField] private float backgroundWidth;

    private Camera mainCamera;
    private float cameraWidth;

    private void Start()
    {
        mainCamera = Camera.main;
        cameraWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;
    }

    private void Update()
    {
        ScrollBackground();
        CheckReposition();
    }

    private void ScrollBackground()
    {
        foreach (Transform part in backgroundParts)
        {
            part.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        }
    }

    private void CheckReposition()
    {
        for (int i = 0; i < backgroundParts.Length; i++)
        {
            if (backgroundParts[i].position.x < mainCamera.transform.position.x - cameraWidth / 2 - backgroundWidth / 2)
            {
                RepositionBackgroundPart(i);
            }
        }
    }

    private void RepositionBackgroundPart(int index)
    {
        Vector3 newPosition = GetFarthestBackgroundPosition() + Vector3.right * backgroundWidth;
        backgroundParts[index].position = newPosition;
    }

    private Vector3 GetFarthestBackgroundPosition()
    {
        Vector3 farthestPosition = backgroundParts[0].position;

        for (int i = 1; i < backgroundParts.Length; i++)
        {
            if (backgroundParts[i].position.x > farthestPosition.x)
            {
                farthestPosition = backgroundParts[i].position;
            }
        }

        return farthestPosition;
    }
}