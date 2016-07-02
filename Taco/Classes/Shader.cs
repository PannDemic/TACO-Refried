using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Taco.Classes
{
    /// <summary>
    /// Shader Class (Vertex Shader and Fragment Shader)
    /// </summary>
    public class Shader : IDisposable
    {

        /// <summary>
        /// Type of Shader
        /// </summary>
        public enum Type
        {
            Vertex = 0x1,
            Fragment = 0x2
        }

        /// <summary>
        /// Get Whether the Shader function is Available on this Machine or not
        /// </summary>
        public static bool IsSupported
        {
            get
            {
                return (new Version(GL.GetString(StringName.Version).Substring(0, 3)) >= new Version(2, 0));
            }
        }

        private int _program;

        public uint ShaderHandle
        {
            get { return (uint) _program; }
        }
        private Dictionary<string, int> _variables = new Dictionary<string, int>();

        /// <summary>
        /// Create a new Shader
        /// </summary>
        /// <param name="source">Vertex or Fragment Source</param>
        /// <param name="type">Type of Source Code</param>
        public Shader(string source, Type type)
        {
            if (!IsSupported)
            {
                Console.WriteLine("Failed to create Shader." +
                    Environment.NewLine + "Your system doesn't support Shader.", "Error");
                return;
            }

            if (type == Type.Vertex)
                Compile(source);
            else
                Compile("", source);
        }

        /// <summary>
        /// Create a new Shader
        /// </summary>
        /// <param name="vsource">Vertex Source</param>
        /// <param name="fsource">Fragment Source</param>
        public Shader(string vsource, string fsource)
        {
            if (!IsSupported)
            {
                Console.WriteLine("Failed to create Shader." +
                    Environment.NewLine + "Your system doesn't support Shader.", "Error");
                return;
            }

            Compile(vsource, fsource);
        }

        // I prefer to return the bool rather than throwing an exception lol
        private bool Compile(string vertexSource = "", string fragmentSource = "")
        {
            int statusCode = -1;
            string info = "";

            if (vertexSource == "" && fragmentSource == "")
            {
                Console.WriteLine("Failed to compile Shader." +
                    Environment.NewLine + "Nothing to Compile.", "Error");
                return false;
            }

            if (_program > 0)
                Renderer.Call(() => GL.DeleteProgram(_program));

            _variables.Clear();

            _program = GL.CreateProgram();

            if (vertexSource != "")
            {
                int vertexShader = GL.CreateShader(ShaderType.VertexShader);
                Renderer.Call(() => GL.ShaderSource(vertexShader, vertexSource));
                Renderer.Call(() => GL.CompileShader(vertexShader));
                Renderer.Call(() => GL.GetShaderInfoLog(vertexShader, out info));
                Renderer.Call(() => GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out statusCode));

                if (statusCode != 1)
                {
                    Console.WriteLine("Failed to Compile Vertex Shader Source." +
                        Environment.NewLine + info + Environment.NewLine + "Status Code: " + statusCode.ToString());

                    Renderer.Call(() => GL.DeleteShader(vertexShader));
                    Renderer.Call(() => GL.DeleteProgram(_program));
                    _program = 0;

                    return false;
                }

                Renderer.Call(() => GL.AttachShader(_program, vertexShader));
                Renderer.Call(() => GL.DeleteShader(vertexShader));
            }

            if (fragmentSource != "")
            {
                int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
                Renderer.Call(() => GL.ShaderSource(fragmentShader, fragmentSource));
                Renderer.Call(() => GL.CompileShader(fragmentShader));
                Renderer.Call(() => GL.GetShaderInfoLog(fragmentShader, out info));
                Renderer.Call(() => GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out statusCode));

                if (statusCode != 1)
                {
                    Console.WriteLine("Failed to Compile Fragment Shader Source." +
                        Environment.NewLine + info + Environment.NewLine + "Status Code: " + statusCode.ToString());

                    Renderer.Call(() => GL.DeleteShader(fragmentShader));
                    Renderer.Call(() => GL.DeleteProgram(_program));
                    _program = 0;

                    return false;
                }

                Renderer.Call(() => GL.AttachShader(_program, fragmentShader));
                Renderer.Call(() => GL.DeleteShader(fragmentShader));
            }

            Renderer.Call(() => GL.LinkProgram(_program));
            Renderer.Call(() => GL.GetProgramInfoLog(_program, out info));
            Renderer.Call(() => GL.GetProgram(_program, GetProgramParameterName.LinkStatus, out statusCode));

            if (statusCode != 1)
            {
                Console.WriteLine("Failed to Link Shader Program." +
                    Environment.NewLine + info + Environment.NewLine + "Status Code: " + statusCode.ToString());

                Renderer.Call(() => GL.DeleteProgram(_program));
                _program = 0;

                return false;
            }

            return true;
        }

        private int GetVariableLocation(string name)
        {
            if (_variables.ContainsKey(name))
                return _variables[name];


            int location = GL.GetUniformLocation(_program, name);

            if (location != -1)
                _variables.Add(name, location);
            else
                Console.WriteLine("Failed to retrieve Variable Location." + name);
            //Environment.NewLine + "Variable Name not found.", "Error");

            return location;
        }

        /// <summary>
        /// Change a value Variable of the Shader
        /// </summary>
        /// <param name="name">Variable Name</param>
        /// <param name="x">Value</param>
        public void SetVariable(string name, int x)
        {
            if (_program > 0)
            {
                Renderer.Call(() => GL.UseProgram(_program));

                int location = GetVariableLocation(name);
                if (location != -1)
                    Renderer.Call(() => GL.Uniform1(location, x));

                Renderer.Call(() => GL.UseProgram(0));
            }
        }

        /// <summary>
        /// Change a value Variable of the Shader
        /// </summary>
        /// <param name="name">Variable Name</param>
        /// <param name="x">Value</param>
        public void SetVariable(string name, int[] x)
        {
            if (_program > 0)
            {
                Renderer.Call(() => GL.UseProgram(_program));

                int location = GetVariableLocation(name);
                if (location != -1)
                    Renderer.Call(() => GL.Uniform1(location, x.Length, x));

                Renderer.Call(() => GL.UseProgram(0));
            }
        }

        /// <summary>
        /// Change a value Variable of the Shader
        /// </summary>
        /// <param name="name">Variable Name</param>
        /// <param name="x">Value</param>
        public void SetVariable(string name, float x)
        {
            if (_program > 0)
            {
                Renderer.Call(() => GL.UseProgram(_program));

                int location = GetVariableLocation(name);
                if (location != -1)
                    Renderer.Call(() => GL.Uniform1(location, x));

                Renderer.Call(() => GL.UseProgram(0));
            }
        }

        /// <summary>
        /// Change a value Variable of the Shader
        /// </summary>
        /// <param name="name">Variable Name</param>
        /// <param name="x">Value</param>
        public void SetVariable(string name, float[] x)
        {
            if (_program > 0)
            {
                Renderer.Call(() => GL.UseProgram(_program));

                int location = GetVariableLocation(name);
                if (location != -1)
                    Renderer.Call(() => GL.Uniform1(location, x.Length, x));

                Renderer.Call(() => GL.UseProgram(0));
            }
        }

        /// <summary>
        /// Change a 2 value Vector Variable of the Shader
        /// </summary>
        /// <param name="name">Variable Name</param>
        /// <param name="x">First Vector Value</param>
        /// <param name="y">Second Vector Value</param>
        public void SetVariable(string name, float x, float y)
        {
            if (_program > 0)
            {
                Renderer.Call(() => GL.UseProgram(_program));

                int location = GetVariableLocation(name);
                if (location != -1)
                    Renderer.Call(() => GL.Uniform2(location, x, y));

                Renderer.Call(() => GL.UseProgram(0));
            }
        }

        /// <summary>
        /// Change a 3 value Vector Variable of the Shader
        /// </summary>
        /// <param name="name">Variable Name</param>
        /// <param name="x">First Vector Value</param>
        /// <param name="y">Second Vector Value</param>
        /// <param name="z">Third Vector Value</param>
        public void SetVariable(string name, float x, float y, float z)
        {
            if (_program > 0)
            {
                Renderer.Call(() => GL.UseProgram(_program));

                int location = GetVariableLocation(name);
                if (location != -1)
                    Renderer.Call(() => GL.Uniform3(location, x, y, z));

                Renderer.Call(() => GL.UseProgram(0));
            }
        }

        /// <summary>
        /// Change a 4 value Vector Variable of the Shader
        /// </summary>
        /// <param name="name">Variable Name</param>
        /// <param name="x">First Vector Value</param>
        /// <param name="y">Second Vector Value</param>
        /// <param name="z">Third Vector Value</param>
        /// <param name="w">Fourth Vector Value</param>
        public void SetVariable(string name, float x, float y, float z, float w)
        {
            if (_program > 0)
            {
                Renderer.Call(() => GL.UseProgram(_program));

                int location = GetVariableLocation(name);
                if (location != -1)
                    Renderer.Call(() => GL.Uniform4(location, x, y, z, w));

                Renderer.Call(() => GL.UseProgram(0));
            }
        }

        public void SetVariable(string name, Color4[] cols)
        {
            float[] colsuni = new float[cols.Length * 4];

            for (int i = 0; i < cols.Length; i++)
            {
                colsuni[i * 4 + 0] = cols[i].R;
                colsuni[i * 4 + 1] = cols[i].G;
                colsuni[i * 4 + 2] = cols[i].B;
                colsuni[i * 4 + 3] = cols[i].A;
            }

            SetVariable(name, colsuni);

        }

        /// <summary>
        /// Change a Matrix4 Variable of the Shader
        /// </summary>
        /// <param name="name">Variable Name</param>
        /// <param name="matrix">Matrix</param>
        public void SetVariable(string name, Matrix4 matrix)
        {
            if (_program > 0)
            {
                Renderer.Call(() => GL.UseProgram(_program));

                int location = GetVariableLocation(name);
                if (location != -1)
                {
                    // Well cannot use ref on lambda expression Lol
                    // So we need to call Check error manually
                    GL.UniformMatrix4(location, false, ref matrix);
                    Renderer.CheckError();
                }

                Renderer.Call(() => GL.UseProgram(0));
            }
        }

        /// <summary>
        /// Change a 2 value Vector Variable of the Shader
        /// </summary>
        /// <param name="name">Variable Name</param>
        /// <param name="vector">Vector Value</param>
        public void SetVariable(string name, Vector2 vector)
        {
            SetVariable(name, vector.X, vector.Y);
        }

        /// <summary>
        /// Change a 3 value Vector Variable of the Shader
        /// </summary>
        /// <param name="name">Variable Name</param>
        /// <param name="vector">Vector Value</param>
        public void SetVariable(string name, Vector3 vector)
        {
            SetVariable(name, vector.X, vector.Y, vector.Z);
        }

        /// <summary>
        /// Change a Color Variable of the Shader
        /// </summary>
        /// <param name="name">Variable Name</param>
        /// <param name="color">Color Value</param>
        public void SetVariable(string name, Color color)
        {
            SetVariable(name, color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
        }

        /// <summary>
        /// Bind a Shader for Rendering
        /// </summary>
        /// <param name="shader">Shader to bind</param>
        public static void Bind(Shader shader)
        {
            if (shader != null && shader._program > 0)
            {
                Renderer.Call(() => GL.UseProgram(shader._program));
            }
            else
            {
                Renderer.Call(() => GL.UseProgram(0));
            }
        }

        public void BindTexture(int textureId, TextureUnit textureUnit, string name)
        {
            if (_program > 0)
            {
                Renderer.Call(() => GL.UseProgram(_program));

                int location = GetVariableLocation(name);
                if (location != -1)
                {
                    Renderer.Call(() => GL.ActiveTexture(textureUnit));
                    Renderer.Call(() => GL.BindTexture(TextureTarget.Texture2D, textureId));
                    Renderer.Call(() => GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear));
                    Renderer.Call(() => GL.Uniform1(GL.GetUniformLocation(_program, name), textureUnit - TextureUnit.Texture0));
                }
            }
        }

        public void Dispose()
        {
            if (_program != 0)
                Renderer.Call(() => GL.DeleteProgram(_program));
        }
    }
}
