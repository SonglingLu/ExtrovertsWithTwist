using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.RadialMenu_v3
{
    [Serializable]
    public struct ClockwisePolarCoord
    {
        
        public float Radius { get; set; }
        /// <summary> 0 ~ 360 각도 </summary>
        public float Angle
        {
            get => _angle;
            set => _angle = ClampAngle(value);
        }
        private float _angle;

        public ClockwisePolarCoord(float radius, float angle)
        {
            Radius = radius;
            _angle = ClampAngle(angle);
        }

        private static float ClampAngle(float angle)
        {
            angle %= 360f;
            if (angle < 0f)
                angle += 360f;
            return angle;
        }

        private static float CovertAngle(float angle)
            => 90f - angle;

        /// <summary> Degree(0 ~ 360)로 Sin 계산 </summary>
        private static float Sin(float angle)
            => Mathf.Sin(angle * Mathf.Deg2Rad);

        /// <summary> Degree(0 ~ 360)로 Cos 계산 </summary>
        private static float Cos(float angle)
            => Mathf.Cos(angle * Mathf.Deg2Rad);

       
        public static ClockwisePolarCoord Zero => new ClockwisePolarCoord(0f, 0f);
        public static ClockwisePolarCoord North => new ClockwisePolarCoord(1f, 0f);
        public static ClockwisePolarCoord East => new ClockwisePolarCoord(1f, 90f);
        public static ClockwisePolarCoord South => new ClockwisePolarCoord(1f, 180f);
        public static ClockwisePolarCoord West => new ClockwisePolarCoord(1f, 270f);

  
        public static ClockwisePolarCoord FromVector2(in Vector2 vec)
        {
            if (vec == Vector2.zero)
                return Zero;

            float radius = vec.magnitude;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;

            return new ClockwisePolarCoord(radius, CovertAngle(angle));
        }

        public static bool operator ==(ClockwisePolarCoord a, ClockwisePolarCoord b)
        {
            return Mathf.Approximately(a.Angle, b.Angle) &&
                   Mathf.Approximately(a.Radius, b.Radius);
        }

        public static bool operator !=(ClockwisePolarCoord a, ClockwisePolarCoord b)
        {
            return !(Mathf.Approximately(a.Angle, b.Angle) &&
                   Mathf.Approximately(a.Radius, b.Radius));
        }

        public ClockwisePolarCoord Normalized => new ClockwisePolarCoord(1f, Angle);

        public Vector2 ToVector2()
        {
            if (Radius == 0f && Angle == 0f)
                return Vector2.zero;

            float angle = CovertAngle(Angle);
            return new Vector2(Radius * Cos(angle), Radius * Sin(angle));
        }

        public override string ToString()
            => $"({Radius}, {Angle})";

        public override bool Equals(object obj)
        {
            if(obj == null) return false;

            if (obj is ClockwisePolarCoord other)
            {
                return this == other;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}