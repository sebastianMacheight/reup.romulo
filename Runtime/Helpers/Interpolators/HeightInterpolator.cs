using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    public class HeightInterpolator : Interpolator<float>
    {
        float origin;
        float target;
        float MAX_SPEED_IN_METERS_PER_SECOND = 3f;
        float MIN_SPEED_IN_METERS_PER_SECOND = 0.2f;
        float FRACTION_TO_MAX_SPEED = 0.2f;
        float FRACTION_TO_MIN_SPEED = 0.6f;
        SpeedHandler speedHandler;

        public void DefineOriginAndTarget(Vector3 originalPostion, float targetHeight)
        {
            origin = originalPostion.y;
            target = targetHeight;
            var totalDistance = Mathf.Abs(target - origin);
            speedHandler = new FractionalEaseInOutSpeedHandler(totalDistance,
                MAX_SPEED_IN_METERS_PER_SECOND,
                MIN_SPEED_IN_METERS_PER_SECOND,
                FRACTION_TO_MAX_SPEED,
                FRACTION_TO_MIN_SPEED);
        }

        public Vector3 Interpolate(Vector3 currentPosition)
        {
            var currentHeight = currentPosition.y;
            //Debug.Log($"in Height interpolate in is {currentHeight}");
            //Debug.Log($"in Height interpolate origin is {origin}");
            var traveledDistance = Mathf.Abs(currentHeight - origin);
            var speed = speedHandler.GetSpeedInMetersPerSecond(traveledDistance);
            //Debug.Log($"height change speed: {speed}");
            var direction = getDirection(currentHeight);
            //Debug.Log($"direction is {direction}");
            var addition = direction * speed * Time.deltaTime;
            //Debug.Log($"addition {addition}");
            currentHeight += direction * speed * Time.deltaTime;
            //Debug.Log($"at the end in Height interpolate out is {currentHeight}");
            var newPosition = new Vector3(currentPosition.x, currentHeight, currentPosition.z);
            return newPosition;
        }

        private int getDirection(float currentHeight)
        {
            if ( currentHeight >= target)
            {
                return -1;
            }
            return 1;
        }
    }
}
