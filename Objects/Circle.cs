﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
 

namespace eventsGame.Objects
{
    class Circle : BaseObject
    {
        public Circle(float x, float y, float angle) : base(x, y, angle)
        {
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(
               new SolidBrush(Color.Green),
               -15, -15,
               30, 30
           );

            g.DrawEllipse(
                new Pen(Color.Green, 2),
                -15, -15,
                30, 30
            );
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-3, -3, 6, 6);
            return path;
        }
    }
}
