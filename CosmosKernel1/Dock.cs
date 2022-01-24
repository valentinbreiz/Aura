using Cosmos.Core;
using Cosmos.HAL;
using Cosmos.System;
using nifanfa.CosmosDrawString;
using System;
using System.Drawing;

namespace CosmosKernel1
{
    class Dock
    {
        uint Width = 200;
        uint Height = 30;
        uint Devide = 20;

        int _frames = 0;
        int _fps = 0;
        int _deltaT = 0;

        public void Update()
        {
            if (_deltaT != RTC.Second)
            {
                _fps = _frames;
                _frames = 0;
                _deltaT = RTC.Second;
            }

            _frames++;

            Width = (uint)(Kernel.apps.Count * Kernel.programlogo.Width + Kernel.apps.Count * Devide);

            Kernel.vMWareSVGAII.DoubleBuffer_DrawFillRectangle(0, 0, Kernel.screenWidth, 20, (uint)Kernel.avgCol.ToArgb());
            string text = "PowerOFF";
            uint strX = 2;
            uint strY = (20 - 16) / 2;
            Kernel.vMWareSVGAII._DrawACSIIString("PowerOFF", (uint)Color.White.ToArgb(), strX, strY);
            Kernel.vMWareSVGAII._DrawACSIIString("fps=" + _fps, (uint)Color.White.ToArgb(), (uint)((strX + 7) * ASC16.fontSize.Width), strY);
            string time = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}";
            Kernel.vMWareSVGAII._DrawACSIIString(time, (uint)Color.White.ToArgb(), (uint)(Kernel.screenWidth - strX - time.Length * ASC16.fontSize.Width), strY);
            if (Kernel.Pressed)
            {
                if (MouseManager.X > strX && MouseManager.X < strX + (text.Length * 8) && MouseManager.Y > strY && MouseManager.Y < strY + 16)
                {
                    ACPI.Shutdown();
                }
            }

            Kernel.vMWareSVGAII.DoubleBuffer_DrawFillRectangle((Kernel.screenWidth - Width) / 2, Kernel.screenHeight - Height, Width, Height, (uint)Kernel.avgCol.ToArgb());

            for (int i = 0; i < Kernel.apps.Count; i++)
            {
                Kernel.apps[i].dockX = (uint)(Devide / 2 + ((Kernel.screenWidth - Width) / 2) + (Kernel.programlogo.Width * i) + (Devide * i));
                Kernel.apps[i].dockY = Kernel.screenHeight - Kernel.programlogo.Height - Devide / 2;
                Kernel.vMWareSVGAII.DoubleBuffer_DrawImage(Kernel.programlogo, Kernel.apps[i].dockX, Kernel.apps[i].dockY);
            }
        }
    }
}
