using UnityEngine;
using System;

namespace ReupVirtualTwin.helpers
{
    public class WalkInterpolator : Interpolator<Vector3>
    {
        Vector3 origin;
        Vector3 sameHeightTarget;
        Vector3 direction;
        Vector3 sameHeightcurrentPosition;
        float MAX_SPEED_IN_METERS_PER_SECOND = 3f;
        float MIN_SPEED_IN_METERS_PER_SECOND = 1.5f;
        float DISTANCE_TO_MAX_SPEED = 2f;
        float DISTANCE_TO_MIN_SPEED = 3f;

        SpeedHandler speedHandler;

        public void DefineOriginAndTarget(Vector3 originalPostion, Vector3 targetPostion)
        {
            origin = originalPostion;
            sameHeightTarget = new Vector3(targetPostion.x, origin.y, targetPostion.z);
            sameHeightcurrentPosition = originalPostion;
            var totalDistance = Vector3.Distance(origin, sameHeightTarget);
            speedHandler = new DistanceSpeedHandler(totalDistance,
                MAX_SPEED_IN_METERS_PER_SECOND,
                MIN_SPEED_IN_METERS_PER_SECOND,
                DISTANCE_TO_MAX_SPEED,
                DISTANCE_TO_MIN_SPEED);
            direction = Vector3.Normalize(sameHeightTarget - origin);
            if (direction.y != 0)
            {
                direction.y = 0;
            }
        }

        public Vector3 Interpolate(Vector3 currentValue)
        {
            var sameHeightCurrentValue = new Vector3(currentValue.x, origin.y, currentValue.z);
            float traveledDistance = Vector3.Distance(sameHeightCurrentValue, origin);
            float speed = speedHandler.GetSpeedInMetersPerSecond(traveledDistance);
            sameHeightcurrentPosition += direction * speed * Time.deltaTime;
            var currentPosition = new Vector3(sameHeightcurrentPosition.x, currentValue.y, sameHeightcurrentPosition.z);
            return currentPosition;
        }
    }
}
