using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Assignment2{
    class Program : GameWindow{

        private Cube redCube;
        private Cube yellowCube;
        private Vector3 cameraPosition;
        private float cameraYaw;
        private float cameraPitch;

        public Program(): base(new GameWindowSettings()){
            Title = "Graphics programs";
        }
        protected override void Onload(){
            redCube = new Cube(Color4.Red, Color3.Zero);
            yellowCube = new Cube(Color4.Yellow, new Vector3(0,0,5));
            cameraPosition = new Vector3(0,0,10);

            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void ONupdateFrame(FrameEventArgs e){
         //Rotate the cube
         redCube.Rotation.Y += 0.01f;
         
         //orbit the yellow cube around the red cube
         float orbitRadius = 5f;
         float orbitSpeed = 0.02f;
         yellowCube.Position.X = redCube.Position.X + (float)Math.Sin(orbitSpeed)* orbitRadius;
         yellowCube.Position.Z = redCube.Position.Z + (float)Math.Cos(orbitSpeed)* orbitRadius;
         orbitSpeed += 0.02f;

         //move the camera based on the keyboard input
         KeyboardState keyboard = keyboard.GetState();
         Vector3 cameraMovement = Vector3.Zero;

         if(keyboard.IsKeyDown(Key.W))
            cameraMovement += Vector3.UnitY;
         if(keyboard.IsKeyDown(Key.S))
            cameraMovement -+ Vector3.UnitY;
         if(keyboard.IsKeyDown(Key.A))
            cameraMovement -+ Vector3.UnitX;
         if(keyboard.IsKeyDown(Key.D))
            cameraMovement +=Vector3.X;

         cameraPosition += cameraMovement*0.1f;

         // Rotate the camera based on the mouse input
         MouseState mouse = Mouse.GetCursorState();
         int deltaX = mouse.X - Size.X/2;
         int deltaY = mouse.Y - Size.Y/2;

         cameraYaw += deltaX*0.01f;
         cameraPitch += deltaY*0.01f;

         //reset the mouse position to the centre of the window
         Mouse.SetPosition(Size.X /2, Size.Y/2);

         //update the view matirx
         Matrix4 viewMatrix = Matrix4.LookAt(cameraPosition, Vector3.Zero, Vector3.UnitY);
         GL.MatrixModel(MatrixMode.Modelview);
         GL.LoadMatrix(ref viewMatrix);
        }

        protected override void ONupdateFrame(FrameEventArgs e){
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //Render the red cube
            redCube.Render();

            //Render the yellow cube
            yellowCube.Render();

            SwapBuffers();
        }
        static void Main(string []args){
            using (Program program = new Program()){
                program.Run();
            }
        }

    }
}