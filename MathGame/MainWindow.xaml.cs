using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MathGame {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private enum Operators {
            Add,
            Subtract,
            Multiply,
            Divide
        }

        private int m_TaskResult = 0;
        private int m_TaskResultOk = 0;
        private int m_TaskResultFailed = 0;

        private const int MAX_OPERATOR_VAL = 4;
        private const int MAX_NUMBER_VAL = 10;

        private Random m_RandomOperatorGenerator = new Random();
        private Random m_RandomNumberGenerator = new Random();

        public MainWindow() {
            InitializeComponent();
        }

        private void textBoxInput_KeyDown(object sender, KeyEventArgs e) {
            if (Key.Enter == e.Key) {
                evaluateInput();
                generateTask();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            generateTask();
        }

        private void generateTask() {
            updateStatistic();
            int Operator = m_RandomOperatorGenerator.Next(MAX_OPERATOR_VAL);
            int x;
            int y;
            switch (Operator) {
                case (int)Operators.Add:
                    x = m_RandomNumberGenerator.Next(1, MAX_NUMBER_VAL);
                    y = m_RandomNumberGenerator.Next(1, MAX_NUMBER_VAL);
                    m_TaskResult = x + y;
                    this.labelTask.Content = String.Format("{0} + {1} = ", x, y);
                    break;
                case (int)Operators.Subtract:
                    x = m_RandomNumberGenerator.Next(1, MAX_NUMBER_VAL);
                    y = m_RandomNumberGenerator.Next(1, MAX_NUMBER_VAL);
                    if (y >= x) {
                        this.labelTask.Content = String.Format("{0} - {1} = ", y, x);
                        m_TaskResult = y - x;
                    }
                    else {
                        this.labelTask.Content = String.Format("{0} - {1} = ", x, y);
                        m_TaskResult = x - y;
                    }
                    break;
                case (int)Operators.Multiply:
                    x = m_RandomNumberGenerator.Next(1, MAX_NUMBER_VAL);
                    y = m_RandomNumberGenerator.Next(1, MAX_NUMBER_VAL);
                    m_TaskResult = x * y;
                    this.labelTask.Content = String.Format("{0} * {1} = ", x, y);
                    break;
                case (int)Operators.Divide:
                    x = m_RandomNumberGenerator.Next(1, MAX_NUMBER_VAL);
                    y = m_RandomNumberGenerator.Next(1, MAX_NUMBER_VAL);
                    m_TaskResult = x * y;
                    this.labelTask.Content = String.Format("{0} / {1} = ", m_TaskResult, x);
                    m_TaskResult = y;
                    break;
                default:
                    this.labelTask.Content = String.Format("unbekannter Operator: {0}", Operator);
                    break;
            }
            textBoxInput.Clear();
            textBoxInput.Focus();
        }

        private void evaluateInput() {
            int value;
            if (Int32.TryParse(this.textBoxInput.Text, out value)) {
                if (m_TaskResult == value) {
                    m_TaskResultOk++;
                }
                else {
                    m_TaskResultFailed++;
                }

                updateStatistic();
            }
        }

        private void updateStatistic() {
            LabelStatistics.Content = String.Format("Richtig: {0}, Falsch {1}", m_TaskResultOk, m_TaskResultFailed);
        }

    }
}
