using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GridComponent
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        // Number of Grid per row
        private static int MAX = 6;

        private List<List<Grid>> Bundle;
        private double GridLength;
        private StackPanel CurrentRow;

        public MainPage()
        {
            this.InitializeComponent();

            // Obtain Grid's length and Boxs list
            this.GridLength = this.Container.Width / MAX;
            this.Bundle = new List<List<Grid>>();

            this.InitGrid(28);
        }

        /// <summary>
        /// Start Drawing Grid
        /// </summary>
        /// <param name="Count"></param>
        private void InitGrid(int Count)
        {
            this.AddNewRow();
            for (var i=0; i < Count; i++)
            {
                this.AddNewGrid();
            }

            // re-arrange border for the last row
            var LastRow = this.Bundle.Last();
            LastRow.Last().BorderThickness = new Thickness(0, 0, 1, 0);
            LastRow.First().BorderThickness = new Thickness(0, 0, 1, 0);
            var index = 0;
            foreach(var item in LastRow)
            {
                if (index > 0 && index < LastRow.Count - 1)
                {
                    item.BorderThickness = new Thickness(0, 0, 1, 0);
                }
                index++;
            }
        }

        /// <summary>
        /// Generate New Grid Item
        /// </summary>
        /// <returns></returns>
        private Grid GenerateNewGrid()
        {
            Grid grid = new Grid();
            grid.Width = this.GridLength;
            grid.Height = this.GridLength;

            grid.BorderBrush = new SolidColorBrush(Colors.Gray);
            return grid;
        }

        /// <summary>
        /// Add New Grid To Subview
        /// </summary>
        /// <param name="grid"></param>
        private void AddNewGrid()
        {
            if (this.CurrentRow.Children.Count == MAX)
            {
                this.AddNewRow();
            }
            var grid = GenerateNewGrid();
            var LastGrid = this.Bundle.Last();
            LastGrid.Add(grid);

            // add border right to the grid everytime we add new gird
            if (LastGrid.Count > 1)
            {
                LastGrid.First().BorderThickness = new Thickness(0, 0, 1, 1);
                LastGrid.Last().BorderThickness = new Thickness(0, 0, 1, 1);
            }

            this.CurrentRow.Children.Add(grid);
        }

        /// <summary>
        /// Add new row if max value exceed
        /// </summary>
        private void AddNewRow()
        {
            StackPanel row = new StackPanel();
            List<Grid> RowList = new List<Grid>();
            row.Orientation = Orientation.Horizontal;
            this.Container.Children.Add(row);
            this.CurrentRow = row;
            this.Bundle.Add(RowList);
        }
    }
}
