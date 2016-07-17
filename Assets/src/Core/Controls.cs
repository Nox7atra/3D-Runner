using UnityEngine;
using System.Collections;
namespace Runner.Core
{
    public static class Controls
    {
        private const float NORMAL_COEF_FOR_GYRO = 5f;
        private static float _InitialOrientationZ;

        public static void InitGyro()
        {
            if (Application.platform == RuntimePlatform.Android && Input.gyro != null)
            {
                // Activate the gyroscope
                Input.gyro.enabled = true;
                // Save the firsts values
                _InitialOrientationZ = 0;
            }
        }
        public static void SideSpeedControl(ref float speed)
        {

            if (Application.platform == RuntimePlatform.Android
                && Input.gyro != null)
            {
                _InitialOrientationZ += 
                    Input.gyro.rotationRateUnbiased.z * Time.deltaTime;
                speed= Mathf.Sign(_InitialOrientationZ)
                    * BalanceManager.Instance.SideSpeed
                    * Mathf.Lerp(0, 1, Mathf.Abs(_InitialOrientationZ))
                    * NORMAL_COEF_FOR_GYRO;
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.LeftArrow)
                    || Input.GetKeyUp(KeyCode.A)
                    || Input.GetKeyUp(KeyCode.RightArrow)
                    || Input.GetKeyUp(KeyCode.D))
                {
                    speed = 0;
                }
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    speed = BalanceManager.Instance.SideSpeed;
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    speed = -BalanceManager.Instance.SideSpeed;
                }
            }
        }
    }
}