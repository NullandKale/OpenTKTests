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
        public static InputManager input;
        public static ButtonManager buttonMan;
        public static Queue<Action> renderQueue = new Queue<Action>();
        public static TextureAtlas font;

        private Matrix4 projMatrix;

        //Testing
        quad t0;
        quad atlasTest;
        TextureAtlas Tatlas;

        public Game(GameWindow w)
        {
            window = w;

            window.Load += window_Load;
            window.UpdateFrame += window_UpdateFrame;
            window.RenderFrame += window_RenderFrame;
            input = new InputManager();
            buttonMan = new ButtonManager();
            font = new TextureAtlas("Content/font.png", 16, 6, 8, 12, 0);
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

            Tatlas = new TextureAtlas("Content/roguelikeDungeon_transparent.png", 29, 18, 16, 16, 1);
            Button[,] tAtlasTestButtons = new Button[29,18];

            for(int i = 0; i < 29; i++)
            {
                int xPos = i * 48;
                for(int j = 0; j < 18; j++)
                {
                    int yPos = j * 48;
                    tAtlasTestButtons[i, j] = new Button(" ", Tatlas.getTile(j * 18 + i),"ID: " + (j * 18 + i), MouseButton.Left);
                    tAtlasTestButtons[i, j].SetPos(new Point(xPos + 20, yPos));
                }
            }            

            projMatrix = Matrix4.CreateOrthographicOffCenter(0, window.Width, window.Height, 0, 0, 1);
        }

        void window_UpdateFrame(object sender, FrameEventArgs e)
        {

        }

        void window_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.ClearColor(Color.DimGray);
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

        void PrintHello()
        {
            Console.WriteLine("Hello World!");
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
