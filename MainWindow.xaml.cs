using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hw5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        List<Drink> drinks = new List<Drink>();
        List<OrderItem> order = new List<OrderItem>();
        string takeout;
        public MainWindow()
        {
            InitializeComponent();

            AddNewDrink(drinks);

            DisplayDrinks(drinks);
        }

        private void DisplayDrinks(List<Drink> drinks)
        {
            foreach (Drink d in drinks)
            {
                StackPanel sp = new StackPanel();
                CheckBox cb = new CheckBox();
                Slider sl = new Slider();
                Label lb = new Label();
                sp.Orientation = Orientation.Horizontal;
                cb.Content = d.Name + d.Size + d.Price;
                cb.Margin = new Thickness(5);
                cb.Width = 150;
                cb.Height = 25;
                sl.Value = 0;
                sl.Width = 100;
                sl.Minimum = 0;
                sl.Maximum = 10;
                sl.TickPlacement = TickPlacement.TopLeft;
                sl.TickFrequency = 1;
                sl.IsSnapToTickEnabled = true;
                lb.Width = 50;
                Binding myBinding = new Binding("Value");
                myBinding.Source = sl;
                lb.SetBinding(ContentProperty, myBinding);
                sp.Children.Add(cb);
                sp.Children.Add(sl);
                sp.Children.Add(lb);
                stackpanel_DrinkMenu.Children.Add(sp);
            }
        }

        private void AddNewDrink(List<Drink> myDrink)
        {
            myDrink.Add(new Drink() { Name = "咖啡", Size = "大杯", Price = 60 });
            myDrink.Add(new Drink() { Name = "咖啡", Size = "中杯", Price = 50 });
            myDrink.Add(new Drink() { Name = "紅茶", Size = "大杯", Price = 30 });
            myDrink.Add(new Drink() { Name = "紅茶", Size = "中杯", Price = 20 });
            myDrink.Add(new Drink() { Name = "綠茶", Size = "大杯", Price = 25 });
            myDrink.Add(new Drink() { Name = "綠茶", Size = "中杯", Price = 20 });
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if(rb.IsChecked == true)
            {
                takeout = rb.Content.ToString();
            }
        }

        private void orderButton_click(object sender, RoutedEventArgs e)
        {
            displayTextBlock.Text = "";
            PlaceOrder(order);
            DisplayOrderDetail(order);
        }
        private void DisplayOrderDetail(List<OrderItem> myOrder)
        {
            int total = 0;
            int sum = 0;
            displayTextBlock.Text = $"您所訂購的飲品{takeout} 訂單如下: \n";
            int i = 1;
            foreach (OrderItem item in myOrder)
            {
                total += item.SubTotal;
                Drink drinkItem = drinks[item.Index];
                sum = sum + item.SubTotal;
                displayTextBlock.Text += $"訂購品項{i} : { drinkItem.Name} { drinkItem.Size} , 單價{ drinkItem.Price}元 X { item.Quantity} , 小計{ item.SubTotal}元 \n";
                i++;
            }
            displayTextBlock.Text += $"總共: {sum}元 \n";
            int sell = 0;
            if (sum >= 500)
            {
                sell = (int)(sum * 0.8);
                displayTextBlock.Text += $"打折後金額: {sell}元\n";
            }
            else if (sum >= 300)
            {
                sell = (int)(sum * 0.85);
                displayTextBlock.Text += $"打折後金額: {sell}元\n";
            }
            else if (sum >= 200)
            {
                sell = (int)(sum * 0.9);
                displayTextBlock.Text += $"打折後金額: {sell}元\n";
            }
        }
        private void PlaceOrder(List<OrderItem> myOrder)
        {
            myOrder.Clear();
            for (int i = 0; i < stackpanel_DrinkMenu.Children.Count; i++)
            {
                StackPanel sp = stackpanel_DrinkMenu.Children[i] as StackPanel;
                CheckBox cb = sp.Children[0] as CheckBox;
                Slider sl = sp.Children[1] as Slider;
                int quantity = Convert.ToInt32(sl.Value);
                if (cb.IsChecked == true && quantity != 0)
                {
                    int price = drinks[i].Price;
                    int subtotal = price * quantity;
                    myOrder.Add(new OrderItem(){ Index = i, Quantity = quantity, SubTotal = subtotal});
                }
            }
        }

    }
}
