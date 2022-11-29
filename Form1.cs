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

//БУДЕМ ДЕЛАТЬ 3 И 4 -ЫЕ ЗАДАНИЯ

namespace eventsGame
{
    public partial class Form1 : Form
    {
        Random rnd; //переменная рандома, привязываем ее к началу создания программы
        Circle circle; 
        List<BaseObject> оbjects = new List<BaseObject>();
        Player player;
        Marker marker;


        public Form1()
        {
            InitializeComponent();

            rnd = new Random(); //привязываем к запуску программы

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0); //создаем игрока по центру экрана

            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };

            player.OnMarkerOverlap += (m) =>
            {
                оbjects.Remove(m); // удаляем маркер те мы его достигли
                marker = null;
            };

            player.OnCircleOverlap += (m) =>
            {
                оbjects.Remove(m);
                circle = null;
                circleRender();
            };

            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            оbjects.Add(marker);
            оbjects.Add(player);

            //нужно тут определить генерацию кружков
            //нужно сделать тоже самое, что и с маркером, но генерироваться он должен самостоятельно 


            circleRender(); //это первичный круг для начала игры

            //оbjects.Add(new Circle(50, 50, 0)); //определяем координаты крга в конструктор 
        }

        private void pbMain_Paint(object sender, PaintEventArgs e) //событие отрисовки поля
        {
            var g = e.Graphics; //забираем все параметры в g 
            g.Clear(Color.White); //стираем все что было 

            updatePlayer();
            

            // пересчитываем пересечения
            foreach (var obj in оbjects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj); //игрок пересек объект
                    obj.Overlap(player); //объект пересек игрока
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
            if (marker != null) //если маркер существует
            {
                //считаем расстояния до маркера
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI; //считается угол, на который поворачивается игрок, чтобы приблизиться к маркеру
            }

            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            player.X += player.vX;
            player.Y += player.vY;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            pbMain.Invalidate(); //обновление поля для отрисовки
        }


        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            // тут добавил создание маркера по клику если он еще не создан
            if (marker == null)
            {
                marker = new Marker(0, 0, 0); //определяем нулевые положения
                оbjects.Add(marker); // и главное не забыть пололжить в objects
            }

            //определяем положение по координатам нажатия
            marker.X = e.X;
            marker.Y = e.Y;
        }


        //создание круга
        public void circleRender()
        {
            if (circle == null) 
            {
                circle = new Circle(0, 0, 0); //создаем новый куг
                оbjects.Add(circle); 
            }
            
            circle.X = rnd.Next(0, pbMain.Width);
            circle.Y = rnd.Next(0, pbMain.Height);
        }

    }
}
