using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MathModelLr6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void PrintMatrix(List<List<int>> matrix, DataGrid dataGrid)
        {
            dataGrid.Items.Clear();
            dataGrid.Columns.Clear();
            dataGrid.AutoGenerateColumns = false;

            // Create columns for each element in the matrix
            for (int i = 0; i < matrix[0].Count; i++)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = $"B{i + 1}";
                column.Binding = new Binding($"[{i}]");
                dataGrid.Columns.Add(column);
            }

            // Add rows to the data grid
            foreach (List<int> row in matrix)
            {
                dataGrid.Items.Add(row);
            }
        }

        private void generateMatrixButton_Click(object sender, RoutedEventArgs e)
        {
            int n;
            int m;

            if (int.TryParse(nTextBox.Text, out n) && int.TryParse(mTextBox.Text, out m))
            {
                if (n < 2 || n > 12 || m < 2 || m > 12)
                {
                    return;
                }

                List<List<int>> matrix = new List<List<int>>();
                Random random = new Random();

                for (int i = 0; i < n; i++)
                {
                    List<int> row = new List<int>();
                    for (int j = 0; j < m; j++)
                    {
                        row.Add(random.Next(2, 21));
                    }
                    matrix.Add(row);
                }

                PrintMatrix(matrix, matrixDataGrid);
            }
        }
    }
}