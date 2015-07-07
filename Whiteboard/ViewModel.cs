using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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
    class ViewModel
    {
        public MainWindow view;
        public Model model;

        public ViewModel(MainWindow v, Model m)
        {
            view = v;
            model = m;
        }

        public void draw(Line l)
        {
            //Adds the new line to the canvas
            model.c.Children.Add(l);
            view.update();
        }
    }
}
