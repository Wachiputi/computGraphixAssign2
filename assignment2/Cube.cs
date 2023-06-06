using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Assignment2
{
    class Cube{
        private Vector3 position;
        public Vector3 position{
            get {
                return position;
                }
            set {
                position = value;
            }
        }
        private Vector3 rotation;
        public Vector3 Rotation{
            get {
                return rotation;
            }
            set{
                rotation = value;
            }
        }
        private Color4 color;
        public Cube(Color4 color, Vector3 position){
            this.color = color;
            this.position = position;
        }
        public void Render(){
            GL.PushMatrix();

            //Apply position and transformations
            GL.Translate(position);
            GL.Rotate(rotation.X, Vector3.UnitX);
            GL.Rotate(rotation.Y, Vector3.UnitY);
            GL/Rotate(rotation.Z, Vector3.UnitZ);

            //render the cube
            GL.Begin(PrimitiveType.Quads);

            GL.Color4(color);

            //front face 
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f,1.0f,1.0f);
            GL.Vertex3(-1.0f, 1.0f,1.0f);

            ////other faces defined here

            GL.End();

            GLPopMatrix();
        }
    }
}