using System.Collections.Generic;
using OpenTK;
using System.Drawing;


// ReSharper disable once CheckNamespace
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

    public static void Move<T>(this IList<T> list, int iIndexToMove,
        MoveDirection direction)
    {

        if (direction == MoveDirection.Up)
        {
            if (iIndexToMove == 0) return;
            var old = list[iIndexToMove - 1];
            list[iIndexToMove - 1] = list[iIndexToMove];
            list[iIndexToMove] = old;
        }
        else
        {
            if (iIndexToMove == list.Count - 1) return;
            var old = list[iIndexToMove + 1];
            list[iIndexToMove + 1] = list[iIndexToMove];
            list[iIndexToMove] = old;
        }
    }
}

public enum MoveDirection
{
    Up,
    Down
}