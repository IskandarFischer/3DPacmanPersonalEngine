using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using PrototypeEngine.Components;
using PrototypeEngine.Objects;
using PrototypeEngine.Scenes;

namespace PrototypeEngine.Systems
{
    public class SystemRender : SystemBase
    {
        protected int pgmID;
        protected int vsID;
        protected int fsID;
        protected int attribute_vtex;
        protected int attribute_vpos;
        protected int uniform_stex;
        protected int uniform_mview;

        public SystemRender()
        {
            pgmID = GL.CreateProgram();
            LoadShader("Shaders/vs.glsl", ShaderType.VertexShader, pgmID, out vsID);
            LoadShader("Shaders/fs.glsl", ShaderType.FragmentShader, pgmID, out fsID);
            GL.LinkProgram(pgmID);
            Console.WriteLine(GL.GetProgramInfoLog(pgmID));

            attribute_vpos = GL.GetAttribLocation(pgmID, "a_Position");
            attribute_vtex = GL.GetAttribLocation(pgmID, "a_TexCoord");
            uniform_mview = GL.GetUniformLocation(pgmID, "WorldViewProj");
            uniform_stex = GL.GetUniformLocation(pgmID, "s_texture");

            if (attribute_vpos == -1 || attribute_vtex == -1 || uniform_stex == -1 || uniform_mview == -1)
            {
                Console.WriteLine("Error binding attributes");
            }
        }

        void LoadShader(String filename, ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);
            using (StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }

        public string Name
        {
            get { return "SystemRender"; }
        }

        public void Draw(Matrix4 world, Geometry geometry, int texture, bool isTexture = false)
        {
            GL.UseProgram(pgmID);

            GL.Uniform1(uniform_stex, 0);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.Enable(EnableCap.Texture2D);

            Matrix4 worldView;
            if (ComponentCamera.MainCamera != null)
                worldView = Matrix4.LookAt(ComponentCamera.MainCamera.Transform.Position + ComponentCamera.MainCamera.PositionFix, ComponentCamera.MainCamera.Transform.Position + ComponentCamera.MainCamera.PositionFix + ComponentCamera.MainCamera.Transform.Forward, ComponentCamera.MainCamera.Transform.Up);
            else
                worldView = Matrix4.LookAt(new Vector3(0, 0, 3), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            //Fallback in case of no Main Camera 

            Matrix4 worldViewProjection;

            if (isTexture)
                worldViewProjection = world;
            else
                worldViewProjection = world * worldView * Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), 800f / 480f, 0.01f, 100f);


            GL.UniformMatrix4(uniform_mview, false, ref worldViewProjection);

            geometry.Render();

            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }

        public override void OnAction(Entity entity)
        {
            bool skybox = entity.GetComponent<ComponentSkybox>() != null;

            var renderer = entity.GetComponent<ComponentRenderer>();
            Geometry geometry = renderer.Geometry;
            int texture = renderer.Texture;
            Matrix4 world = entity.Transform.ObjectTransform;

            RepeatTexture(renderer);

            if (skybox)
                InvertDraw();

            Draw(world, geometry, texture, renderer.ComponentUI);

            if (skybox)
                ResetDraw();
        }

        void RepeatTexture(ComponentRenderer renderer)
        {
            if (renderer.RepeatTexture)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.Repeat);
            }
            else
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToEdge);
            }

        }


        void InvertDraw()
        {
            GL.CullFace(CullFaceMode.Front);
            GL.Disable(EnableCap.DepthTest);
        }

        void ResetDraw()
        {
            GL.CullFace(CullFaceMode.Back);
            GL.Enable(EnableCap.DepthTest);
        }
    }
}
