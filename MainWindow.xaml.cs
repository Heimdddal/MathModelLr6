using System.Data;
using System.Net;
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

            int[,] example2 = new int[,] {
                { 5, 4, 5, 4, 8, 4 },
                { 11, 2, 3, 2, 6, 4 },
                { 7, 5, 9, 10, 9, 4 },
                { 4, 10, 8, 2, 10, 2 },
                { 9, 6, 3, 12, 2, 4}
            };

            List<int> row1 = new List<int>() { 5, 4, 5, 4, 8, 4 };
            List<int> row2 = new List<int>() { 11, 2, 3, 2, 6, 4 };
            List<int> row3 = new List<int>() { 7, 5, 9, 10, 9, 4 };
            List<int> row4 = new List<int>() { 4, 10, 8, 2, 10, 2 };
            List<int> row5 = new List<int>() { 9, 6, 3, 12, 2, 4 };

            List<List<int>> matrix2 = new List<List<int>>() { row1, row2, row3, row4, row5 };

            ShowTheResult(matrix);
            ResolveMatrix(matrix);
        }

        private void ShowTheResult(List<List<int>> matrix)
        {
            Result.Text = $"Верхняя цена игры: {Maximin(FindMaxInCols(matrix))}\nНижняя цена игры: {Minimax(FindMinInRows(matrix))}\n";
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

        //Найти верхнюю цену игры
        private List<int> FindMinInRows(List<List<int>> matrix)
        {
            int rowCount = matrix.Count;

            List<int> max = new List<int>();

            for (int i = 0; i < rowCount; i++)
            {
                max.Add(matrix[i].Min());
            }
            return max;
        }

        private int Minimax(List<int> max)
        {
            return max.Max();
        }

        //Найти нижнюю цену игры
        private List<int> FindMaxInCols(List<List<int>> matrix)
        {
            List<int> min = new List<int>();

            for(int i = 0; i < matrix[0].Count; i++)
            {
                List<int> column = new List<int>();

                for(int j = 0; j < matrix.Count; j++)
                {
                    column.Add(matrix[j][i]);
                }

                min.Add(column.Max());
            }

            return min;
        }

        private int Maximin(List<int> min)
        {
            return min.Min();
        }

        private void ResolveMatrix(List<List<int>> matrix)
        {
            var maxmin = Maximin(FindMaxInCols(matrix));
            var minmax = Minimax(FindMinInRows(matrix));
            var minInRows = FindMinInRows(matrix);
            var maxInCols = FindMaxInCols(matrix);

            if (maxmin != minmax)
            {
                ResolveMatrixInMixedStrategy(matrix);
            }
            else
            {
                Result.Text += "Седловые точки:\n";
                for (int i = 0; i < minInRows.Count; i++)
                {
                    for (int j = 0; j < maxInCols.Count; j++)
                    {
                        if (maxInCols[j] == maxmin && minInRows[i] == maxmin)
                        {
                            Result.Text += $"({i + 1};{j + 1})\n";
                        }
                    }
                }
            }
        }

        private void ResolveMatrixInMixedStrategy(List<List<int>> matrix)
        {
            if (matrix.Count == 2 && matrix[0].Count == 2)
            {

                double x1, x2, y1, y2, V;

                x1 = (matrix[1][1] - matrix[1][0])
                        / (double)
                     (matrix[0][0] + matrix[1][1] - matrix[1][0] - matrix[0][1]);

                x2 = (matrix[0][0] - matrix[0][1])
                        / (double)
                     (matrix[0][0] + matrix[1][1] - matrix[1][0] - matrix[0][1]);


                y1 = (matrix[1][1] - matrix[0][1])
                        / (double)
                     (matrix[0][0] + matrix[1][1] - matrix[1][0] - matrix[0][1]);

                y2 = (matrix[0][0] - matrix[1][0])
                        / (double)
                     (matrix[0][0] + matrix[1][1] - matrix[1][0] - matrix[0][1]);


                V = (matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0])
                        / (double)
                     (matrix[0][0] + matrix[1][1] - matrix[1][0] - matrix[0][1]);


                Result.Text = $"x1 = {x1,2:F3}\nx2 = {x2,2:F3}\ny1 = {y1,2:F3}\ny2 = {y2,2:F3}\nV (Цена игры) = {V,2:F3}";
            }
        }
    }
}

