using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjektTestowy.CustomClasses
{
    public class Camera
    {
        public Vector3 camTarget;
        public Vector3 camPosition;
        public Matrix projectionMatrix { get; set; }
        public Matrix viewMatrix { get; set; }
        public Matrix worldMatrix { get; set; }

        public Vector3 Rotation { get; set; }

        public Camera(GraphicsDevice graphicsDevice)
        {
            camTarget = new Vector3(0, 0, 0);
            camPosition = new Vector3(0, 0, -100);
            Rotation = new Vector3();

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), graphicsDevice.DisplayMode.AspectRatio, 1, 1000);
            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, Vector3.Up);
            worldMatrix = Matrix.CreateWorld(camTarget, Vector3.Forward, Vector3.Up);
        }
        public void CalculateMatrix()
        {
            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, Vector3.Up);
        }
        public void UpdateView(BasicEffect basicEffect)
        {
            basicEffect.Projection = projectionMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.World = worldMatrix;
        }

        // Sterowanie pozycją i obrotem !


        public void Rotate(Vector3 degrees)
        {
            Matrix rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(degrees.Y));
            camPosition = Vector3.Transform(camPosition, rotationMatrix);
        }
        
        public void MoveCamera(Vector3 position)
        {
            camPosition += position;
            camTarget += position;
        }

        public void SetTarget(Vector3 position)
        {
            camTarget += position;
        }

        public void SetCameraPosition(Vector3 newPosition, Vector3 newTarget)
        {
            camTarget = newPosition;
            camPosition = newTarget;
        }
    }
}
