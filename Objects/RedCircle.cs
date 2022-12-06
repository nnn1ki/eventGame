using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace eventsGame.Objects
{
    class RedCircle : BaseObject
    {
        int width = 10;
        int height = 10;
        public int time = 0;
        public int dieTime;

        public Action<Player> OnPlayerOverlap; //красный круг пересекает игрока
        public Action<Circle> OnCrclOverlap; //красный круг пересекает игрока

        public RedCircle(float x, float y, float angle) : base(x, y, angle)
        {
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(
               new SolidBrush(Color.LightPink),
               X, Y, 
               width, height
           );

        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(X, Y, width, height);
            return path;
        }

        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);

            if (obj is Player)
            {
                OnPlayerOverlap(obj as Player); 
            }

            if (obj is Circle)
            {
                OnCrclOverlap(obj as Circle); 
            }

        }


        public void changeSize()
        {
            X -= 0.5f;
            Y -= 0.5f;
            width += 2;
            height += 2;
        }







    }
}
