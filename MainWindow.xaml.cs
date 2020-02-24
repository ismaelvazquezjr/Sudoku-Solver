using Patagames.Ocr;
using Patagames.Ocr.Enums;
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
using System.Drawing;

namespace SudokuSolver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int TOTAL_SUDOKU_VALUES = 81;
        public static int BOARD_DIMENSION = 9;
        public TextBox[,] BoardArray = new TextBox[BOARD_DIMENSION,BOARD_DIMENSION];
        public static int INVALID_CELL = -1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SolveButton_Click(object sender, RoutedEventArgs e)
        {
            ConvertGridToMultiArray();
            int row = INVALID_CELL;
            int col = INVALID_CELL;
            GetIndexOfUnassignedCell(ref row, ref col);
            if (solve(row, col) == false)
            {
                MessageBox.Show("Unsolvable");
            }
        }

        private void ConvertGridToMultiArray()
        {
            // https://stackoverflow.com/questions/5134555/how-to-convert-a-1d-array-to-2d-array
            // Implementation details taken from this answer
            for (int i = 0; i < BOARD_DIMENSION; i++)
            {
                for (int j = 0; j < BOARD_DIMENSION; j++)
                {
                    TextBox temp = (TextBox)BoardGrid.Children[i * BOARD_DIMENSION + j];
                    BoardArray[i, j] = temp;
                }
            }
        }

        private bool solve(int row, int col)
        {
            if (row == INVALID_CELL) return true;
            for (int i = 1; i <= 9; i++)
            {
                if (IsValidCellPlacement(row, col, i))
                {
                    BoardArray[row, col].Foreground = new SolidColorBrush(Colors.Red);
                    BoardArray[row, col].Text = i.ToString();
                    int r = INVALID_CELL, c = INVALID_CELL;
                    GetIndexOfUnassignedCell(ref r, ref c);
                    if (solve(r, c)) return true;
                    else BoardArray[row, col].Text = "";
                } 
            }
            return false;
        }

        // https://www.geeksforgeeks.org/sudoku-backtracking-7/
        private bool IsValidCellPlacement(int row, int col, int num)
        {
            // row has the unique (row-clash) 
            for (int d = 0; d < BoardArray.GetLength(0); d++)
            {
                // if the number we are trying to  
                // place is already present in  
                // that row, return false; 
                int temp1 = INVALID_CELL;
                Int32.TryParse(BoardArray[row, d].Text, out temp1);
                if (temp1 == num)
                {
                    return false;
                }
            }

            // column has the unique numbers (column-clash) 
            for (int r = 0; r < BoardArray.GetLength(0); r++)
            {
                // if the number we are trying to 
                // place is already present in 
                // that column, return false; 
                int temp2 = INVALID_CELL;
                Int32.TryParse(BoardArray[r, col].Text, out temp2);
                if (temp2 == num)
                {
                    return false;
                }
            }

            // corresponding square has 
            // unique number (box-clash) 
            int sqrt = (int)Math.Sqrt(BoardArray.GetLength(0));
            int boxRowStart = row - row % sqrt;
            int boxColStart = col - col % sqrt;

            for (int r = boxRowStart;
                    r < boxRowStart + sqrt; r++)
            {
                for (int d = boxColStart;
                        d < boxColStart + sqrt; d++)
                {
                    int temp3 = INVALID_CELL;
                    Int32.TryParse(BoardArray[r, d].Text, out temp3);
                    if (temp3 == num)
                    {
                        return false;
                    }
                }
            }

            // if there is no clash, it's safe 
            return true;
        }

        private void GetIndexOfUnassignedCell(ref int row, ref int col)
        {
            for(int i = 0; i < BOARD_DIMENSION; i++)
            {
                for (int j = 0; j < BOARD_DIMENSION; j++)
                {
                    if (BoardArray[i, j].Text == "")
                    {
                        row = i;
                        col = j;
                        return;
                    }
                }
            }
            row = INVALID_CELL;
            col = INVALID_CELL;
        }

        private void ScanImageButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog sudokuBoardImage = new Microsoft.Win32.OpenFileDialog();
            sudokuBoardImage.Filter = "Image files |*.png;*.jpeg;*jpg";
            if (sudokuBoardImage.ShowDialog() == true)
            {
                var api = OcrApi.Create();
                api.Init(Languages.English);
                try
                {
                    string sudokuValuesFromFile = api.GetTextFromImage(sudokuBoardImage.FileName);
                    sudokuValuesFromFile = sudokuValuesFromFile.Replace("\n", "");
                    if (sudokuValuesFromFile.Length != TOTAL_SUDOKU_VALUES)
                    {
                        MessageBox.Show($"{sudokuValuesFromFile.Length} Incompatible board - Empty boxes should be filled in with X's");
                        MessageBox.Show(sudokuValuesFromFile);
                        return;
                    }

                    
                    for (int i = 0; i < TOTAL_SUDOKU_VALUES; i++)
                    {
                        TextBox temp = (TextBox)BoardGrid.Children[i];
                        int newValue;
                        if (!Int32.TryParse(sudokuValuesFromFile[i].ToString(), out newValue)) continue;
                        temp.Text = sudokuValuesFromFile[i] == 'X' ? "" : newValue.ToString();
                    }

                } catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
            }
        }

        private void ClearBoardButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < TOTAL_SUDOKU_VALUES; i++)
            {
                TextBox temp = (TextBox)BoardGrid.Children[i];
                temp.Text = "";
                temp.Foreground = new SolidColorBrush(Colors.Black);
            }
        }
    }
}
