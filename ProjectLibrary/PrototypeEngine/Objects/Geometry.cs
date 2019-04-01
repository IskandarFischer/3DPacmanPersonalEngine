using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace PrototypeEngine.Objects
{
    public class Geometry
    {
        List<float> vertices = new List<float>();
        List<int> indices = new List<int>();
        int numberOfTriangles;
        int numberOfIndices;

        // Graphics
        private int vao_Handle;
        private int vbo_verts;
        private int[] mVertexBufferObjectIDArray = new int[2];

        public Geometry()
        {
        }


        void LoadFromBIN(string pModelFile)
        {
            GL.GenVertexArrays(1, out vao_Handle);
            GL.BindVertexArray(vao_Handle);
            GL.GenBuffers(2, mVertexBufferObjectIDArray);

            BinaryReader reader = new BinaryReader(new FileStream(pModelFile, FileMode.Open));

            int numberOfVertices = reader.ReadInt32();
            int floatsPerVertex = 6;

            float[] vert = new float[numberOfVertices * floatsPerVertex];

            byte[] byteArray = new byte[vert.Length * sizeof(float)];
            byteArray = reader.ReadBytes(byteArray.Length);

            Buffer.BlockCopy(byteArray, 0, vert, 0, byteArray.Length);
            vertices = vert.ToList();

            int numberOfTriangles = reader.ReadInt32();

            int[] indice = new int[numberOfTriangles * 3];

            byteArray = new byte[indice.Length * sizeof(int)];
            byteArray = reader.ReadBytes(indice.Length * sizeof(int));
            Buffer.BlockCopy(byteArray, 0, indice, 0, byteArray.Length);

            indices = indice.ToList();

            reader.Close();

            GL.BindBuffer(BufferTarget.ArrayBuffer, mVertexBufferObjectIDArray[0]);
            GL.BufferData<float>(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Count * sizeof(float)), vertices.ToArray<float>(), BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, mVertexBufferObjectIDArray[1]);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Count() * sizeof(float)), indices.ToArray<int>(), BufferUsageHint.StaticDraw);

            // Positions
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

            // Tex Coords
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));

            GL.BindVertexArray(0);

        }


        public void LoadObject(string filename)
        {
            string line;

            string extension = filename.Substring(filename.IndexOf('.'));

            if (extension == ".bin")
            {
                LoadFromBIN(filename);
                return;
            }

            try
            {
                FileStream fin = File.OpenRead(filename);
                StreamReader sr = new StreamReader(fin);

                GL.GenVertexArrays(1, out vao_Handle);
                GL.BindVertexArray(vao_Handle);
                GL.GenBuffers(2, mVertexBufferObjectIDArray);

                bool verticesParsed = false;

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    string[] values = line.Split(',');

                    if (values[0].StartsWith("NUM_OF_TRIANGLES"))
                    {
                        numberOfTriangles = int.Parse(values[0].Remove(0, "NUM_OF_TRIANGLES".Length));
                        continue;
                    }
                    else if (values[0].StartsWith("NUM_OF_INDICES"))
                    {
                        verticesParsed = true;
                        numberOfIndices = int.Parse(values[0].Remove(0, "NUM_OF_INDICES".Length));
                        continue;
                    }
                    if (values[0].StartsWith("//") || values.Length < 3) continue;

                    if (verticesParsed == false)
                    {
                        vertices.Add(float.Parse(values[0]));
                        vertices.Add(float.Parse(values[1]));
                        vertices.Add(float.Parse(values[2]));
                        vertices.Add(float.Parse(values[3]));
                        vertices.Add(float.Parse(values[4]));
                    }
                    else
                    {
                        indices.Add(int.Parse(values[0]));
                        indices.Add(int.Parse(values[1]));
                        indices.Add(int.Parse(values[2]));
                    }
                    //if (values[0].StartsWith("//") || values.Length < 3) continue;
                }

                GL.BindBuffer(BufferTarget.ArrayBuffer, mVertexBufferObjectIDArray[0]);
                GL.BufferData<float>(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Count * sizeof(float)), vertices.ToArray<float>(), BufferUsageHint.StaticDraw);

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, mVertexBufferObjectIDArray[1]);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Count() * sizeof(float)), indices.ToArray<int>(), BufferUsageHint.StaticDraw);

                // Positions
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * 4, 0);

                // Tex Coords
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * 4, 3 * 4);

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
            GL.BindBuffer(BufferTarget.ArrayBuffer, mVertexBufferObjectIDArray[0]);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, mVertexBufferObjectIDArray[1]);

            GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);
        }
    }
}
