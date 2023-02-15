using System.Collections.Generic;
using UnityEngine;

namespace utils {
    public enum VectorAxis {
        X,
        Y,
        Z
    }
    public enum QuaternionAxis {
        X,
        Y,
        Z,
        W
    }
    public enum Side {
        Left,
        Center,
        Right
    }
    public enum ColorAxis {
        R,
        G,
        B,
        A
    }
    public static partial class StaticMethod {
        #region Vector3
        public static Vector3 Restricted(this Vector3 movement, bool x = false, bool y = false, bool z = false) {
            return new Vector3(x ? 0 : movement.x, y ? 0 : movement.y, z ? 0 : movement.z);
        }
        public static Vector3 UpdateAxis(this Vector3 movement, float newValue, VectorAxis axis) {
            return new Vector3(axis == VectorAxis.X ? newValue : movement.x, axis == VectorAxis.Y ? newValue : movement.y, axis == VectorAxis.Z ? newValue : movement.z);
        }
        public static Vector3 Add(this Vector3 first, Vector3 second) {
            return new Vector3(first.x + second.x,first.y + second.y,first.z + second.z);
        }
        public static Vector3 Divide(this Vector3 first, Vector3 second) {
            return new Vector3(first.x/second.x, first.y/second.y, first.z/second.z);
        }
        #endregion
        #region Vector2
        public static Vector2 UpdateAxis(this Vector2 movement, float newValue, VectorAxis axis) {
            if (axis != VectorAxis.Z) return new Vector2(axis == VectorAxis.X ? newValue : movement.x, axis == VectorAxis.Y ? newValue : movement.y);
            else Debug.LogError("Incorrect Axis Given to Vector2 UpdateAxis");
            return movement;
        }
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
            return new Quaternion(first.x - second.x, first.y - second.y, first.z - second.z, first.w);
        }
        public static Quaternion Add(this Quaternion first, Quaternion second) {
            return new Quaternion(first.x + second.x, first.y + second.y, first.z + second.z, first.w);
        }
        public static Quaternion Update(this Quaternion inQuaternion, float newValue, QuaternionAxis axis) {
            return new Quaternion(axis == QuaternionAxis.X ? newValue : inQuaternion.x, axis == QuaternionAxis.Y ? newValue : inQuaternion.y, axis == QuaternionAxis.Z ? newValue : inQuaternion.z, axis == QuaternionAxis.W ? newValue : inQuaternion.w);
        }
        #endregion
        #region Color
        public static Color UpdateColor(this Color color,float newValue,ColorAxis axis) {
            return new Color(axis == ColorAxis.R ? newValue : color.r, axis == ColorAxis.G ? newValue : color.g, axis == ColorAxis.B ? newValue : color.b, axis == ColorAxis.A ? newValue : color.a);
        }
        #endregion
        #region Generic
        public static T RandomElements<T>(this T[] array) {
            return array[Random.Range(0, array.Length)];
        }
        public static T RandomElements<T>(this List<T> list) {
            return list[Random.Range(0, list.Count)];
        }
        public static bool isNull<T>(this T element) {
            return element == null || element.Equals(null) || (element is string && string.IsNullOrEmpty(element.ToString()));
        }
        #endregion
    }
}