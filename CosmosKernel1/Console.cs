using Cosmos.System;
using System.Drawing;

namespace CosmosKernel1
{
    public class Console : App
    {
        int textEachLine;
        public string text = string.Empty;
        public string _text = string.Empty;
        int Lines { get => _text.Split('\n').Length; }

        public Console(uint width, uint height, uint x = 0, uint y = 0) : base(width, height, x, y)
        {
            //ASC16 = 16*8
            textEachLine = (int)width / 8;
            name = "Console";
        }

        public override void _Update()
        {
            KeyEvent keyEvent;
            if (KeyboardManager.TryReadKey(out keyEvent))
            {
                switch (keyEvent.Key)
                {
                    case ConsoleKeyEx.Enter:
                        this.text += "\n";
                        break;
                    case ConsoleKeyEx.Backspace:
                        if (this.text.Length != 0)
                        {
                            this.text = this.text.Remove(this.text.Length - 1);
                        }
                        break;
                    default:
                        this.text += keyEvent.KeyChar;
                        break;
                }
            }

            Kernel.canvas.DrawFilledRectangle(Kernel.BlackPen, (int)x, (int)y, (int)width, (int)height);

            Kernel.canvas.DrawString(text + "_", Kernel.font, Kernel.WhitePen, (int)x, (int)y);
        }
    }
}
