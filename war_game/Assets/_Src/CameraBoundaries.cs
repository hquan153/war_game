using UnityEngine;

public class CameraBoundaries : MonoBehaviour
{
    private void Awake()
    {
        Camera camera = transform.GetComponent<Camera>();

        Vector2 bottomLeft = camera.ScreenToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Vector2 topRight = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.nearClipPlane));
        Vector2 topLeft = new(bottomLeft.x, topRight.y);
        Vector2 bottomRight = new(topRight.x, bottomLeft.y);

        transform.GetComponent<EdgeCollider2D>().points = new Vector2[] { bottomLeft, topLeft, topRight, bottomRight, bottomLeft };
    }
}
