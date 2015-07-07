using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

/*
 *  Copyright 2012 Marc-André Bär
 
 *  This project is for educational use and part of the workshop "Introduction to C#" from Marc-André Bär.
 *  Task summary: Create a simple Whiteboard
 
 *  This file is part of Demo2-Whiteboard.

    Demo2-Whiteboard is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Demo2-Whiteboard is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Demo2-Whiteboard.  If not, see <http://www.gnu.org/licenses/>.
  
 */
namespace Whiteboard
{
    /// <summary>
    /// Interactionlogic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Model model;
        private ViewModel controller;

        public MainWindow()
        {
            //Init Components
            InitializeComponent();
            model = new Model();
            controller = new ViewModel(this, model);
            //Add event for mouse movement
            this.MouseMove += new MouseEventHandler(canvas_MouseMove);
            canvas.Children.Add(model.c);
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (true) //For demonstration use we always draw the line. Can be modified with Mouse_Pressed etc.
            {
                //Draws a new point/line on the mouse over event
                var myLine = new Line();
                myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
                myLine.X1 = e.GetPosition(sender as Window).X;
                myLine.X2 = e.GetPosition(sender as Window).X + 2;
                myLine.Y1 = e.GetPosition(sender as Window).Y;
                myLine.Y2 = e.GetPosition(sender as Window).Y + 2;
                myLine.StrokeThickness = 2;
                controller.draw(myLine);            
            }
        }

        public void update()
        {
            //canvas.Background = model.c.;
        }
    }
}
