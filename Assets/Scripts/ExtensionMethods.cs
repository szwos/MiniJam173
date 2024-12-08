using UnityEngine;

public static class ExtensionMethods
{
    public static void LookAt2D(this UnityEngine.Transform transform, UnityEngine.Transform target)
    {
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}