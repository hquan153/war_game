using UnityEngine;

public class CameraBoundaries : MonoBehaviour
{
    protected Camera camera;
    protected Vector2 extend;

    protected virtual void Awake()
    {
        camera = transform.GetComponent<Camera>();
        extend = new(1.02f, 1.02f);
    }

    protected void Start()
    {
        Vector2 bottomLeft = camera.ScreenToWorldPoint(new Vector3(0, 0, camera.nearClipPlane)) * extend;
        Vector2 topRight = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.nearClipPlane)) * extend;
        Vector2 topLeft = new(bottomLeft.x, topRight.y);
        Vector2 bottomRight = new(topRight.x, bottomLeft.y);

        transform.GetComponent<EdgeCollider2D>().points = new Vector2[] { bottomLeft, topLeft, topRight, bottomRight, bottomLeft };
    }
}
