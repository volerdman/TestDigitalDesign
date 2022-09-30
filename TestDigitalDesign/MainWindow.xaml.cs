using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestDigitalDesign
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double angleUIElement = 90;
        int[,] num = new int[4, 4] {
            { 1, 0, 1, 1 },
            { 0, 1, 1, 1 },
            { 0, 1, 0, 0 },
            { 1, 0, 0, 1 },
        };

        int[,] numStart = new int[4, 4] {
            { 1, 0, 1, 1 },
            { 0, 1, 1, 1 },
            { 0, 1, 0, 0 },
            { 1, 0, 0, 1 },
        };

        Grid grid = new Grid();

        public MainWindow()
        {
            InitializeComponent();
            CreateButton(num);
        }

        //изменение ориентации кнопок
        private void ButtonRotate(object sender, RoutedEventArgs e)
        {
            var check = CheckArray();
            var button = sender as Button;
            var tag = (int)button.Tag;
            var changeTag = 0;

            if (check)
            {
                MessageBoxButton boxButton = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                string messageBoxText = "Хотите начать заново?";
                string caption = "Поздравляю! Вы выиграли!";
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, boxButton, icon);

                if (result == MessageBoxResult.Yes)
                {
                    test_form.Children.Remove(grid);
                    CreateButton(numStart);
                }
                else
                {
                    Close();
                }
            }
            else
            {
                if ((tag == 0))
                {
                    angleUIElement = 0;
                    button.Tag = 1;
                    changeTag = 1;
                }
                else
                {
                    angleUIElement = -90;
                    button.Tag = 0;
                    changeTag = 0;
                }
                var rotateRender = new RotateTransform(angleUIElement);

                button.RenderTransform = rotateRender;
                test_form.Children.Remove(grid);

                int col = Convert.ToInt32(new string(button.Name[3], 1));
                int st = Convert.ToInt32(new string(button.Name[1], 1));

                CreateButton(ChangeArray(col, st, changeTag));
            }
            
        }

        //изменение поля, после манипуляций игрока
        private int[,] ChangeArray(int col, int st, int tag)
        {

            for (int i = 0; i<4; i++)
            {
                
                for (int j = 0; j<4; j++)
                {
                    if(j == col)
                    {
                        if (num[i, j] == 0)
                        {
                            num[i, j] = 1;
                        }
                        else
                            num[i, j] = 0;
                    }
                    if (i == st && j!= col) {
                        if (num[i, j] == 0)
                        {
                            num[i, j] = 1;
                        }
                        else
                            num[i, j] = 0;
                    }
                }
            }
            
            return num;
        }

        //поворот кнопок при прорисовке
        private void ButtonRotateInit(Button name)
        {
            var rotateRender = new RotateTransform(90);

            name.RenderTransform = rotateRender;
            name.Tag = 0;
        }

        //создание поля кнопок
        private void CreateButton(int[,] arr)
        {
            Grid test = new Grid();
            test.Name = "test";
            test_form.Children.Add(test);
            grid = test;

            Button[] but = new Button[16];

            int y = -400, x = -250;
            int buttonWidth = 25;
            int buttonHeight = 50;
            int i = 0;
            int j = 0;

            for (int k = 0; k < 16; k++)
            {
                but[k] = new Button();

                //свойства кнопки
                but[k].RenderTransformOrigin = new Point(0.5, 0.5);
                but[k].Click += ButtonRotate;
                but[k].Name = "b" + i.ToString() + "_" + j.ToString();
                but[k].Tag = arr[i,j];

                //отступы
                var margin = but[k].Margin;
                margin.Top = y;
                margin.Left = x;
                but[k].Margin = margin;

                //размер кнопки
                but[k].Width = buttonWidth;
                but[k].Height = buttonHeight;

                if (arr[i, j] == 0)
                {
                    ButtonRotateInit(but[k]);
                }


                test.Children.Add(but[k]);

                if ((k + 1) % 4 == 0)
                {
                    i++;
                    j = 0;
                    x = -250;
                    y += buttonHeight * 4;
                }
                else
                {
                    x += buttonWidth * 4 + 10;
                    j++;
                }
            }
        }

        //проверка на выигрыш
        private bool CheckArray()
        {
            int arrLength = num.Length;
            int reverseCount = 0;
            int standartCount = 0;
            bool check = false;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (num[i, j] == 0)
                        reverseCount += 1;
                    else
                        standartCount += 1;
                }
            }

            if (reverseCount == arrLength || standartCount == arrLength)
                check = true;

            return check;
        }
    }
}
