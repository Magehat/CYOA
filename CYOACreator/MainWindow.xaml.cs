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

namespace CYOACreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Page m_selectedPage;
        StoryManager m_manager;

        public MainWindow()
        {
            m_manager = new StoryManager();
            InitializeComponent();

            PageGrid.ItemsSource = m_manager.GetPages();

            UpdateCurrentPage(m_manager.GetPage(1));
        }

        private void PageGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                UpdateCurrentPage(e.AddedItems[0] as Page);
            }
        }

        private void UpdateCurrentPage(Page newPage)
        {
            m_selectedPage = newPage;

            CurrentPageNumTextBlock.Text = m_selectedPage.PageNum.ToString();
            CurrentPageTextBox.Text = m_selectedPage.Text;
            CurrentPageTransitionsGrid.ItemsSource = m_selectedPage.Transitions.ToList();
        }

        private void UpdatePageList()
        {
            PageGrid.ItemsSource = m_manager.GetPages();
            PageGrid.Items.Refresh();
        }

        private void CreateNewButton_Click(object sender, RoutedEventArgs e)
        {
            long newId = m_manager.CreatePage();
            UpdateCurrentPage(m_manager.GetPage(newId));
            UpdatePageList();
        }

        private void SavePageButton_Click(object sender, RoutedEventArgs e)
        {
            m_manager.UpdatePage(m_selectedPage.PageNum, CurrentPageTextBox.Text, m_selectedPage.Transitions);
            UpdatePageList();
        }

        private void NewTransitionButton_Click(object sender, RoutedEventArgs e)
        {
            Transition[] trans = { new Transition("", 0) };
            m_selectedPage.Transitions = m_selectedPage.Transitions.Concat(trans);
            CurrentPageTransitionsGrid.ItemsSource = m_selectedPage.Transitions.ToList();
        }

        private void SaveStoryButton_Click(object sender, RoutedEventArgs e)
        {
            StoryWriter.Write(m_manager.GetPages());
        }
    }
}
