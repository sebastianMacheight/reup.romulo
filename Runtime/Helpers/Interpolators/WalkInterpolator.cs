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
        float totalDistance;
        float MAX_SPEED_IN_METERS_PER_SECOND = 3f;
        float MIN_SPEED_IN_METERS_PER_SECOND = 1.5f;
        float FRACTION_TO_MAX_SPEED = 0.2f;
        float FRACTION_TO_SLOW_DOWN = 0.85f;

        public void DefineOriginAndTarget(Vector3 originalPostion, Vector3 targetPostion)
        {
            origin = originalPostion;
            sameHeightTarget = new Vector3(targetPostion.x, origin.y, targetPostion.z);
            sameHeightcurrentPosition = originalPostion;
            totalDistance = Vector3.Distance(origin, sameHeightTarget);
            direction = Vector3.Normalize(sameHeightTarget - origin);
            if (direction.y != 0)
            {
                direction.y = 0;
            }
        }

        public Vector3 Interpolate(Vector3 currentValue)
        {
            //Debug.Log($"in Walk interpolate in is {currentValue}");
            var sameHeightCurrentValue = new Vector3(currentValue.x, origin.y, currentValue.z);
            float traveledDistance = Vector3.Distance(sameHeightCurrentValue, origin);
            float traveledFraction = traveledDistance / totalDistance;
            float speed = GetSpeed(traveledFraction);
            sameHeightcurrentPosition += direction * speed * Time.deltaTime;
            var currentPosition = new Vector3(sameHeightcurrentPosition.x, currentValue.y, sameHeightcurrentPosition.z);
            //Debug.Log($"at the end in Walk interpolate out is {currentPosition}");
            return currentPosition;
        }

        private float GetSpeed(float travelFraction)
        {
            if (travelFraction < FRACTION_TO_MAX_SPEED)
            {
                float growingSlope = (MAX_SPEED_IN_METERS_PER_SECOND - MIN_SPEED_IN_METERS_PER_SECOND) / FRACTION_TO_MAX_SPEED;
                float growingSpeed = growingSlope * travelFraction  + MIN_SPEED_IN_METERS_PER_SECOND;
                return growingSpeed;
            }
            if (travelFraction < FRACTION_TO_SLOW_DOWN)
            {
                return MAX_SPEED_IN_METERS_PER_SECOND;
            }
            float decreasingSlope = (MIN_SPEED_IN_METERS_PER_SECOND - MAX_SPEED_IN_METERS_PER_SECOND) / (1 - FRACTION_TO_SLOW_DOWN);
            float decreasingSpeed = decreasingSlope * (travelFraction - 1) + MIN_SPEED_IN_METERS_PER_SECOND; 
            return Mathf.Max(decreasingSpeed, MIN_SPEED_IN_METERS_PER_SECOND);
        }
    }
}
