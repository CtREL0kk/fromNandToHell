using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    private Camera mainCamera;
    private Collider2D enemy;

    public void Initialize(Camera camera, Collider2D enemyCollider)
    {
        mainCamera = camera;
        enemy = enemyCollider;
    }

    void Update()
    {
        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }

        var screenPoint = mainCamera.WorldToScreenPoint(enemy.transform.position);
        var onScreen = screenPoint.x > 0 && screenPoint.x < Screen.width 
                    && screenPoint.y > 0 && screenPoint.y < Screen.height;

        if (onScreen)
        {
            transform.position = mainCamera.ScreenToWorldPoint(screenPoint + new Vector3(0, 150, 0));
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else
        {
            var cappedScreenPoint = screenPoint;

            if (screenPoint.x < 0) cappedScreenPoint.x = 10;
            if (screenPoint.x > Screen.width) cappedScreenPoint.x = Screen.width - 10;
            if (screenPoint.y < 0) cappedScreenPoint.y = 10;
            if (screenPoint.y > Screen.height) cappedScreenPoint.y = Screen.height - 10;

            transform.position = mainCamera.ScreenToWorldPoint(cappedScreenPoint);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
}
