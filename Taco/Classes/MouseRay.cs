using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Taco.Classes
{
    public class MouseRay
    {
        private Vector3 _start;
        private Vector3 _end;
        private Vector3 _direction;

        public Vector3 Start { get { return _start; } }

        public Vector3 End { get { return _end; } }

        public Vector3 Direction { get { return _direction; } }

        public string StartString { get { return _start.X.ToString() + ", " + _start.Y.ToString() + ", " + _start.Z.ToString(); } }

        public string EndString { get { return _end.X.ToString() + ", " + _end.Y.ToString() + ", " + _end.Z.ToString(); } }

        public string DirectionString { get { return _direction.X.ToString() + ", " + _direction.Y.ToString() + ", " + _direction.Z.ToString(); } }

        public MouseRay(int x, int y, Matrix4 modelView, Matrix4 projection)
        {
            int[] viewport = new int[4];
            Matrix4 modelMatrix, projMatrix;

            GL.GetFloat(GetPName.ModelviewMatrix, out modelMatrix);
            GL.GetFloat(GetPName.ProjectionMatrix, out projMatrix);
            GL.GetInteger(GetPName.Viewport, viewport);

            _start = new Vector3(x, y, 0.0f).UnProject(projection, modelView, new Size(viewport[2], viewport[3]));
            _end = new Vector3(x, y, 1.0f).UnProject(projection, modelView, new Size(viewport[2], viewport[3]));
            _direction = _end - _start;
            _direction.Normalize();
        }

        public float Intersection(Vector3 point, float radius)
        {

            var sphere = new BoundingSphere
            {
                Center = point,
                Radius = radius
            };


            Vector3 difference = sphere.Center - _start;
            float differenceLengthSquared = difference.LengthSquared;
            float sphereRadiusSquared = sphere.Radius * sphere.Radius;
            float distanceAlongRay;
            if (differenceLengthSquared < sphereRadiusSquared)
            {
                return 0f;
            }

            Vector3.Dot(ref _direction, ref difference, out distanceAlongRay);
            if (distanceAlongRay < 0)
            {
                return 0f;
            }

            float dist = sphereRadiusSquared + distanceAlongRay * distanceAlongRay - differenceLengthSquared;
            return (dist < 0) ? 0f : distanceAlongRay - (float)Math.Sqrt(dist);
        }

        public bool Intersects(Vector3 point, float radius)
        {
            var intersection = Intersection(point, radius);

            if (intersection < 0)
                return true;

            return (!(intersection <= 0));
        }
    }

    public struct BoundingSphere
    {
        public Vector3 Center;
        public float Radius;
    }

    public static class Extensions
    {
        public static Vector3 UnProject(this Vector3 mouse, Matrix4 projection, Matrix4 view, Size viewport)
        {
            Vector4 vec;

            vec.X = 2.0f * mouse.X / viewport.Width - 1;
            vec.Y = -(2.0f * mouse.Y / viewport.Height - 1);
            vec.Z = mouse.Z;
            vec.W = 1.0f;

            Matrix4 viewInv = Matrix4.Invert(view);
            Matrix4 projInv = Matrix4.Invert(projection);

            Vector4.Transform(ref vec, ref projInv, out vec);
            Vector4.Transform(ref vec, ref viewInv, out vec);

            if (vec.W > 0.000001f || vec.W < -0.000001f)
            {
                vec.X /= vec.W;
                vec.Y /= vec.W;
                vec.Z /= vec.W;
            }

            return vec.Xyz;
        }
    }
}

//using System;
//using OpenTK;
//using OpenTK.Graphics.OpenGL;
//using System.Drawing;

//namespace Taco.Classes
//{
//    public class MouseRay
//    {
//        private Vector3 _start;
//        private Vector3 _end;
//        private Vector3 _direction;

//        public Vector3 Start { get { return _start; } }

//        public Vector3 End { get { return _end; } }

//        public Vector3 Direction { get { return _direction; } }

//        public string StartString { get { return _start.X.ToString() + ", " + _start.Y.ToString() + ", " + _start.Z.ToString(); } }

//        public string EndString { get { return _end.X.ToString() + ", " + _end.Y.ToString() + ", " + _end.Z.ToString(); } }

//        public string DirectionString { get { return _direction.X.ToString() + ", " + _direction.Y.ToString() + ", " + _direction.Z.ToString(); } }

//        public MouseRay(int x, int y, Matrix4 modelView, Matrix4 projection)
//        {
//            int[] viewport = new int[4];
//            Matrix4 modelMatrix, projMatrix;

//            GL.GetFloat(GetPName.ModelviewMatrix, out modelMatrix);
//            GL.GetFloat(GetPName.ProjectionMatrix, out projMatrix);
//            GL.GetInteger(GetPName.Viewport, viewport);

//            _start = new Vector3(x, y, 0.0f).UnProject(projection, modelView, new Size(viewport[2], viewport[3]));
//            _end = new Vector3(x, y, 1.0f).UnProject(projection, modelView, new Size(viewport[2], viewport[3]));
//            _direction = _end - _start;
//            _direction.Normalize();
//        }

//        public float Intersection(Vector3 point, float radius)
//        {

//            BoundingSphere sphere = new BoundingSphere
//            {
//                Center = point,
//                Radius = radius
//            };


//            var difference = sphere.Center - _start;
//            var differenceLengthSquared = difference.LengthSquared;
//            var sphereRadiusSquared = sphere.Radius * sphere.Radius;
//            float distanceAlongRay;
//            if (differenceLengthSquared < sphereRadiusSquared)
//            {
//                return 0f;
//            }

//            Vector3.Dot(ref _direction, ref difference, out distanceAlongRay);
//            if (distanceAlongRay < 0)
//            {
//                return 0f;
//            }

//            var dist = sphereRadiusSquared + distanceAlongRay * distanceAlongRay - differenceLengthSquared;
//            return (dist < 0) ? 0f : distanceAlongRay - (float)Math.Sqrt(dist);
//        }

//        public bool Intersects(Vector3 point, float radius)
//        {
//            var intersection = Intersection(point, radius);

//            if (intersection < 0)
//                return true;

//            return false;
//        }
//    }

//    public struct BoundingSphere
//    {
//        public Vector3 Center;
//        public float Radius;
//    }

//    public static class Extensions
//    {
//        public static Vector3 UnProject(this Vector3 mouse, Matrix4 projection, Matrix4 view, Size viewport)
//        {
//            Vector4 vec;

//            vec.X = 2.0f * mouse.X / viewport.Width - 1;
//            vec.Y = -(2.0f * mouse.Y / viewport.Height - 1);
//            vec.Z = mouse.Z;
//            vec.W = 1.0f;

//            Matrix4 viewInv = Matrix4.Invert(view);
//            Matrix4 projInv = Matrix4.Invert(projection);

//            Vector4.Transform(ref vec, ref projInv, out vec);
//            Vector4.Transform(ref vec, ref viewInv, out vec);

//            if (vec.W > 0.000001f || vec.W < -0.000001f)
//            {
//                vec.X /= vec.W;
//                vec.Y /= vec.W;
//                vec.Z /= vec.W;
//            }

//            return vec.Xyz;
//        }
//    }
//}
