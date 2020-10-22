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
using System.Windows.Forms.DataVisualization.Charting;

namespace GraphicsEgor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PaymentSystemEntities db = new PaymentSystemEntities();
        
        public MainWindow()
        {
            InitializeComponent();
            
            chartPayment.ChartAreas.Add(new ChartArea("Main"));
            var currentSeries = new Series("Payments")
            {
                IsValueShownAsLabel = true
            };
            chartPayment.Series.Add(currentSeries);

            cmbChartTypes.ItemsSource = Enum.GetValues(typeof(SeriesChartType));
        }

        private void cmbChartTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbChartTypes.SelectedItem is SeriesChartType currentType)
            {
                Series currentSeries = chartPayment.Series.FirstOrDefault();
                currentSeries.ChartType = currentType;
                currentSeries.Points.Clear();

                var categoryList = db.Categories.ToList();
                foreach(var cat in categoryList)
                {
                    currentSeries.Points.AddXY(cat.Name, db.Payments.ToList().Where(a=>a.CategoryId == cat.Id).Sum(s=>s.Price));
                }
            }
        }
    }
}
