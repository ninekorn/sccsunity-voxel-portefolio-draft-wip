//using SharpDX;
using System;
using UnityEditor;
using UnityEngine;




public class DCamera : MonoBehaviour              // 63 lines
{

    float angle;
    Vector3 axis;

    // Properties.
    private float PositionX { get; set; }
    private float PositionY { get; set; }
    private float PositionZ { get; set; }
    private float RotationX { get; set; }
    private float RotationY { get; set; }
    private float RotationZ { get; set; }
    public Matrix4x4 ViewMatrix { get; private set; }

    // Constructor
    public DCamera() { }

    // Methods.
    public void SetPosition(float x, float y, float z)
    {
        PositionX = x;
        PositionY = y;
        PositionZ = z;
    }
    public void SetRotation(float x, float y, float z)
    {
        RotationX = x;
        RotationY = y;
        RotationZ = z;
    }
    public Vector3 GetPosition()
    {
        return new Vector3(PositionX, PositionY, PositionZ);
    }
    public void Render()
    {
        // Setup the position of the camera in the world.
        Vector3 position = new Vector3(PositionX, PositionY, PositionZ);

        // Setup where the camera is looking  forwardby default.
        Vector3 lookAt = new Vector3(0, 0, 1.0f);

        // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
        float pitch = RotationX * 0.0174532925f;
        float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
        float roll = RotationZ * 0.0174532925f;
        

        /*this.transform.rotation.ToAngleAxis(out angle, out axis);


        Quaternion q = this.transform.rotation;

        float x = q.x;
        float y = q.x;
        float z = q.x;
        float w = q.x;

        //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
        float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
        float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
        float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);
        */


        //Vector3 lookatlocal = transform.TransformDirection(lookAt);


        //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
        transform.rotation *= Quaternion.Euler(pitch * Time.deltaTime, yaw * Time.deltaTime, roll * Time.deltaTime);
        //Matrix4x4.Rotate






        //// Create the rotation matrix from the yaw, pitch, and roll values.
        /*Matrix4x4 rotationMatrix = Matrix4x4.RotationYawPitchRoll(yaw, pitch, roll);

        // Transform the lookAt and up vector by the rotation matrix so the view is correctly rotated at the origin.
        lookAt = Vector3.TransformCoordinate(lookAt, rotationMatrix);
        Vector3 up = Vector3.TransformCoordinate(Vector3.UnitY, rotationMatrix);
        */





        // Translate the rotated camera position to the location of the viewer.
        //lookAt = position + lookAt;

        // Finally create the view matrix from the three updated vectors.
        //ViewMatrix = Matrix4x4.LookAtLH(position, lookAt, up);
    }
}
