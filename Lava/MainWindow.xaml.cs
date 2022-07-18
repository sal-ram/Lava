using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Lava
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapImage[] bm = { new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Дверь_1.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Трубы_1.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Горящие Баллоны_1.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Объёмное воспламенение_1.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Кровать_1.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Телевизор_1.png", UriKind.RelativeOrAbsolute))
        };
        BitmapImage[] bmFire1 = { new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Дверь_2.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Трубы_2.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Горящие Баллоны_2.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Объёмное воспламенение_2.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Кровать_2.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Телевизор_2.png", UriKind.RelativeOrAbsolute))
        };
        BitmapImage[] bmFire2 = { new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Дверь_3.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Трубы_3.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Горящие Баллоны_3.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Объёмное воспламенение_3.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Кровать_3.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Телевизор_3.png", UriKind.RelativeOrAbsolute))
        };
        Image[] img;

        BitmapImage svetOn = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"Render\СиловойЯщик_3.png", UriKind.RelativeOrAbsolute));
        BitmapImage svetOff = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"Render\СиловойЯщик_4.png", UriKind.RelativeOrAbsolute));

        BitmapImage bmButtonRed = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"Button\Red кнопка.png", UriKind.RelativeOrAbsolute));
        BitmapImage bmButtonGrey = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"Button\Grey кнопка.png", UriKind.RelativeOrAbsolute));

        BitmapImage monometr_img = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Манометр.png", UriKind.RelativeOrAbsolute));
        BitmapImage strelka_img = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Стрелка.png", UriKind.RelativeOrAbsolute));

        private const int monometr_start_value = -137;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DateTime start;
        TimeSpan timer;
        string infoTimer;

        bool fuseDoorFlag = true;
        bool fuseDoorFlag2 = true;

        bool fuseFlangerFlag_Low = true;
        bool fuseFlangerFlag2_Low = true;
        bool fuseFlangerFlag_High = true;
        bool fuseFlangerFlag2_High = true;

        bool fuseCylindersFlag = true;
        bool fuseCylindersFlag2 = true;

        bool fuseCeilingFlag = true;
        bool fuseCeilingFlag2 = true;

        bool fuseBedFlag = true;
        bool fuseBedFlag2 = true;

        bool fuseTVFlag = true;
        bool fuseTVFlag2 = true;

        BitmapImage img_Flange_LowActive_HighNotActive = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Трубы_5.png", UriKind.RelativeOrAbsolute));
        BitmapImage img_Flange_LowNotActive_HighActive = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Трубы_4.png", UriKind.RelativeOrAbsolute));
        BitmapImage img_Flange_LowActive_HighActive = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Трубы_6.png", UriKind.RelativeOrAbsolute));
        BitmapImage img_Flange_LowActive_Low2Active_HighNotActive = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Трубы_8.png", UriKind.RelativeOrAbsolute));
        BitmapImage img_Flange_LowNotActive_HighActive_High2Active = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Трубы_7.png", UriKind.RelativeOrAbsolute));
        BitmapImage img_Flange_LowActive_HighActive_High2Active = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Трубы_10.png", UriKind.RelativeOrAbsolute));
        BitmapImage img_Flange_LowActive_Low2Active_HighActive = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Трубы_11.png", UriKind.RelativeOrAbsolute));
        BitmapImage img_Flange_LowActive_Low2Active_HighActive_High2Active = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"NewRender\Трубы_9.png", UriKind.RelativeOrAbsolute));

        public MainWindow()
        {
            InitializeComponent();

            img = new Image[] { Door, Flange, Cylinders, Ceiling, Bed, TV };
            for (int i = 0; i <= 5; i++)
                img[i].Source = bm[i];

            Svet.Source = svetOn;

            Monometr.Source = monometr_img;
            Monometr_Strelka.Source = strelka_img;

            ChangeMonometrValue(0.4f);

            start = DateTime.Now;
            dispatcherTimer.Tick += new EventHandler(Info_Time);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            ButtonConnector buttonConnector = new ButtonConnector("192.168.1.50", 5000);
 
            buttonConnector.OnTRMUpdated += OnTrmValuesChanged;
            buttonConnector.OnGazAnalyzerGazohranUpdated += OnGasAnalyzerGazohranValuesChanged;
            buttonConnector.OnGazAnalyzerLiveZoneUpdated += OnGasAnalyzerLiveZoneValuesChanged;
            buttonConnector.OnGazAnalyzerPromZoneUpdated += OnGasAnalyzerPromZoneValuesChanged;
            buttonConnector.OnBoxControllerUpdated += OnBoxControllerValuesChanged;

            Host_FakeDevice.FakeBoxControllerValuesUpdate += OnBoxControllerValuesChanged;
            Host_FakeDevice.StartThread();
            

        }

        private bool isRunning;
        public void button_Click()
        {
            var flashButton = FindResource("FlashButton") as Storyboard;
            var changeColor = FindResource("ChangeColor") as Storyboard;
            var changeColor2 = FindResource("ChangeColor2") as Storyboard;

            if (isRunning)
            {
                flashButton.Stop();
                changeColor2.Begin();
                isRunning = false;
            }
            else
            {
                flashButton.Begin();
                changeColor.Begin();
                isRunning = true;
            }
        }

        private void Info_Time(object sender, EventArgs e)
        {
            Time.Content = DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2");
            timer = DateTime.Now - start;
            infoTimer = timer.Hours.ToString("D2") + ":" + timer.Minutes.ToString("D2") + ":"  + timer.Seconds.ToString("D2");
            TimeTraining.Content = infoTimer;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        //  ненужные функции нажатия на картинки
        private void Door_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {/*
            if (fire[0])
            {
                Door.Source = bm[0];
                fire[0] = false;
            }
            else
            {
                Door.Source = bmFire1[0];
                fire[0] = true;
            }*/
        }
        private void Flange_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {/*
            if (fire[1])
            {
                Flange.Source = bm[1];
                fire[1] = false;
            }
            else
            {
                Flange.Source = bmFire1[1];
                fire[1] = true;
            }*/
        }
        private void Cylinders_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {/*
            if (fire[2])
            {
                Cylinders.Source = bm[2];
                fire[2] = false;
            }
            else
            {
                Cylinders.Source = bmFire1[2];
                fire[2] = true;
            }*/
        }
        private void Ceiling_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {/*
            if (fire[3])
            {
                Ceiling.Source = bm[3];
                fire[3] = false;
            }
            else
            {
                Ceiling.Source = bmFire1[3];
                fire[3] = true;
            }*/
        }
        private void Bed_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {/*
            if (fire[4])
            {
                Bed.Source = bm[4];
                fire[4] = false;
            }
            else
            {
                Bed.Source = bmFire1[4];
                fire[4] = true;
            }*/
        }
        private void TV_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /*if (fire[5])
            {
                TV.Source = bm[5];
                fire[5] = false;
            }
            else
            {
                TV.Source = bmFire1[5];
                fire[5] = true;
            }*/
        }


        // сброс времени
        private void ResetTime_Click(object sender, RoutedEventArgs e)
        {
            infoTimer = "00:00:00";
            start = DateTime.Now;
        }


        // нажатия на кнопки горелок
        private void Fuse_Door_Click(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Zapal(Img_But_Door, Img_But_Door2, Door, Fuse_Door, Fuse_Door2, ref fuseDoorFlag, ref fuseDoorFlag2, 0);
        }
        private void Fuse_Door_Click2(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Osnov(Img_But_Door2, Door, Fuse_Door2, ref fuseDoorFlag, ref fuseDoorFlag2, 0);
        }

        private void Fuse_Flange_Click_Low(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Zapal(Img_But_Flange_Low, Img_But_Flange2_Low, Flange, Fuse_Flange_Low, Fuse_Flange2_Low, ref fuseFlangerFlag_Low, ref fuseFlangerFlag2_Low, 1);
            Set_Img_FlangeCondition();
        }
        private void Fuse_Flange_Click2_Low(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Osnov(Img_But_Flange2_Low, Flange, Fuse_Flange2_Low, ref fuseFlangerFlag_Low, ref fuseFlangerFlag2_Low, 1);
            Set_Img_FlangeCondition();
        }

        private void Fuse_Flange_Click_High(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Zapal(Img_But_Flange_High, Img_But_Flange2_High, Flange, Fuse_Flange_High, Fuse_Flange2_High, ref fuseFlangerFlag_High, ref fuseFlangerFlag2_High, 1);
            Set_Img_FlangeCondition();
        }
        private void Fuse_Flange_Click2_High(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Osnov(Img_But_Flange2_High, Flange, Fuse_Flange2_High, ref fuseFlangerFlag_High, ref fuseFlangerFlag2_High, 1);
            Set_Img_FlangeCondition();
        }
 
        private void Set_Img_FlangeCondition()
        {
            if (fuseFlangerFlag_Low && fuseFlangerFlag_High)
            {
                Flange.Source = bm[1];
            }
            else if (!fuseFlangerFlag_Low && fuseFlangerFlag_High)
            {
                if (!fuseFlangerFlag2_Low)
                {
                    Flange.Source = img_Flange_LowActive_Low2Active_HighNotActive;
                }
                else 
                {
                    Flange.Source = img_Flange_LowActive_HighNotActive;
                }
            }
            else if (fuseFlangerFlag_Low && !fuseFlangerFlag_High)
            {
                if (!fuseFlangerFlag2_High)
                {
                    Flange.Source = img_Flange_LowNotActive_HighActive_High2Active;
                }
                else
                {
                    Flange.Source = img_Flange_LowNotActive_HighActive;
                }
            }
            else if (!fuseFlangerFlag_Low && !fuseFlangerFlag_High)
            {
                if (fuseFlangerFlag2_Low && fuseFlangerFlag2_High)
                {
                    Flange.Source = img_Flange_LowActive_HighActive;
                }
                else if(!fuseFlangerFlag2_Low && fuseFlangerFlag2_High)
                {
                    Flange.Source = img_Flange_LowActive_Low2Active_HighActive;
                }
                else if (fuseFlangerFlag2_Low && !fuseFlangerFlag2_High)
                {
                    Flange.Source = img_Flange_LowActive_HighActive_High2Active;
                }
                else if (!fuseFlangerFlag2_Low && !fuseFlangerFlag2_High)
                {
                    Flange.Source = img_Flange_LowActive_Low2Active_HighActive_High2Active;
                }
            }
        }

        private void Fuse_Cylinders_Click(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Zapal(Img_But_Cylinders, Img_But_Cylinders2, Cylinders, Fuse_Cylinders, Fuse_Cylinders2, ref fuseCylindersFlag, ref fuseCylindersFlag2, 2);
        }
        private void Fuse_Cylinders_Click2(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Osnov(Img_But_Cylinders2, Cylinders, Fuse_Cylinders2, ref fuseCylindersFlag, ref fuseCylindersFlag2, 2);
        }

        private void Fuse_Ceiling_Click(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Zapal(Img_But_Ceiling, Img_But_Ceiling2, Ceiling, Fuse_Ceiling, Fuse_Ceiling2, ref fuseCeilingFlag, ref fuseCeilingFlag2, 3);
        }
        private void Fuse_Ceiling_Click2(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Osnov(Img_But_Ceiling2, Ceiling, Fuse_Ceiling2, ref fuseCeilingFlag, ref fuseCeilingFlag2, 3);
        }

        private void Fuse_Bed_Click(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Zapal(Img_But_Bed, Img_But_Bed2, Bed, Fuse_Bed, Fuse_Bed2, ref fuseBedFlag, ref fuseBedFlag2, 4);
        }
        private void Fuse_Bed_Click2(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Osnov(Img_But_Bed2, Bed, Fuse_Bed2, ref fuseBedFlag, ref fuseBedFlag2, 4);
        }

        private void Fuse_TV_Click(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Zapal(Img_But_TV, Img_But_TV2, TV, Fuse_TV, Fuse_TV2, ref fuseTVFlag, ref fuseTVFlag2, 5);
        }
        private void Fuse_TV_Click2(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Osnov(Img_But_TV2, TV, Fuse_TV2, ref fuseTVFlag, ref fuseTVFlag2, 5);
        }

        private void Fuse_Click_Zapal(Image img_but_Zapal, Image img_but_Osnov, Image img_Zapal, Button button_Zapal, Button button_Osnov, ref bool fuseFlag_Zapal, ref bool fuseFlag_Osnov, int numImgInMassive)
        {
            var textOfButton = button_Zapal.Content as TextBlock;

            if (fuseFlag_Zapal)
            {
                textOfButton.Text = "STOP";
                textOfButton.Padding = new Thickness(8);

                img_but_Zapal.Source = bmButtonRed;

                button_Zapal.Foreground = Brushes.White;
                button_Osnov.Foreground = Brushes.Black;

                fuseFlag_Zapal = false;
                fuseFlag_Osnov = true;

                img_Zapal.Source = bmFire1[numImgInMassive];
            }
            else
            {
                textOfButton.Text = "Запальная горелка";
                textOfButton.Padding = new Thickness(0);

                img_but_Zapal.Source = bmButtonGrey;
                button_Zapal.Foreground = Brushes.Black;

                img_but_Osnov.Source = bmButtonGrey;
                button_Osnov.Foreground = Brushes.Gray;

                fuseFlag_Zapal = true;
                img_Zapal.Source = bm[numImgInMassive];
            }
        }
        private void Fuse_Click_Osnov(Image img_but_Osnov, Image img_Osnov, Button button_Osnov, ref bool fuseFlag_Zapal, ref bool fuseFlag_Osnov, int numImgInMassive)
        {
            if (!fuseFlag_Zapal)
            {
                if (fuseFlag_Osnov)
                {
                    img_but_Osnov.Source = bmButtonRed;
                    button_Osnov.Foreground = Brushes.White;
                    fuseFlag_Osnov = false;
                    img_Osnov.Source = bmFire2[numImgInMassive];
                }
                else
                {
                    img_but_Osnov.Source = bmButtonGrey;
                    button_Osnov.Foreground = Brushes.Black;
                    fuseFlag_Osnov = true;
                    img_Osnov.Source = bmFire1[numImgInMassive];
                }
            }
        }

        // меняет состояние рубильника
        /*public void ChangeRubilnikState(object sender, RoutedEventArgs e)
        {
            if (!flagSvet)
            {
                Svet.Source = svetOn;
                BorderSvet.Background = new SolidColorBrush(new Color { R = 255, G = 187, B = 100, A = 255 });
                Voltage.Content = "Под напряжением";
            }
            else
            {
                Svet.Source = svetOff;
                BorderSvet.Background = new SolidColorBrush(new Color { R = 197, G = 197, B = 197, A = byte.MaxValue });
                Voltage.Content = "Без напряжения";
            }
        }*/

        public void RubilnikUpdate(bool flagSvet)
        {
            if (!flagSvet)
            {
                Svet.Source = svetOn;
                BorderSvet.Background = new SolidColorBrush(new Color { R = 255, G = 187, B = 100, A = 255 });
                Voltage.Content = "Под напряжением";
            }
            else
            {
                Svet.Source = svetOff;
                BorderSvet.Background = new SolidColorBrush(new Color { R = 197, G = 197, B = 197, A = byte.MaxValue });
                Voltage.Content = "Без напряжения";
            }
        }

        // устанавливает значение Давления в окошке "Общая Информация" и угол наклона стрелки на манометре
        public void ChangeMonometrValue(float value)
        {
            Monometr_Value_text.Content = value + " MPa";

            TransformGroup group = Monometr_Strelka.RenderTransform as TransformGroup;

            var child = group.Children[2] as RotateTransform;

            child.Angle = monometr_start_value + value * 274;
        }

        private void OnTrmValuesChanged(object sender, TRMArgs e) // trm изменение значений
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    PromZoneTemp.Content = e.PromZone_Temp + " °C";
                    GazhranZoneTemp.Content = e.Gazohran_Temp + " °C";
                    LizeZoneTemp.Content = e.LiveZone_Temp + " °C";
                });
            }
            catch (Exception ex)
            {

            }
        }

        private void OnGasAnalyzerPromZoneValuesChanged(object sender, GasAnalyzerArgs e) // пром зона. Газоанализатор
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    GasAnalyzerPromZonePercent.Content = e.GasPercent + "%";
                });
            }
            catch (Exception ex)
            {

            }
        }
        private void OnGasAnalyzerLiveZoneValuesChanged(object sender, GasAnalyzerArgs e) // жил зона. Газоанализатор
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    GasAnalyzerLiveZonePercent.Content = e.GasPercent + "%";
                });
            }
            catch (Exception ex)
            {

            }
        }
        private void OnGasAnalyzerGazohranValuesChanged(object sender, GasAnalyzerArgs e) // газовое хранилище. Газоанализатор
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    GasAnalyzerGazohranZonePercent.Content = e.GasPercent + "%";
                });
            }
            catch (Exception ex)
            {

            }
        }

        private void OnBoxControllerValuesChanged(object sender, BoxControllerArgs e) // Обновление показания горелок, рубильника, тревоги
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    FusesUpdateStates(e.BoxFlangesArgs);
                    RubilnikChangeState(e.BoxRubilnikArgs[0][0]);
                    // тут же делать alarm check
                });
            }
            catch (Exception ex)
            {

            }
        }

        private void RubilnikChangeState(byte state)
        {
            RubilnikUpdate(Convert.ToBoolean(state));
        }

        private void FusesUpdateStates(List<byte[]> newStates)
        {
            UpdateFuseCeiling(newStates[0]);
            UpdateFuseTV(newStates[1]);
            UpdateFuseBed(newStates[2]);
            UpdateFuseDoor(newStates[3]);
            UpdateFuseCylinder(newStates[4]);
            UpdateFusePipe1(newStates[5]);
            UpdateFusePipe2(newStates[6]);
        }

        private void UpdateFuseCeiling(byte[] newState)
        {
            fuseCeilingFlag = Convert.ToBoolean(newState[0]);
            Fuse_Ceiling_Click(this, new RoutedEventArgs());
            fuseCeilingFlag2 = Convert.ToBoolean(newState[1]);
            Fuse_Ceiling_Click2(this, new RoutedEventArgs());
        }

        private void UpdateFuseTV(byte[] newState)
        {
            fuseTVFlag = Convert.ToBoolean(newState[0]);
            Fuse_TV_Click(this, new RoutedEventArgs());
            fuseTVFlag2 = Convert.ToBoolean(newState[1]);
            Fuse_TV_Click2(this, new RoutedEventArgs());
        }

        private void UpdateFuseBed(byte[] newState)
        {
            fuseBedFlag = Convert.ToBoolean(newState[0]);
            Fuse_Bed_Click(this, new RoutedEventArgs());
            fuseBedFlag2 = Convert.ToBoolean(newState[1]);
            Fuse_Bed_Click2(this, new RoutedEventArgs());
        }

        private void UpdateFuseDoor(byte[] newState)
        {
            fuseDoorFlag = Convert.ToBoolean(newState[0]);
            Fuse_Door_Click(this, new RoutedEventArgs());
            fuseDoorFlag2 = Convert.ToBoolean(newState[1]);
            Fuse_Door_Click2(this, new RoutedEventArgs());
        }

        private void UpdateFuseCylinder(byte[] newState)
        {
            fuseCylindersFlag = Convert.ToBoolean(newState[0]);
            Fuse_Cylinders_Click(this, new RoutedEventArgs());
            fuseCylindersFlag2 = Convert.ToBoolean(newState[1]);
            Fuse_Cylinders_Click2(this, new RoutedEventArgs());
        }

        private void UpdateFusePipe1(byte[] newState)
        {
            fuseFlangerFlag_Low = Convert.ToBoolean(newState[0]);
            Fuse_Flange_Click_Low(this, new RoutedEventArgs());
            fuseFlangerFlag2_Low = Convert.ToBoolean(newState[1]);
            Fuse_Flange_Click2_Low(this, new RoutedEventArgs());
        }

        private void UpdateFusePipe2(byte[] newState)
        {
            fuseFlangerFlag_High = Convert.ToBoolean(newState[0]);
            Fuse_Flange_Click_High(this, new RoutedEventArgs());
            fuseFlangerFlag2_High = Convert.ToBoolean(newState[1]);
            Fuse_Flange_Click2_High(this, new RoutedEventArgs());
        }
    }
}
