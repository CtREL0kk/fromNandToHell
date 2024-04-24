using UnityEngine;

public static class VectorExtensions
{
    public static Vector3 Rotate(this Vector3 vector, float angleInDegrees)
    {
        var angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        var cosTheta = Mathf.Cos(angleInRadians);
        var sinTheta = Mathf.Sin(angleInRadians);
        var x = vector.x * cosTheta - vector.y * sinTheta;
        var y = vector.x * sinTheta + vector.y * cosTheta;
        return new Vector3(x, y, 0);
    }
}
