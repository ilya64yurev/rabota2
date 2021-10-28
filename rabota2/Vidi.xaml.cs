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
using System.Windows.Shapes;

namespace rabota2
{
    /// <summary>
    /// Логика взаимодействия для Vidi.xaml
    /// </summary>
    public partial class Vidi : Window
    {
        Entities entities = new Entities();
        public Vidi()
        {
            InitializeComponent();
            foreach (var vidi in entities.VidTovara)
                ListBoxAdd.Items.Add(vidi);
        }

        private void ListBoxAdd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBoxAdd.Text = ListBoxAdd.SelectedItem.ToString();
        }

        private void SaveAdd_Click(object sender, RoutedEventArgs e)
        {
            var vid_tov = new VidTovara();
            vid_tov.vid = TextBoxAdd.Text;
            if (vid_tov.vid == "")
                MessageBox.Show("Не указан вид товара!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                entities.VidTovara.Add(vid_tov);
                entities.SaveChanges();
                ListBoxAdd.Items.Add(vid_tov);
                TextBoxAdd.Text = "";
                ListBoxAdd.Items.Refresh();
                MessageBox.Show("Введен вид товара!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteAdd_Click(object sender, RoutedEventArgs e)
        {
         
            var delete_vid = ListBoxAdd.SelectedItem as VidTovara;
            if (delete_vid != null)
            {
                var result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                entities.VidTovara.Remove(delete_vid);
                entities.SaveChanges();
                TextBoxAdd.Clear();
                ListBoxAdd.Items.Remove(delete_vid);
            }
            else
                MessageBox.Show("Нет удаляемых объектов!!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
