using System;
using System.Collections.Generic;
using System.IO;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonDiffPatchDotNet.Demo
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			LeftJsonTextBox.TextChanged += JsonTextBoxOnTextChanged;
			RightJsonTextBox.TextChanged += JsonTextBoxOnTextChanged;
		}

		private void JsonTextBoxOnTextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				var left = LeftJsonTextBox.Text;
				var right = RightJsonTextBox.Text;

				if (string.IsNullOrWhiteSpace(left) || string.IsNullOrWhiteSpace(right))
				{
					return;
				}

				var diffHandler = new DiffHandler();
				var patch = diffHandler.Diff(left, right);
				DiffResultTextBox.Text = patch;
				var rightOfPatch = diffHandler.Patch(left, patch);

				RecoverResultTextBox.Text= rightOfPatch;

				if (right != rightOfPatch)
				{
					RecoverResultTextBox.Background = Brushes.LightCoral;
				}
				else
				{
					RecoverResultTextBox.Background = null;
				}

			}
			catch (Exception ex)
			{
				DiffResultTextBox.Text = $"发生异常\r\n{ex.Message}";
			}
		}

		private void SwapButton_OnClick(object sender, RoutedEventArgs e)
		{
			(LeftJsonTextBox.Text, RightJsonTextBox.Text) = (RightJsonTextBox.Text, LeftJsonTextBox.Text);
		}

		private void ClearButton_OnClick(object sender, RoutedEventArgs e)
		{
			LeftJsonTextBox.Clear();
			RightJsonTextBox.Clear();
		}

		private void LeftFormatButton_OnClick(object sender, RoutedEventArgs e)
		{
			LeftJsonTextBox.Text = Utils.FormatJsonString(LeftJsonTextBox.Text);
		}

		private void RightFormatButton_OnClick(object sender, RoutedEventArgs e)
		{
			RightJsonTextBox.Text = Utils.FormatJsonString(RightJsonTextBox.Text);
		}

		


	}
}
