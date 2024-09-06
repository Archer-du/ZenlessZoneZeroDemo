using UnityEngine;

namespace ZZZDemo.Runtime.Model.Utils
{
    public static class MovementUtils
    {
        public static Vector3 GetHorizontalProjectionVector(Vector3 originVector)
        {
            Vector3 outVector = new Vector3(originVector.x, 0, originVector.z);
            return outVector.normalized;
        }

        public static float GetRelativeInputAngle(Vector2 inputVector)
        {
            if (inputVector == Vector2.zero) return 0;
            inputVector = inputVector.normalized;
            float angleInRadians = Mathf.Atan2(inputVector.y, inputVector.x);
            float angleInDegrees = Mathf.Rad2Deg * angleInRadians;
            // offset to adapt quaternion rotation
            angleInDegrees = - angleInDegrees + 90;
            return angleInDegrees;
        }

        public static Quaternion GetRotationByAxis(float angle, Vector3 axis)
        {
            return Quaternion.AngleAxis(angle, axis);
        }

        public static float GetRelativeRotateAngle(Vector3 baseVec, Vector3 targetVec)
        {
            float cos = Vector3.Dot(baseVec, targetVec) / (baseVec.magnitude * targetVec.magnitude);
            float angleInRadians = Mathf.Acos(cos);
            float angleInDegrees = Mathf.Rad2Deg * angleInRadians;
            return Vector3.Cross(targetVec, baseVec).y < 0 ? angleInDegrees : -angleInDegrees;
        }
    }
}