using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGL_Game.Objects
{
    public class Geometry
    {
        List<float> vertices = new List<float>();
        int numberOfTriangles;

        // Graphics
        private int vao_Handle;
        private int vbo_verts;

        public Geometry()
        {
        }

        public void LoadObject(string filename)
        {
            string line;

            try
            {
                FileStream fin = File.OpenRead(filename);
                StreamReader sr = new StreamReader(fin);

                GL.GenVertexArrays(1, out vao_Handle);
                GL.BindVertexArray(vao_Handle);
                GL.GenBuffers(1, out vbo_verts);

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    string[] values = line.Split(',');

                    if (values[0].StartsWith("NUM_OF_TRIANGLES"))
                    {
                        numberOfTriangles = int.Parse(values[0].Remove(0, "NUM_OF_TRIANGLES".Length));
                        continue;
                    }
                    if (values[0].StartsWith("//") || values.Length < 5) continue;

                    vertices.Add(float.Parse(values[0]));
                    vertices.Add(float.Parse(values[1]));
                    vertices.Add(float.Parse(values[2]));
                    vertices.Add(float.Parse(values[3]));
                    vertices.Add(float.Parse(values[4]));
                }

                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_verts);
                GL.BufferData<float>(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Count * 4), vertices.ToArray<float>(), BufferUsageHint.StaticDraw);

                // Positions
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5*4, 0);

                // Tex Coords
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5*4, 3*4);

                GL.BindVertexArray(0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Render()
        {
            GL.BindVertexArray(vao_Handle);
            GL.DrawArrays(PrimitiveType.Triangles, 0, numberOfTriangles * 3);
        }
    }
}
