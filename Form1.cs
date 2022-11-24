using eventsGame.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace eventsGame
{
    public partial class Form1 : Form
    {
        MyRectangle myRect; //поле под фигуру
        List<BaseObject> оbjects = new List<BaseObject>();
        Player player;
        Marker marker;


        public Form1()
        {
            InitializeComponent();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };

            player.OnMarkerOverlap += (m) =>
            {
                оbjects.Remove(m);
                marker = null;
            };

            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            оbjects.Add(marker);
            оbjects.Add(player); 

            оbjects.Add(new MyRectangle(50, 50, 0));
            оbjects.Add(new MyRectangle(100, 100, 45));

        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            //updatePlayer(); 

            // пересчитываем пересечения
            foreach (var obj in оbjects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }
            }

            // рендерим объекты
            foreach (var obj in оbjects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }



        }


        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float lenght = MathF.Sqrt(dx * dx + dy * dy);
                dx /= lenght;
                dy /= lenght;

                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            player.vX += -player.vX + 0.1f;
            player.vY += -player.vY * 0.1f;

            player.X += player.vX;
            player.Y += player.vY; 

        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            pbMain.Invalidate();
            updatePlayer();
        }

        

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                оbjects.Add(marker);
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}
