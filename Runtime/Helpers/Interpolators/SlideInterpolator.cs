using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    public class SlideInterpolator : Interpolator<Vector3>
    {
        Vector3 origin;
        Vector3 target;
        Vector3 direction;
        Vector3 currentPosition;
        float totalDistance;
        float MAX_SPEED_IN_METERS_PER_SECOND = 4f;
        float MIN_SPEED_IN_METERS_PER_SECOND = 0.1f;
        float FRACTION_TO_MAX_SPEED = 0.3f;
        float FRACTION_TO_SLOW_DOWN = 0.9f;

        public void DefineOriginAndTarget(Vector3 originalPostion, Vector3 targetPostion)
        {
            origin = originalPostion;
            target = targetPostion;
            currentPosition = originalPostion;
            totalDistance = Vector3.Distance(origin, target);
        }

        public Vector3 Interpolate(Vector3 currentValue)
        {
            direction = Vector3.Normalize(target - currentValue);
            float traveledDistance = Vector3.Distance(currentValue, origin);
            float traveledFraction = traveledDistance / totalDistance;
            float speed = GetSpeed(traveledFraction);
            currentPosition += direction * speed * Time.deltaTime;
            return currentPosition;
        }

        private float GetSpeed(float travelFraction)
        {
            if (travelFraction < FRACTION_TO_MAX_SPEED)
            {
                float growingSpeed = MAX_SPEED_IN_METERS_PER_SECOND * travelFraction / FRACTION_TO_MAX_SPEED;
                return Mathf.Max(growingSpeed, MIN_SPEED_IN_METERS_PER_SECOND);
            }
            if (travelFraction < FRACTION_TO_SLOW_DOWN)
            {
                return MAX_SPEED_IN_METERS_PER_SECOND;
            }
            float decreasingSpeed = -1 * MAX_SPEED_IN_METERS_PER_SECOND * (travelFraction - 1) / (1 - FRACTION_TO_SLOW_DOWN);
            return Mathf.Max(decreasingSpeed, MIN_SPEED_IN_METERS_PER_SECOND);
        }
    }
}
