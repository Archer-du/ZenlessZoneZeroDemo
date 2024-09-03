using UnityEngine;

namespace ZZZDemo.Runtime.Model.Utils
{
    public static class MovementUtils
    {
        public static Vector3 GetXZProjection(Vector3 originVector)
        {
            Vector3 outVector = new Vector3(originVector.x, 0, originVector.z);
            return outVector.normalized;
        }
    }
}