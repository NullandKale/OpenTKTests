using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace OpenTKTest1
{
    class Game
    {
        public static GameWindow window;
        public static int tick = 0;
        public static KeyboardManager Keyboard;
        public static Queue<Action> renderQueue = new Queue<Action>();

        private Matrix4 projMatrix;

        //Testing
        quad t0;
        quad atlasTest;
        TextureAtlas Tatlas;
        int temp;


        public Game(GameWindow _window)
        {
            window = _window;

            window.Load += window_Load;
            window.UpdateFrame += window_UpdateFrame;
            window.RenderFrame += window_RenderFrame;
            Keyboard = new KeyboardManager();
        }

        void window_Load(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.AlphaTest);
            GL.AlphaFunc(AlphaFunction.Gequal, 0.5f);

            t0 = new quad("Content/roguelikeCharBeard_transparent.png");
            t0.pos.xPos = 600;
            t0.pos.yPos = 100;

            Tatlas = new TextureAtlas("Content/roguelikeDungeon_transparent.png", 29, 18, 16, 1);

            atlasTest = new quad(Tatlas, 1);
            atlasTest.components.Add(new KeyboardControl(5));

            projMatrix = Matrix4.CreateOrthographicOffCenter(0, window.Width, window.Height, 0, 0, 1);
        }

        void window_UpdateFrame(object sender, FrameEventArgs e)
        {
            if(tick == 19)
            {
                temp++;
                if(temp > 522)
                {
                    temp = 0;
                }
                atlasTest.tex = Tatlas.getTile(temp);
            }
        }

        void window_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.ClearColor(Color.CornflowerBlue);
            GL.ClearDepth(1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projMatrix);

            while (renderQueue.Count != 0)
            {
                renderQueue.Dequeue().Invoke();
            }

            window.SwapBuffers();

            //Do tick at END of frame.
            Tick();
        }

        void Tick()
        {
            tick++;
            if (tick > 19)
            {
                tick = 0;
            }
        }
    }
}
