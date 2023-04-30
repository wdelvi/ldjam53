// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;

namespace Sonity.Internal {

    public static class AxisLock {

        public static Vector3 Lock(Vector3 vector, AxisToLock axisToLock, float position) {
            if (axisToLock == AxisToLock.X) {
                vector.x = position;
            } else if (axisToLock == AxisToLock.Y) {
                vector.y = position;
            } else if (axisToLock == AxisToLock.Z) {
                vector.z = position;
            }
            return vector;
        }
    }
}