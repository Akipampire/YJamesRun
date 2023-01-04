using System.Collections;
using UnityEngine;

namespace utils {
    public enum VectorAxis {
        X,
        Y,
        Z
    }
    public static partial class StaticMethod {
        #region Vector3
        public static Vector3 Restricted(this Vector3 movement, bool x = false, bool y = false, bool z = false) {
            return new Vector3(x ? 0 : movement.x, y ? 0 : movement.y, z ? 0 : movement.z);
        }
        public static Vector3 UpdateAxis(this Vector3 movement, float newValue, VectorAxis axis) {
            return new Vector3(axis == VectorAxis.X ? newValue : movement.x, axis == VectorAxis.Y ? newValue : movement.y, axis == VectorAxis.Z ? newValue : movement.z);
        }
        public static Vector3 Divide(this Vector3 first, Vector3 second) {
            return new Vector3(first.x/second.x, first.y/second.y, first.z/second.z);
        }
        #endregion
        #region Vector2
        public static Vector2 Add(this Vector2 first, Vector2 second) {
            return new Vector2(first.x + second.x, first.y + second.y);
        }
        #endregion
        #region Collider
        public static bool IsGrounded(this Collider obj) {
            return Physics.CheckSphere(obj.bounds.min, .2f, LayerMask.GetMask("Ground"));
        }
        #endregion
        #region Quaternion
        public static Quaternion Sub(this Quaternion first, Quaternion second) {
            return new Quaternion(first.x - second.x, first.y - second.y, first.z - second.z,first.w);
        }
        public static Quaternion Add(this Quaternion first, Quaternion second) {
            return new Quaternion(first.x + second.x, first.y + second.y, first.z + second.z, first.w);
        }
        #endregion

    }
}