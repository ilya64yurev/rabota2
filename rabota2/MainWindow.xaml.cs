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

namespace rabota2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Entities entities = new Entities();
        public MainWindow()
        {
            InitializeComponent();
            foreach (var tovar in entities.Tovar)
                TovaryList.Items.Add(tovar);
            foreach (var vid in entities.VidTovara)
                comboBox_TypeOfTovar.Items.Add(vid);
        }

        private void TovaryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_tovar = TovaryList.SelectedItem as Tovar;
            if (selected_tovar != null)
            {
                textBoxName.Text = selected_tovar.nazvanie;
                comboBox_TypeOfTovar.SelectedItem = (from vid in entities.VidTovara where vid.Id == selected_tovar.vid select vid).Single<VidTovara>();
            }
            else
            {
                textBoxName.Text = "";
                comboBox_TypeOfTovar.SelectedIndex = -1;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var delete_tovar = TovaryList.SelectedItem as Tovar;
            if (delete_tovar != null)
            {
                var result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                entities.Tovar.Remove(delete_tovar);
                entities.SaveChanges();
                textBoxName.Clear();
                TovaryList.Items.Remove(delete_tovar);
                comboBox_TypeOfTovar.SelectedIndex = -1;
            }
            else
                MessageBox.Show("Нет удаляемых объектов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var tovar = TovaryList.SelectedItem as Tovar;
            if (textBoxName.Text == "" || comboBox_TypeOfTovar.SelectedIndex == -1)
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (tovar == null)
                {
                    tovar = new Tovar();
                    entities.Tovar.Add(tovar);
                    TovaryList.Items.Add(tovar);
                }
                tovar.nazvanie = textBoxName.Text;
                tovar.vid = (comboBox_TypeOfTovar.SelectedItem as VidTovara).Id;
                entities.SaveChanges();
                TovaryList.Items.Refresh();

            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            textBoxName.Text = "";
            TovaryList.SelectedIndex = -1;
            comboBox_TypeOfTovar.SelectedIndex = -1;
            textBoxName.Focus();
        }

        private void VidTovara_Click(object sender, RoutedEventArgs e)
        {
            var window_ = new Vidi();
            window_.ShowDialog();
        }


    }
}

