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
				DiffResultTextBox.Text = Diff();
			}
			catch (Exception ex)
			{
				DiffResultTextBox.Text =
					$"""
					发生异常
					{ex.Message}
					""";
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
			LeftJsonTextBox.Text = FormatJsonString(LeftJsonTextBox.Text);
		}

		private void RightFormatButton_OnClick(object sender, RoutedEventArgs e)
		{
			RightJsonTextBox.Text = FormatJsonString(RightJsonTextBox.Text);
		}

		private string Diff()
		{
			var left = LeftJsonTextBox.Text;
			var right = RightJsonTextBox.Text;

			var jdp = new JsonDiffPatch(new Options()
			{
				ArrayDiff = ArrayDiffMode.Efficient,
				ObjectHash = (obj, index) =>
				{
					var objString = obj.ToString(Formatting.Indented); // for debug view
					return $"$$index{index}";

					////var id = obj["Id"]?.Value<string>();
					////if (!string.IsNullOrWhiteSpace(id))
					////{
					////    return id;
					////}
					////else
					////{
					////    return $"$$index{index}";
					////}
				}
			});

			var leftToken = JToken.Parse(left);
			var rightToken = JToken.Parse(right);

			JToken? patch = jdp.Diff(leftToken, rightToken);

			if (patch == null)
			{
				return "";
			}

			return patch.ToString(Formatting.Indented);
		}

		/// <summary>
		/// 格式化 json 字符串
		/// </summary>
		public static string FormatJsonString(string jsonString)
		{
			if (jsonString == null || string.IsNullOrWhiteSpace(jsonString))
			{
				return "";
			}

			var result = jsonString;
			try
			{
				JsonSerializer jsonSerializer = new JsonSerializer();
				using TextReader textReader = new StringReader(jsonString);
				using JsonTextReader jsonTextReader = new JsonTextReader(textReader);
				object? obj = jsonSerializer.Deserialize(jsonTextReader);
				if (obj != null)
				{
					using StringWriter textWriter = new StringWriter();
					using JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
					{
						Formatting = Formatting.Indented,
						Indentation = 2,
						IndentChar = ' '
					};
					jsonSerializer.Serialize(jsonWriter, obj);
					result = textWriter.ToString();
				}
				return result;
			}
			catch (Exception)
			{
				return jsonString;
			}

		}

	}
}
