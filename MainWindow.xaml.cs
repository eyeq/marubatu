using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace marubatu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public const int TileWidth = 3;
        public const int TileHeight = 3;
        public const int Line = 3;

        private TileManager TileManager;
        
        public MainWindow()
        {
            InitializeComponent();
            
            Init();
        }

        public void Init()
        {
            TileManager = new TileManager(TileWidth, TileHeight, 2);
            
            Panel.ColumnDefinitions.Clear();
            for (var i = 0; i < TileWidth; i++)
            {
                Panel.ColumnDefinitions.Add(new ColumnDefinition());
            }
            
            Panel.RowDefinitions.Clear();
            for (var i = 0; i < TileHeight; i++)
            {
                Panel.RowDefinitions.Add(new RowDefinition());
            }
            
            Panel.Children.Clear();
            
            for (var i = 0; i < TileHeight; i++)
            {
                for (var j = 0; j < TileWidth; j++)
                {
                    var label = new Label
                    {
                        Content =  TileManager.GetTileState(j, i).GetLabel(),
                        VerticalAlignment = VerticalAlignment.Stretch,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        FontSize = 50,
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1),
                    };
                    label.SetValue(Grid.ColumnProperty, i);
                    label.SetValue(Grid.RowProperty, j);
                    
                    label.MouseDown += LabelClick;
                    Panel.Children.Add(label);
                }
            }
        }
        
        private void LabelClick(object sender, EventArgs e)
        {
            var label = sender as Label;
            
            var x = (int) label.GetValue(Grid.ColumnProperty); 
            var y = (int) label.GetValue(Grid.RowProperty); 
            
            if (TileManager.Put(x, y))
            {
                label.Content = TileManager.GetTileState(x, y).GetLabel();

                if (TileManager.GetMaxLine(TileManager.Current) >= Line)
                {
                    MessageBox.Show(TileManager.Current.GetLabel() + "の勝ち！");
                    Init();
                    return;
                }

                if (TileManager.GetEmpty() == 0)
                {
                    MessageBox.Show("引き分け");
                    Init();
                    return;
                }

                TileManager.Next();
            }
        }
    }
}