using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
/*
textBox1.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0, 0));


*/
namespace ColourAtlas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StackPanel s = new StackPanel();
        SolidColorBrush brushColor = new SolidColorBrush();
        Color stackPanelColor;
        //Rectangle rectangle = new Rectangle();
        bool colorsMargin;
        public int canvasHeight;
        public int canvasWidth;
        int cycleorder = 1;
        public int cBoxHeight;
        public int cBoxWidth;
        string path = "colors.txt";
        string space = " ";
        StreamWriter writer;
        int displayedColors;
        Thickness border;
        Thickness panelThickness;
        int temp;

      
        

        public int crMin, crMax, cgMin, cgMax, cbMin, cbMax, rts, gts, bts, chMarg;

        private void texto_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void rMin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public int tempR, tempG, tempB;
        public float cvMarg;


        private void vStep_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void colorBoxMaxWidth_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public MainWindow()
        {
            InitializeComponent();

            writer = new StreamWriter(path, true);
            var ape_not_kill_ape = new ObservableCollection<int> { 1,2,3,4,5,6 };
            items_box.ItemsSource = ape_not_kill_ape;
            items_box.SelectedIndex = 0;
        }



        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void createImage_Click(object sender, RoutedEventArgs e)
        {
            ImageShow win2 = new ImageShow();
            win2.Show();
            /*
            VisualBrush myVisualBrush = new VisualBrush();

             // Create the visual brush's contents.
             StackPanel myStackPanel = new StackPanel();
             myStackPanel.Background = Brushes.White;

             Rectangle redRectangle = new Rectangle();
             redRectangle.Width = 25;
             redRectangle.Height = 25;
             redRectangle.Fill = Brushes.Red;
             redRectangle.Margin = new Thickness(2);
             myStackPanel.Children.Add(redRectangle);

             TextBlock someText = new TextBlock();
             FontSizeConverter myFontSizeConverter = new FontSizeConverter();
             someText.FontSize = (double)myFontSizeConverter.ConvertFrom("10pt");
             someText.Text = "Hello, World!";
             someText.Margin = new Thickness(2);
             myStackPanel.Children.Add(someText);

             Button aButton = new Button();
             aButton.Content = "A Button";
            aButton.Margin = new Thickness(2);
             myStackPanel.Children.Add(aButton);

             // Use myStackPanel as myVisualBrush's content.
             myVisualBrush.Visual = myStackPanel;

             // Create a rectangle to paint.
             Rectangle myRectangle = new Rectangle();
             myRectangle.Width = 150;
             myRectangle.Height = 150;
             myRectangle.Stroke = Brushes.Black;
             myRectangle.Margin = new Thickness(5, 0, 5, 0);

             // Use myVisualBrush to paint myRectangle.
             win2.imageRectange.Fill = myVisualBrush;
            */

            ///////////
            ///SETUP SIZES
            ///
            
            if(bts<1) bts = 1;
            if(gts<1) gts = 1;  
            if(rts<1) rts = 1;






            {
                if (Int32.Parse(canvasSizeHeight.Text) > 4320)
                {
                    canvasHeight = 4320;
                }
                else
                {
                    canvasHeight = Int32.Parse(canvasSizeHeight.Text);
                }
                if (Int32.Parse(canvasSizeWidth.Text) > 7680)
                {
                    canvasWidth = 7680;
                }
                else
                {
                    canvasWidth = Int32.Parse(canvasSizeWidth.Text);
                }

                // win2.imageRectange.Height = win2.imageCanvas.Height;
                // win2.imageRectange.Width = win2.imageCanvas.Width;
                if (Int32.Parse(colorBoxMaxHeight.Text) > 50)
                {
                    cBoxHeight = 50;
                }
                else
                {
                    cBoxHeight = Int32.Parse(colorBoxMaxHeight.Text);
                }
                if (Int32.Parse(colorBoxMaxWidth.Text) > 50)
                {
                    cBoxWidth = 50;
                }
                else
                {
                    cBoxWidth = Int32.Parse(colorBoxMaxWidth.Text);
                }

                if (Int32.Parse(rMin.Text) > 254)
                {
                    crMin = 254;
                }
                else
                {
                    crMin = Int32.Parse(rMin.Text);
                }
                if (Int32.Parse(rMax.Text) > 255)
                {
                    crMax = 255;
                }
                else
                {
                    crMax = Int32.Parse(rMax.Text);
                }
                if (Int32.Parse(gMin.Text) > 254)
                {
                    cgMin = 254;
                }
                else
                {
                    cgMin = Int32.Parse(gMin.Text);
                }
                if (Int32.Parse(gMax.Text) > 255)
                {
                    cgMax = 255;
                }
                else
                {
                    cgMax = Int32.Parse(gMax.Text);
                }
                if (Int32.Parse(bMin.Text) > 254)
                {
                    cbMin = 254;
                }
                else
                {
                    cbMin = Int32.Parse(bMin.Text);
                }
                if (Int32.Parse(bMax.Text) > 255)
                {
                    cbMax = 255;
                }
                else
                {
                    cbMax = Int32.Parse(bMax.Text);
                }

                rts = Int32.Parse(rToneStep.Text);
                gts = Int32.Parse(gToneStep.Text);
                bts = Int32.Parse(bToneStep.Text);

                if (canvasWidth == 0)
                {
                    canvasWidth = (int)Math.Floor((((float)(crMax - crMin) / (float)rts) * (float)cBoxWidth));
                }
                if (canvasHeight == 0)
                {
                    double rowBoxCount = canvasWidth / cBoxWidth;
                    //MessageBox.Show(rowBoxCount.ToString()+"  "+canvasWidth.ToString()+"  "+cBoxWidth);
                    rowBoxCount = Math.Floor(rowBoxCount);
                    var colorBoxesAmount = ((crMax-crMin)*(cgMax-cgMin)*(cbMax-cbMin))/(rts*bts*gts);
                    // MessageBox.Show(colorBoxesAmount.ToString());
                    var stackPanelsAmount = Math.Floor(colorBoxesAmount / rowBoxCount);
                    canvasHeight = ((int)stackPanelsAmount * cBoxHeight) + (((int)stackPanelsAmount*cBoxHeight * Int32.Parse(vMarg.Text))) + 100;
                }

                win2.imageCanvas.Height = canvasHeight;
                win2.imageCanvas.Width = canvasWidth;
            }
            ///
            ////////////

            /// refs
            /// win2.imageCanvas
            /// win2.imageRectangle
            ///
            buildPalette3(win2);

        }
        // GOOD WORKING METHOD BUT NOT PERFECT
        public void buildPalette(ImageShow win2)
        {

            int lastRowR, lastRowG, lastRowB;
            tempR = crMin;
            tempG = cgMin;
            tempB = cbMin;
            cvMarg = float.Parse(vMarg.Text);
            chMarg = Int32.Parse(hMarg.Text);
            List<StackPanel> stackPanels = new List<StackPanel>();
            int stackPanelsCounter = 0;
            double rowBoxCount = canvasWidth / cBoxWidth;
            // MessageBox.Show(tempR.ToString());
            rowBoxCount = Math.Floor(rowBoxCount);
            var colorBoxesAmount = Math.Floor((float)((((crMax - crMin) / (float)rts) * ((cgMax - cgMin) / (float)gts) * ((cbMax - cbMin) / (float)bts))));
            var stackPanelsAmount = Math.Floor(colorBoxesAmount / rowBoxCount) + 1;
            AddNewStackPanel(stackPanels, stackPanelsCounter);

           
            /*
            for (int k = 0; k < stackPanelsAmount+1; k++)
            {
                StackPanel s = new StackPanel();
                s.Orientation = Orientation.Horizontal;
                s.HorizontalAlignment = HorizontalAlignment.Left;
                s.Margin = new Thickness(0, (k * cBoxHeight*(1+(cvMarg / 10))), 0, 5);
                stackPanels.Add(s);
            }
            */
            /*
            StackPanel myStackPanel = new StackPanel();
            StackPanel myStackPanel1 = new StackPanel();
            myStackPanel.Background = Brushes.White;
            myStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            myStackPanel.Orientation = Orientation.Horizontal;
            
            myStackPanel1.HorizontalAlignment = HorizontalAlignment.Left;
            myStackPanel1.Orientation = Orientation.Horizontal;
            myStackPanel1.Margin = new Thickness(0, 20, 0, 0);
            */
            for (int redC = tempR; redC < crMax; redC++)
            {
                for (int greenC = tempG; greenC < cgMax; greenC++)
                {
                    for (int blueC = tempB; blueC < cbMax; blueC++)
                    {
                        for (int b = 0; b < rowBoxCount; b++)
                        {
                           var rectangle = new Rectangle();
                            rectangle.Width = cBoxWidth;
                            rectangle.Height = cBoxHeight;
                            rectangle.Margin = border; 

                            stackPanelColor = Color.FromArgb(255, (byte)redC, (byte)greenC, (byte)blueC);

                            //brushColor.Color = stackPanelColor;

                            brushColor = new SolidColorBrush(stackPanelColor);


                            rectangle.Fill = brushColor;
                            stackPanels[stackPanelsCounter].Children.Add(rectangle);
                            blueC += bts;
                        }
                        AddNewStackPanel(stackPanels, stackPanelsCounter);
                        //stackPanels.Clear();
                        stackPanelsCounter++;

                    }

                    greenC += gts;

                }
                redC += rts;
            }

          //  canvasHeight = (int)(((int)stackPanels.Count * cBoxHeight)*1.1f); //+ (((int)stackPanels.Count) * (cBoxHeight * Int32.Parse(vMarg.Text))) + 100;
            /*
            if (canvasHeight > 65535)
            {
                canvasHeight = 65000;
            }
            */
            win2.imageCanvas.Height = canvasHeight;
            for (temp=0;temp< stackPanels.Count;temp++)
            {
                if (stackPanels[temp].Parent == null)
                {
                    win2.imageCanvas.Children.Add(stackPanels[temp]);

                }

            }

            MessageBox.Show(stackPanels.Count.ToString() + " Amount of stackPanels" + "\n" +
                            colorBoxesAmount.ToString() + " overall color boxes count" + "\n" +
                            rowBoxCount.ToString() + " color boxes in a row" + "\n" +
                            canvasWidth.ToString() + " Canvas width" + "\n" +
                            canvasHeight.ToString() + " Canvas height"




                            );

            /*
            VisualBrush myVisualBrush = new VisualBrush();
            myVisualBrush.Visual = myStackPanel;
            win2.imageCanvas.Children.Add(myStackPanel);
            win2.imageCanvas.Children.Add(myStackPanel1);
            */




        }

        
        public void buildPalette2(ImageShow win2)
        {

            int lastRowR, lastRowG, lastRowB;
            tempR = crMin;
            tempG = cgMin;
            tempB = cbMin;
            cvMarg = float.Parse(vMarg.Text);
            chMarg = Int32.Parse(hMarg.Text);
            List<StackPanel> stackPanels = new List<StackPanel>();
            int stackPanelsCounter = 0;
            double rowBoxCount = canvasWidth / cBoxWidth;
            // MessageBox.Show(tempR.ToString());
            rowBoxCount = Math.Floor(rowBoxCount);
            var colorBoxesAmount = Math.Floor((float)((((crMax - crMin) / (float)rts) * ((cgMax - cgMin) / (float)gts) * ((cbMax - cbMin) / (float)bts))));
            var stackPanelsAmount = Math.Floor(colorBoxesAmount / rowBoxCount) + 1;
            AddNewStackPanel(stackPanels, stackPanelsCounter);
            /*
            for (int k = 0; k < stackPanelsAmount+1; k++)
            {
                StackPanel s = new StackPanel();
                s.Orientation = Orientation.Horizontal;
                s.HorizontalAlignment = HorizontalAlignment.Left;
                s.Margin = new Thickness(0, (k * cBoxHeight*(1+(cvMarg / 10))), 0, 5);
                stackPanels.Add(s);
            }
            */
            /*
            StackPanel myStackPanel = new StackPanel();
            StackPanel myStackPanel1 = new StackPanel();
            myStackPanel.Background = Brushes.White;
            myStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            myStackPanel.Orientation = Orientation.Horizontal;
            
            myStackPanel1.HorizontalAlignment = HorizontalAlignment.Left;
            myStackPanel1.Orientation = Orientation.Horizontal;
            myStackPanel1.Margin = new Thickness(0, 20, 0, 0);
            */
            for (int redC = tempR; redC < crMax; redC++)
            {
                for (int greenC = tempG; greenC < cgMax; greenC++)
                {
                    for (int blueC = tempB; blueC < cbMax; blueC++)
                    {
                        for (int b = 0; b < rowBoxCount; b++)
                        {
                           var rectangle = new Rectangle();
                            rectangle.Width = cBoxWidth;
                            rectangle.Height = cBoxHeight;
                            rectangle.Margin = new Thickness(chMarg, 0, chMarg, 0);

                            stackPanelColor = Color.FromArgb(255, (byte)redC, (byte)greenC, (byte)blueC);

                            //brushColor.Color = stackPanelColor;

                            brushColor = new SolidColorBrush(stackPanelColor);


                            rectangle.Fill = brushColor;
                            stackPanels[stackPanelsCounter].Children.Add(rectangle);
                            blueC += bts;
                        }
                        AddNewStackPanel(stackPanels, stackPanelsCounter);
                        //stackPanels.Clear();
                        stackPanelsCounter++;

                    }

                    greenC += gts;

                }
                redC += rts;
            }

            canvasHeight = (int)(((int)stackPanels.Count * cBoxHeight) * 1.1f); //+ (((int)stackPanels.Count) * (cBoxHeight * Int32.Parse(vMarg.Text))) + 100;
            if (canvasHeight > 65535)
            {
                canvasHeight = 65000;
            }
            win2.imageCanvas.Height = canvasHeight;
            for (temp = 0; temp < stackPanels.Count; temp++)
            {
                if (stackPanels[temp].Parent == null)
                {
                    win2.imageCanvas.Children.Add(stackPanels[temp]);

                }

            }

            MessageBox.Show(stackPanels.Count.ToString() + " Amount of stackPanels" + "\n" +
                            colorBoxesAmount.ToString() + " overall color boxes count" + "\n" +
                            rowBoxCount.ToString() + " color boxes in a row" + "\n" +
                            canvasWidth.ToString() + " Canvas width" + "\n" +
                            canvasHeight.ToString() + " Canvas height"




                            );

            /*
            VisualBrush myVisualBrush = new VisualBrush();
            myVisualBrush.Visual = myStackPanel;
            win2.imageCanvas.Children.Add(myStackPanel);
            win2.imageCanvas.Children.Add(myStackPanel1);
            */




        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cycleorder = items_box.SelectedIndex+1;
        }

        public void buildPalette3(ImageShow win2)
        {
            int colorBoxCount=0;
            int boxesInSingleRow = ((crMax - crMin)+(cgMax - cgMin)+(cbMax - cbMin))/(bts+gts+rts);
           // MessageBox.Show(boxesInSingleRow.ToString() + " THAT BOXES SHOULD BE IN ONE LINE");
            




            int lastRowR, lastRowG, lastRowB;
            tempR = crMin;
            tempG = cgMin;
            tempB = cbMin;
            cvMarg = float.Parse(vMarg.Text);
            chMarg = Int32.Parse(hMarg.Text);
            List<StackPanel> stackPanels = new List<StackPanel>();
            int stackPanelsCounter = 0;
            double rowBoxCount = canvasWidth / cBoxWidth;
            // MessageBox.Show(tempR.ToString());
            rowBoxCount = Math.Floor(rowBoxCount);
            var colorBoxesAmount = Math.Floor((float)((((crMax - crMin) / (float)rts) * ((cgMax - cgMin) / (float)gts) * ((cbMax - cbMin) / (float)bts))));
            var stackPanelsAmount = Math.Floor(colorBoxesAmount / rowBoxCount) + 1;
            AddNewStackPanel(stackPanels, stackPanelsCounter);
            border = new Thickness(chMarg, 0, chMarg, 0);

            if (colorsMargin == false)
            {
                boxesInSingleRow = (int)rowBoxCount;
            }

            if(cycleorder==1)
               shuffle_colors1(stackPanels, colorBoxCount, boxesInSingleRow, stackPanelsCounter);
            else if(cycleorder == 2)
                shuffle_colors2(stackPanels, colorBoxCount, boxesInSingleRow, stackPanelsCounter);
            else if (cycleorder == 3)
                shuffle_colors3(stackPanels, colorBoxCount, boxesInSingleRow, stackPanelsCounter);
            else if (cycleorder == 4)
                shuffle_colors4(stackPanels, colorBoxCount, boxesInSingleRow, stackPanelsCounter);
            else if (cycleorder == 5)
                shuffle_colors5(stackPanels, colorBoxCount, boxesInSingleRow, stackPanelsCounter);
            else if (cycleorder == 6)
                shuffle_colors6(stackPanels, colorBoxCount, boxesInSingleRow, stackPanelsCounter);
            //shuffle_colors1(stackPanels, colorBoxCount, boxesInSingleRow, stackPanelsCounter);


            win2.imageCanvas.Height = (((colorBoxesAmount * cBoxHeight) + ((colorBoxesAmount * (cBoxHeight/2) * Int32.Parse(vMarg.Text))) + 100)/ (boxesInSingleRow))*(Int32.Parse(heightMult.Text));
            for (temp = 0; temp < stackPanels.Count; temp++)
            {
                if (stackPanels[temp].Parent == null)
                {
                    win2.imageCanvas.Children.Add(stackPanels[temp]);
                }

            }

           
        }

        public void AddNewStackPanel(List<StackPanel> staP, int panelCounter)
        {
            panelThickness = new Thickness(0, (panelCounter * cBoxHeight * (1 + (cvMarg / 10))), 0, 5);
            s = new StackPanel();
            s.Orientation = Orientation.Horizontal;
            s.HorizontalAlignment = HorizontalAlignment.Left;
            s.Margin = panelThickness;
            staP.Add(s);


        }
        public void AddNewStackPanel2(List<StackPanel> staP, int panelCounter)
        {
            panelThickness = new Thickness(0, (panelCounter * cBoxHeight * (1 + (cvMarg / 10))), 0, 5);
            s = new StackPanel();
            s.Orientation = Orientation.Horizontal;
            s.HorizontalAlignment = HorizontalAlignment.Left;
            s.Margin = panelThickness;
            staP.Add(s);


        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            colorsMargin = true;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            colorsMargin = false;
        }


        #region colors cycle fun code
        private void shuffle_colors1(List<StackPanel> stackPanels, Double colorBoxCount, int boxesInSingleRow, int stackPanelsCounter)
        {

            for (int redC = tempR; redC < crMax; redC++)
            {
                for (int greenC = tempG; greenC < cgMax; greenC++)
                {
                    for (int blueC = tempB; blueC < cbMax; blueC++)
                    {
                        // for (int b = 0; b < rowBoxCount; b++)
                        // {
                        var rectangle = new Rectangle();
                        rectangle.Width = cBoxWidth;
                        rectangle.Height = cBoxHeight;
                        rectangle.Margin = border;

                        stackPanelColor = Color.FromRgb((byte)redC, (byte)greenC, (byte)blueC);

                        //brushColor.Color = stackPanelColor;
                        writer.Write(redC + space + greenC + space + blueC + Environment.NewLine);

                        brushColor = new SolidColorBrush(stackPanelColor);
                        displayedColors++;

                        rectangle.Fill = brushColor;
                        stackPanels[stackPanelsCounter].Children.Add(rectangle);
                        colorBoxCount++;
                        // }

                        if (colorBoxCount >= boxesInSingleRow)
                        {
                            AddNewStackPanel(stackPanels, stackPanelsCounter);
                            stackPanelsCounter++;
                            colorBoxCount = 0;
                        }

                        //stackPanels.Clear();

                        blueC += bts - 1;
                    }

                    greenC += gts - 1;

                }
                redC += rts - 1;
            }



        }

        private void shuffle_colors2(List<StackPanel> stackPanels, Double colorBoxCount, int boxesInSingleRow, int stackPanelsCounter)
        {

            for (int redC = tempR; redC < crMax; redC++)
            {
                for (int blueC = tempB; blueC < cbMax; blueC++)//int blueC = tempB; blueC < cbMax; blueC++
                {
                    for (int greenC = tempG; greenC < cgMax; greenC++)//
                    {
                        // for (int b = 0; b < rowBoxCount; b++)
                        // {
                        var rectangle = new Rectangle();
                        rectangle.Width = cBoxWidth;
                        rectangle.Height = cBoxHeight;
                        rectangle.Margin = border;

                        stackPanelColor = Color.FromRgb((byte)redC, (byte)greenC, (byte)blueC);

                        //brushColor.Color = stackPanelColor;
                        writer.Write(redC + space + greenC + space + blueC + Environment.NewLine);

                        brushColor = new SolidColorBrush(stackPanelColor);
                        displayedColors++;

                        rectangle.Fill = brushColor;
                        stackPanels[stackPanelsCounter].Children.Add(rectangle);
                        colorBoxCount++;
                        // }

                        if (colorBoxCount >= boxesInSingleRow)
                        {
                            AddNewStackPanel(stackPanels, stackPanelsCounter);
                            stackPanelsCounter++;
                            colorBoxCount = 0;
                        }

                        //stackPanels.Clear();

                        greenC += gts - 1;
                    }

                    blueC += bts - 1;

                }
                redC += rts - 1;
            }



        }
        private void shuffle_colors3(List<StackPanel> stackPanels, Double colorBoxCount, int boxesInSingleRow, int stackPanelsCounter)
        {

            for (int blueC = tempB; blueC < cbMax; blueC++)//int redC = tempR; redC < crMax; redC++
            {
                for (int redC = tempR; redC < crMax; redC++)//int greenC = tempG; greenC < cgMax; greenC++
                {
                    for (int greenC = tempG; greenC < cgMax; greenC++)//int blueC = tempB; blueC < cbMax; blueC++
                    {
                        // for (int b = 0; b < rowBoxCount; b++)
                        // {
                        var rectangle = new Rectangle();
                        rectangle.Width = cBoxWidth;
                        rectangle.Height = cBoxHeight;
                        rectangle.Margin = border;

                        stackPanelColor = Color.FromRgb((byte)redC, (byte)greenC, (byte)blueC);

                        //brushColor.Color = stackPanelColor;
                        writer.Write(redC + space + greenC + space + blueC + Environment.NewLine);

                        brushColor = new SolidColorBrush(stackPanelColor);
                        displayedColors++;

                        rectangle.Fill = brushColor;
                        stackPanels[stackPanelsCounter].Children.Add(rectangle);
                        colorBoxCount++;
                        // }

                        if (colorBoxCount >= boxesInSingleRow)
                        {
                            AddNewStackPanel(stackPanels, stackPanelsCounter);
                            stackPanelsCounter++;
                            colorBoxCount = 0;
                        }

                        //stackPanels.Clear();

                        greenC += gts - 1;// blueC += bts - 1;
                    }

                    redC += rts - 1;// greenC += gts - 1;

                }
                blueC += bts - 1;// redC += rts - 1;
            }



        }

        private void shuffle_colors4(List<StackPanel> stackPanels, Double colorBoxCount, int boxesInSingleRow, int stackPanelsCounter)
        {

            for (int blueC = tempB; blueC < cbMax; blueC++)//int redC = tempR; redC < crMax; redC++
            {
                for (int greenC = tempG; greenC < cgMax; greenC++)//int greenC = tempG; greenC < cgMax; greenC++
                {
                    for (int redC = tempR; redC < crMax; redC++)//int blueC = tempB; blueC < cbMax; blueC++
                    {
                        // for (int b = 0; b < rowBoxCount; b++)
                        // {
                        var rectangle = new Rectangle();
                        rectangle.Width = cBoxWidth;
                        rectangle.Height = cBoxHeight;
                        rectangle.Margin = border;

                        stackPanelColor = Color.FromRgb((byte)redC, (byte)greenC, (byte)blueC);

                        //brushColor.Color = stackPanelColor;
                        writer.Write(redC + space + greenC + space + blueC + Environment.NewLine);

                        brushColor = new SolidColorBrush(stackPanelColor);
                        displayedColors++;

                        rectangle.Fill = brushColor;
                        stackPanels[stackPanelsCounter].Children.Add(rectangle);
                        colorBoxCount++;
                        // }

                        if (colorBoxCount >= boxesInSingleRow)
                        {
                            AddNewStackPanel(stackPanels, stackPanelsCounter);
                            stackPanelsCounter++;
                            colorBoxCount = 0;
                        }

                        //stackPanels.Clear();

                        redC += rts - 1;// blueC += bts - 1;
                    }

                    greenC += gts - 1;// greenC += gts - 1;

                }
                blueC += bts - 1;// redC += rts - 1;
            }



        }
        private void shuffle_colors5(List<StackPanel> stackPanels, Double colorBoxCount, int boxesInSingleRow, int stackPanelsCounter)
        {

            for (int greenC = tempG; greenC < cgMax; greenC++)//int redC = tempR; redC < crMax; redC++
            {
                for (int redC = tempR; redC < crMax; redC++)//int greenC = tempG; greenC < cgMax; greenC++
                {
                    for (int blueC = tempB; blueC < cbMax; blueC++)//int blueC = tempB; blueC < cbMax; blueC++
                    {
                        // for (int b = 0; b < rowBoxCount; b++)
                        // {
                        var rectangle = new Rectangle();
                        rectangle.Width = cBoxWidth;
                        rectangle.Height = cBoxHeight;
                        rectangle.Margin = border;

                        stackPanelColor = Color.FromRgb((byte)redC, (byte)greenC, (byte)blueC);

                        //brushColor.Color = stackPanelColor;
                        writer.Write(redC + space + greenC + space + blueC + Environment.NewLine);

                        brushColor = new SolidColorBrush(stackPanelColor);
                        displayedColors++;

                        rectangle.Fill = brushColor;
                        stackPanels[stackPanelsCounter].Children.Add(rectangle);
                        colorBoxCount++;
                        // }

                        if (colorBoxCount >= boxesInSingleRow)
                        {
                            AddNewStackPanel(stackPanels, stackPanelsCounter);
                            stackPanelsCounter++;
                            colorBoxCount = 0;
                        }

                        //stackPanels.Clear();

                        blueC += bts - 1;// blueC += bts - 1;
                    }

                    redC += rts - 1;// greenC += gts - 1;

                }
                greenC += gts - 1;// redC += rts - 1;
            }



        }

        private void shuffle_colors6(List<StackPanel> stackPanels, Double colorBoxCount, int boxesInSingleRow, int stackPanelsCounter)
        {

            for (int greenC = tempG; greenC < cgMax; greenC++)//int redC = tempR; redC < crMax; redC++
            {
                for (int blueC = tempB; blueC < cbMax; blueC++)//int greenC = tempG; greenC < cgMax; greenC++
                {
                    for (int redC = tempR; redC < crMax; redC++)//int blueC = tempB; blueC < cbMax; blueC++
                    {
                        // for (int b = 0; b < rowBoxCount; b++)
                        // {
                        var rectangle = new Rectangle();
                        rectangle.Width = cBoxWidth;
                        rectangle.Height = cBoxHeight;
                        rectangle.Margin = border;

                        stackPanelColor = Color.FromRgb((byte)redC, (byte)greenC, (byte)blueC);

                        //brushColor.Color = stackPanelColor;
                        writer.Write(redC + space + greenC + space + blueC + Environment.NewLine);

                        brushColor = new SolidColorBrush(stackPanelColor);
                        displayedColors++;

                        rectangle.Fill = brushColor;
                        stackPanels[stackPanelsCounter].Children.Add(rectangle);
                        colorBoxCount++;
                        // }

                        if (colorBoxCount >= boxesInSingleRow)
                        {
                            AddNewStackPanel(stackPanels, stackPanelsCounter);
                            stackPanelsCounter++;
                            colorBoxCount = 0;
                        }

                        //stackPanels.Clear();

                        redC += rts - 1;// blueC += bts - 1;
                    }

                    blueC += bts - 1;// greenC += gts - 1;

                }
                greenC += gts - 1;// redC += rts - 1;
            }



        }
        #endregion




    }





}

