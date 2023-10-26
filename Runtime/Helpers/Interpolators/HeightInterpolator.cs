using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    public class HeightInterpolator : Interpolator<float>
    {
        float current;
        float target;

        public void DefineOriginAndTarget(Vector3 originalPostion, float targetHeight)
        {
            current = originalPostion.y;
            target = targetHeight;
        }

        public Vector3 Interpolate(Vector3 currentValue)
        {
            //Debug.Log($"in Height interpolate in is {currentValue}");
            current += (target - current) * 10 * Time.deltaTime;
            var newPosition = new Vector3(currentValue.x, current, currentValue.z);
            //Debug.Log($"at the end in Height interpolate out is {newPosition}");
            return newPosition;

        }
    }
}
