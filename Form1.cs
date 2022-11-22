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
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            оbjects.Add(marker);
            оbjects.Add(player); 

            оbjects.Add(new MyRectangle(50, 50, 0));
            оbjects.Add(new MyRectangle(100, 100, 45));

            //myRect = new MyRectangle(100, 100, 45);
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            foreach (var obj in оbjects)
            {
                g.Transform = obj.GetTranform();
                obj.Render(g); 
            }


            //g.Transform = myRect.GetTranform();
            //myRect.Render(g); 
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //расчитаем вектор между игрокоом и меркером
            float dx = marker.X - player.X;
            float dy = marker.Y - player.Y;

            //найдем ее длину 
            float lenght = MathF.Sqrt(dx * dx + dy * dy);
            dx /= lenght;
            dy /= lenght;

            player.X += dx * 2;
            player.Y += dy * 2;

            pbMain.Invalidate(); 
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e) {}

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}
