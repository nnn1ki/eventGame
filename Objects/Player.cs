using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace eventsGame.Objects
{
    class Player : BaseObject
    {
        public Action<Marker> OnMarkerOverlap;
        public float vX, vY;

        public Player(float x, float y, float angle) : base(x, y, angle)
        {

        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(
                new SolidBrush(Color.DeepSkyBlue), 
                -15, -15,
                30, 30
            );

            g.DrawEllipse(
                new Pen(Color.Black, 2),
                -15, -15, 
                30, 30
            );

            g.DrawLine(new Pen(Color.Black, 2), 0, 0, 25, 0); 

        }

        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);

            if (obj is Marker)
            {
                OnMarkerOverlap(obj as Marker);
            }
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var parth = base.GetGraphicsPath();
            parth.AddEllipse(-15, -15, 30, 30);
            return parth;
        }


    }
}
