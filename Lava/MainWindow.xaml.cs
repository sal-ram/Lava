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
        BitmapImage[] bm = { new BitmapImage(new Uri(@"Images/Дверь_1.png", UriKind.RelativeOrAbsolute)),
             new BitmapImage(new Uri( @"Images/Трубы_1.png", UriKind.RelativeOrAbsolute)),
             new BitmapImage(new Uri( @"Images/Горящие баллоны_1.png", UriKind.RelativeOrAbsolute)),
             new BitmapImage(new Uri( @"Images/Объёмное воспламенение_1.png", UriKind.RelativeOrAbsolute)),
             new BitmapImage(new Uri( @"Images/Кровать_1.png", UriKind.RelativeOrAbsolute)),
             new BitmapImage(new Uri( @"Images/Телевизор_1.png", UriKind.RelativeOrAbsolute))
         };
        BitmapImage[] bmFire1 = { new BitmapImage(new Uri(@"Images\Дверь_2.png", UriKind.RelativeOrAbsolute)),
             new BitmapImage(new Uri( @"Images/Трубы_2.png", UriKind.RelativeOrAbsolute)),
             new BitmapImage(new Uri( @"Images/Горящие баллоны_2.png", UriKind.RelativeOrAbsolute)),
             new BitmapImage(new Uri( @"Images/Объёмное воспламенение_2.png", UriKind.RelativeOrAbsolute)),
             new BitmapImage(new Uri( @"Images/Кровать_2.png", UriKind.RelativeOrAbsolute)),
             new BitmapImage(new Uri( @"Images/Телевизор_2.png", UriKind.RelativeOrAbsolute))
         };
        BitmapImage[] bmFire2 = { new BitmapImage(new Uri( @"Images/Дверь_3.png", UriKind.RelativeOrAbsolute)),
             new BitmapImage(new Uri( @"Images/Трубы_3.png", UriKind.RelativeOrAbsolute)),
             new BitmapImage(new Uri( @"Images/Горящие баллоны_3.png", UriKind.RelativeOrAbsolute)),
             new BitmapImage(new Uri( @"Images/Объёмное воспламенение_3.png", UriKind.RelativeOrAbsolute)),
             new BitmapImage(new Uri( @"Images/Кровать_3.png", UriKind.RelativeOrAbsolute)),
             new BitmapImage(new Uri( @"Images/Телевизор_3.png", UriKind.RelativeOrAbsolute))
         };
        Image[] img;


        BitmapImage svetOn = new BitmapImage(new Uri(@"Images/Силовой ящик_вкл.png", UriKind.Relative));
        BitmapImage svetOff = new BitmapImage(new Uri(@"Images/Силовой ящик_выкл.png", UriKind.Relative));

        BitmapImage bmButtonRed = new BitmapImage(new Uri(@"Images/Red кнопка.png", UriKind.Relative));
        BitmapImage bmButtonGrey = new BitmapImage(new Uri(@"Images/Grey кнопка.png", UriKind.Relative));

        BitmapImage monometr_img = new BitmapImage(new Uri(@"Images/Манометр.png", UriKind.Relative));
        BitmapImage strelka_img = new BitmapImage(new Uri(@"Images/Стрелка.png", UriKind.Relative));

        private const int monometr_start_value = -137;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DateTime start;
        TimeSpan timer;
        string infoTimer;

        bool fuseDoorFlag = false;
        bool fuseDoorFlag2 = false;

        bool fuseFlangerFlag_Low = false;
        bool fuseFlangerFlag2_Low = false;
        bool fuseFlangerFlag_High = false;
        bool fuseFlangerFlag2_High = false;

        bool fuseCylindersFlag = false;
        bool fuseCylindersFlag2 = false;

        bool fuseCeilingFlag = false;
        bool fuseCeilingFlag2 = false;

        bool fuseBedFlag = false;
        bool fuseBedFlag2 = false;

        bool fuseTVFlag = false;
        bool fuseTVFlag2 = false;

        BitmapImage img_Flange_LowActive_HighNotActive = new BitmapImage(new Uri(@"Images/Трубы_5.png", UriKind.RelativeOrAbsolute));
        BitmapImage img_Flange_LowNotActive_HighActive = new BitmapImage(new Uri(@"Images/Трубы_4.png", UriKind.RelativeOrAbsolute));
        BitmapImage img_Flange_LowActive_HighActive = new BitmapImage(new Uri(@"Images/Трубы_6.png", UriKind.RelativeOrAbsolute));
        BitmapImage img_Flange_LowActive_Low2Active_HighNotActive = new BitmapImage(new Uri(@"Images/Трубы_8.png", UriKind.RelativeOrAbsolute));
        BitmapImage img_Flange_LowNotActive_HighActive_High2Active = new BitmapImage(new Uri(@"Images/Трубы_7.png", UriKind.RelativeOrAbsolute));
        BitmapImage img_Flange_LowActive_HighActive_High2Active = new BitmapImage(new Uri(@"Images/Трубы_10.png", UriKind.RelativeOrAbsolute));
        BitmapImage img_Flange_LowActive_Low2Active_HighActive = new BitmapImage(new Uri(@"Images/Трубы_11.png", UriKind.RelativeOrAbsolute));
        BitmapImage img_Flange_LowActive_Low2Active_HighActive_High2Active = new BitmapImage(new Uri(@"Images/Трубы_9.png", UriKind.RelativeOrAbsolute));

        ButtonConnector buttonConnector;

        public MainWindow()
        {
            InitializeComponent();
            img = new Image[] { Door, Flange, Cylinders, Ceiling, Bed, TV };
            for (int i = 0; i <= 5; i++)
                img[i].Source = bm[i];

            Svet.Source = svetOn;
            Monometr.Source = monometr_img;
            Monometr_Strelka.Source = strelka_img;

            Fuse_Ceiling2.IsEnabled = false;
            Fuse_TV2.IsEnabled = false;
            Fuse_Bed2.IsEnabled = false;
            Fuse_Door2.IsEnabled = false;
            Fuse_Cylinders2.IsEnabled = false;
            Fuse_Flange2_Low.IsEnabled = false;
            Fuse_Flange2_High.IsEnabled = false;

            start = DateTime.Now;
            dispatcherTimer.Tick += new EventHandler(Info_Time);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            buttonConnector = new ButtonConnector("192.168.1.50", 5000);

            buttonConnector.OnTRMUpdated += OnTrmValuesChanged;
            buttonConnector.OnGazAnalyzerGazohranUpdated += OnGasAnalyzerGazohranValuesChanged;
            buttonConnector.OnGazAnalyzerLiveZoneUpdated += OnGasAnalyzerLiveZoneValuesChanged;
            buttonConnector.OnGazAnalyzerPromZoneUpdated += OnGasAnalyzerPromZoneValuesChanged;
            buttonConnector.OnBoxControllerUpdated += OnBoxControllerValuesChanged;

            buttonConnector.ConnectHandlers();
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

        bool[] ReturnActualArrayOfStates()
        {
            return new bool[] { fuseCeilingFlag, fuseCeilingFlag2, fuseTVFlag, fuseTVFlag2, fuseBedFlag, 
                fuseBedFlag2, fuseDoorFlag, fuseDoorFlag2, fuseCylindersFlag, fuseCylindersFlag2, 
                fuseFlangerFlag_Low, fuseFlangerFlag2_Low, fuseFlangerFlag_High, fuseFlangerFlag2_High };
        }                      

        private void Info_Time(object sender, EventArgs e)
        {
            Time.Content = DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2");
            timer = DateTime.Now - start;
            infoTimer = timer.Hours.ToString("D2") + ":" + timer.Minutes.ToString("D2") + ":" + timer.Seconds.ToString("D2");
            TimeTraining.Content = infoTimer;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // сброс времени
        private void ResetTime_Click(object sender, RoutedEventArgs e)
        {
            infoTimer = "00:00:00";
            start = DateTime.Now;
        }


        // нажатия на кнопки горелок c экрана компа
        private void Fuse_Door_Click(object sender, RoutedEventArgs e)
        {
            fuseDoorFlag = !fuseDoorFlag;
            if (fuseDoorFlag == false)
                fuseDoorFlag2 = false;
            buttonConnector.Send(ReturnActualArrayOfStates());

            Fuse_Door_UpdateState(this, new RoutedEventArgs());
        }
        private void Fuse_Door_Click2(object sender, RoutedEventArgs e)
        {
            fuseDoorFlag2 = !fuseDoorFlag2;
            buttonConnector.Send(ReturnActualArrayOfStates());

            Fuse_Door_UpdateState2(this, new RoutedEventArgs());
        }

        private void Fuse_Flange_Click_Low(object sender, RoutedEventArgs e)
        {
            fuseFlangerFlag_Low = !fuseFlangerFlag_Low;
            if (fuseFlangerFlag_Low == false)
                fuseFlangerFlag2_Low = false;
            buttonConnector.Send(ReturnActualArrayOfStates());

            Fuse_Flange_UpdateState_Low(this, new RoutedEventArgs());
        }
        private void Fuse_Flange_Click2_Low(object sender, RoutedEventArgs e)
        {
            fuseFlangerFlag2_Low = !fuseFlangerFlag2_Low;
            buttonConnector.Send(ReturnActualArrayOfStates());

            Fuse_Flange_UpdateState2_Low(this, new RoutedEventArgs());
        }

        private void Fuse_Flange_Click_High(object sender, RoutedEventArgs e)
        {
            fuseFlangerFlag_High = !fuseFlangerFlag_High;
            if (fuseFlangerFlag_High == false)
                fuseFlangerFlag2_High = false;
            buttonConnector.Send(ReturnActualArrayOfStates());

            Fuse_Flange_UpdateState_High(this, new RoutedEventArgs());
        }
        private void Fuse_Flange_Click2_High(object sender, RoutedEventArgs e)
        {
            fuseFlangerFlag2_High = !fuseFlangerFlag2_High;
            buttonConnector.Send(ReturnActualArrayOfStates());

            Fuse_Flange_UpdateState2_High(this, new RoutedEventArgs());
        }

        private void Fuse_Cylinders_Click(object sender, RoutedEventArgs e)
        {
            fuseCylindersFlag = !fuseCylindersFlag;
            if (fuseCylindersFlag == false)
                fuseCylindersFlag2 = false;
            buttonConnector.Send(ReturnActualArrayOfStates());

            Fuse_Cylinders_UpdateState(this, new RoutedEventArgs());
        }
        private void Fuse_Cylinders_Click2(object sender, RoutedEventArgs e)
        {
            fuseCylindersFlag2 = !fuseCylindersFlag2;
            buttonConnector.Send(ReturnActualArrayOfStates());

            Fuse_Cylinders_UpdateState2(this, new RoutedEventArgs());
        }

        private void Fuse_Ceiling_Click(object sender, RoutedEventArgs e)
        {
            fuseCeilingFlag = !fuseCeilingFlag;
            if (fuseCeilingFlag == false)
                fuseCeilingFlag2 = false;
            buttonConnector.Send(ReturnActualArrayOfStates());

            Fuse_Ceiling_UpdateState(this, new RoutedEventArgs());
        }
        private void Fuse_Ceiling_Click2(object sender, RoutedEventArgs e)
        {
            fuseCeilingFlag2 = !fuseCeilingFlag2;
            buttonConnector.Send(ReturnActualArrayOfStates());

            Fuse_Ceiling_UpdateState2(this, new RoutedEventArgs());
        }

        private void Fuse_Bed_Click(object sender, RoutedEventArgs e)
        {
            fuseBedFlag = !fuseBedFlag;
            if (fuseBedFlag == false)
                fuseBedFlag2 = false;
            buttonConnector.Send(ReturnActualArrayOfStates());

            Fuse_Bed_UpdateState(this, new RoutedEventArgs());
        }
        private void Fuse_Bed_Click2(object sender, RoutedEventArgs e)
        {
            fuseBedFlag2 = !fuseBedFlag2;
            buttonConnector.Send(ReturnActualArrayOfStates());
            Fuse_Bed_UpdateState2(this, new RoutedEventArgs());
        }

        private void Fuse_TV_Click(object sender, RoutedEventArgs e)
        {
            fuseTVFlag = !fuseTVFlag;
            if (fuseTVFlag == false)
                fuseTVFlag2 = false;
            buttonConnector.Send(ReturnActualArrayOfStates());

            Fuse_TV_UpdateState(this, new RoutedEventArgs());
        }
        private void Fuse_TV_Click2(object sender, RoutedEventArgs e)
        {
            fuseTVFlag2 = !fuseTVFlag2;
            buttonConnector.Send(ReturnActualArrayOfStates());

            Fuse_TV_UpdateState2(this, new RoutedEventArgs());
        }

        // обновление состояния горелок
        private void Fuse_Door_UpdateState(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Zapal(Img_But_Door, Img_But_Door2, Door, Fuse_Door, Fuse_Door2, ref fuseDoorFlag, ref fuseDoorFlag2, 0);
        }
        private void Fuse_Door_UpdateState2(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Osnov(Img_But_Door2, Door, Fuse_Door2, ref fuseDoorFlag, ref fuseDoorFlag2, 0);
        }

        private void Fuse_Flange_UpdateState_Low(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Zapal(Img_But_Flange_Low, Img_But_Flange2_Low, Flange, Fuse_Flange_Low, Fuse_Flange2_Low, ref fuseFlangerFlag_Low, ref fuseFlangerFlag2_Low, 1);
            Set_Img_FlangeCondition();
        }
        private void Fuse_Flange_UpdateState2_Low(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Osnov(Img_But_Flange2_Low, Flange, Fuse_Flange2_Low, ref fuseFlangerFlag_Low, ref fuseFlangerFlag2_Low, 1);
            Set_Img_FlangeCondition();
        }

        private void Fuse_Flange_UpdateState_High(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Zapal(Img_But_Flange_High, Img_But_Flange2_High, Flange, Fuse_Flange_High, Fuse_Flange2_High, ref fuseFlangerFlag_High, ref fuseFlangerFlag2_High, 1);
            Set_Img_FlangeCondition();
        }
        private void Fuse_Flange_UpdateState2_High(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Osnov(Img_But_Flange2_High, Flange, Fuse_Flange2_High, ref fuseFlangerFlag_High, ref fuseFlangerFlag2_High, 1);
            Set_Img_FlangeCondition();
        }

        private void Fuse_Cylinders_UpdateState(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Zapal(Img_But_Cylinders, Img_But_Cylinders2, Cylinders, Fuse_Cylinders, Fuse_Cylinders2, ref fuseCylindersFlag, ref fuseCylindersFlag2, 2);
        }
        private void Fuse_Cylinders_UpdateState2(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Osnov(Img_But_Cylinders2, Cylinders, Fuse_Cylinders2, ref fuseCylindersFlag, ref fuseCylindersFlag2, 2);
        }

        private void Fuse_Ceiling_UpdateState(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Zapal(Img_But_Ceiling, Img_But_Ceiling2, Ceiling, Fuse_Ceiling, Fuse_Ceiling2, ref fuseCeilingFlag, ref fuseCeilingFlag2, 3);
        }
        private void Fuse_Ceiling_UpdateState2(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Osnov(Img_But_Ceiling2, Ceiling, Fuse_Ceiling2, ref fuseCeilingFlag, ref fuseCeilingFlag2, 3);
        }

        private void Fuse_Bed_UpdateState(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Zapal(Img_But_Bed, Img_But_Bed2, Bed, Fuse_Bed, Fuse_Bed2, ref fuseBedFlag, ref fuseBedFlag2, 4);
        }
        private void Fuse_Bed_UpdateState2(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Osnov(Img_But_Bed2, Bed, Fuse_Bed2, ref fuseBedFlag, ref fuseBedFlag2, 4);
        }

        private void Fuse_TV_UpdateState(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Zapal(Img_But_TV, Img_But_TV2, TV, Fuse_TV, Fuse_TV2, ref fuseTVFlag, ref fuseTVFlag2, 5);
        }
        private void Fuse_TV_UpdateState2(object sender, RoutedEventArgs e)
        {
            Fuse_Click_Osnov(Img_But_TV2, TV, Fuse_TV2, ref fuseTVFlag, ref fuseTVFlag2, 5);
        }

        private void Set_Img_FlangeCondition()
        {
            if (!fuseFlangerFlag_Low && !fuseFlangerFlag_High)
            {
                Flange.Source = bm[1];
            }
            else if (fuseFlangerFlag_Low && !fuseFlangerFlag_High)
            {
                if (fuseFlangerFlag2_Low)
                {
                    Flange.Source = img_Flange_LowActive_Low2Active_HighNotActive;
                }
                else
                {
                    Flange.Source = img_Flange_LowActive_HighNotActive;
                }
            }
            else if (!fuseFlangerFlag_Low && fuseFlangerFlag_High)
            {
                if (fuseFlangerFlag2_High)
                {
                    Flange.Source = img_Flange_LowNotActive_HighActive_High2Active;
                }
                else
                {
                    Flange.Source = img_Flange_LowNotActive_HighActive;
                }
            }
            else if (fuseFlangerFlag_Low && fuseFlangerFlag_High)
            {
                if (!fuseFlangerFlag2_Low && !fuseFlangerFlag2_High)
                {
                    Flange.Source = img_Flange_LowActive_HighActive;
                }
                else if (fuseFlangerFlag2_Low && !fuseFlangerFlag2_High)
                {
                    Flange.Source = img_Flange_LowActive_Low2Active_HighActive;
                }
                else if (!fuseFlangerFlag2_Low && fuseFlangerFlag2_High)
                {
                    Flange.Source = img_Flange_LowActive_HighActive_High2Active;
                }
                else if (fuseFlangerFlag2_Low && fuseFlangerFlag2_High)
                {
                    Flange.Source = img_Flange_LowActive_Low2Active_HighActive_High2Active;
                }
            }
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
                button_Osnov.IsEnabled = true;

                /* fuseFlag_Zapal = false;
                 fuseFlag_Osnov = true;*/

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
                button_Osnov.IsEnabled = false;

                fuseFlag_Osnov = false;
                img_Zapal.Source = bm[numImgInMassive];
            }
        }
        private void Fuse_Click_Osnov(Image img_but_Osnov, Image img_Osnov, Button button_Osnov, ref bool fuseFlag_Zapal, ref bool fuseFlag_Osnov, int numImgInMassive)
        {
            if (fuseFlag_Zapal)
            {
                if (fuseFlag_Osnov)
                {
                    img_but_Osnov.Source = bmButtonRed;
                    button_Osnov.Foreground = Brushes.White;
                    //fuseFlag_Osnov = false;
                    img_Osnov.Source = bmFire2[numImgInMassive];
                }
                else
                {
                    img_but_Osnov.Source = bmButtonGrey;
                    button_Osnov.Foreground = Brushes.Black;
                    //fuseFlag_Osnov = true;
                    img_Osnov.Source = bmFire1[numImgInMassive];
                }
            }
        }

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
                    PromZoneTemp.Content = e.PromZoneTemp + " °C";
                    GazhranZoneTemp.Content = e.GazohranTemp + " °C";
                    LizeZoneTemp.Content = e.LiveZoneTemp + " °C";
                    ChangeMonometrValue(e.ManometrValue);

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
                    AlarmChangeState(e.BoxAlarmArgs[0][0]);
                    WorkModeChangeState(e.WorkModeArgs[0][0]);
                });
            }
            catch (Exception ex)
            {

            }
        }

        private void AlarmChangeState(byte state)
        {
            AlarmUpdate(Convert.ToBoolean(state));
        }

        private void WorkModeChangeState(byte state)
        {
            WorkModeUpdate(Convert.ToBoolean(state));
        }

        private void AlarmUpdate(bool state)
        {
            if (!state)
            {
                AlarmBorder.Background = new SolidColorBrush(new Color { R = 90, G = 193, B = 85, A = 255 });
                AlarmText.Content = "Состояние: исправно";
            }
            else
            {
                AlarmBorder.Background = new SolidColorBrush(new Color { R = 255, G = 0, B = 0, A = 255 });
                AlarmText.Content = "Состояние: АВАРИЯ";
            }
        }

        private void WorkModeUpdate(bool state)
        {
            if (!state)
            {
                Mode_Value_text.Content = "Автоматический";
            }
            else
            {
                Mode_Value_text.Content = "Ручной";
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
            Fuse_Ceiling_UpdateState(this, new RoutedEventArgs());
            fuseCeilingFlag2 = Convert.ToBoolean(newState[1]);
            Fuse_Ceiling_UpdateState2(this, new RoutedEventArgs());
        }

        private void UpdateFuseTV(byte[] newState)
        {
            fuseTVFlag = Convert.ToBoolean(newState[0]);
            Fuse_TV_UpdateState(this, new RoutedEventArgs());
            fuseTVFlag2 = Convert.ToBoolean(newState[1]);
            Fuse_TV_UpdateState2(this, new RoutedEventArgs());
        }

        private void UpdateFuseBed(byte[] newState)
        {
            fuseBedFlag = Convert.ToBoolean(newState[0]);
            Fuse_Bed_UpdateState(this, new RoutedEventArgs());
            fuseBedFlag2 = Convert.ToBoolean(newState[1]);
            Fuse_Bed_UpdateState2(this, new RoutedEventArgs());
        }

        private void UpdateFuseDoor(byte[] newState)
        {
            fuseDoorFlag = Convert.ToBoolean(newState[0]);
            Fuse_Door_UpdateState(this, new RoutedEventArgs());
            fuseDoorFlag2 = Convert.ToBoolean(newState[1]);
            Fuse_Door_UpdateState2(this, new RoutedEventArgs());
        }

        private void UpdateFuseCylinder(byte[] newState)
        {
            fuseCylindersFlag = Convert.ToBoolean(newState[0]);
            Fuse_Cylinders_UpdateState(this, new RoutedEventArgs());
            fuseCylindersFlag2 = Convert.ToBoolean(newState[1]);
            Fuse_Cylinders_UpdateState2(this, new RoutedEventArgs());
        }

        private void UpdateFusePipe1(byte[] newState)
        {
            fuseFlangerFlag_Low = Convert.ToBoolean(newState[0]);
            Fuse_Flange_UpdateState_Low(this, new RoutedEventArgs());
            fuseFlangerFlag2_Low = Convert.ToBoolean(newState[1]);
            Fuse_Flange_UpdateState2_Low(this, new RoutedEventArgs());
        }

        private void UpdateFusePipe2(byte[] newState)
        {
            fuseFlangerFlag_High = Convert.ToBoolean(newState[0]);
            Fuse_Flange_UpdateState_High(this, new RoutedEventArgs());
            fuseFlangerFlag2_High = Convert.ToBoolean(newState[1]);
            Fuse_Flange_UpdateState2_High(this, new RoutedEventArgs());
        }
    }
}
