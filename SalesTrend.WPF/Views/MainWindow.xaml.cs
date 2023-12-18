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

namespace SalesTrend.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("О каждом предприятии или организации, информация о которой \r\nфиксируется в БД, необходимо хранить следующее: название предприятия \r\n(организации), юридический адрес, контактные телефоны, электронный адрес,\r\nадрес сайта предприятия, ФИО контактного лица. Всё это указывается в \r\nрекламном листе (прайс-листе), выпускаемом предприятием товара. В прайс листе указывается дата выпуска листа, реквизиты предприятия, выпускающего \r\nтовар и список выпускаемых товаров. Каждый товар характеризуется артикулом, \r\nназванием, ценой за единицу (на дату, указанную в листе), представляемым \r\nколичеством. В каждом прайс-листе, как правило, содержится много позиций с \r\nописанием разных товаров.\r\nВ БД также необходимо хранить информацию о потенциальных клиентах \r\nзаказчиках товара. Для каждого клиента фиксируются:\r\n- для юридических лиц – код, название, краткое название, ИНН, адрес, \r\nконтактные телефоны, электронный адрес, ФИО контактных лиц;\r\n- для физического лица – ФИО, адрес, паспортные данные (серия, номер, \r\nдата выдачи, кем выдан), ИНН.\r\nТакже необходимо хранить информацию о заказах клиентов:\r\n- номер, дата заказа;\r\n- позиции заказа, в каждой из которых указывается: номер, название товара, \r\nколичество требуемого товара.\r\nНеобходимо осуществлять следующую обработку данных:\r\n- на заданную дату список клиентов, заказавших товар заданного \r\nнаименования, требуемое количество товара;\r\n- на заданную дату список товаров заданной категории с указанием цены;\r\n- на заданный период динамика изменения стоимости заданного товара –\r\nстоимость по декадам", "Справка");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var window = new ClientList();
            window.Show();
            //var companiesWithVacanciesView = new CompaniesWithVacanciesView();
            //companiesWithVacanciesView.Show();

        }
        
        
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            var win = new ProductList();
            win.Show();

        }

        private void MenuItem_Click_CompaniesWithNoEducationRequirementView(object sender, RoutedEventArgs e)
        {
            //var CompaniesWithNoEducationRequirementView = new CompaniesWithNoEducationRequirementView();
            //CompaniesWithNoEducationRequirementView.Show();
        }

        private void MenuItem_Click_ShowKmeansClick(object sender, RoutedEventArgs e)
        {
            //var kmeans = new ClusterAnalysisView();
            //kmeans.Show();
        }
        private void MenuItem_Click_ShowKmeansTableClick(object sender, RoutedEventArgs e)
        {
            //var kmeans = new ClusterAnalysisTableView();
            //kmeans.Show();
        }

    }
}
