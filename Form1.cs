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

        public Form1()
        {
            InitializeComponent();
            myRect = new MyRectangle(100, 100, 45);
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            g.Transform = myRect.GetTranform();

            myRect.Render(g); 
            
        }
    }
}
