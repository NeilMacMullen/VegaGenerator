using System.Security.Policy;
using System.Windows;
using System.Linq;

namespace Demo;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        foreach(var kv in DemoCharts.Demos)
        {
           ChartOption.Items.Add(kv.Key);
        }
        ChartOption.SelectedIndex = 0;

        foreach(var theme in VegaMaker.KnownThemes)
        {
            Theme.Items.Add(theme);
        }
        Theme.SelectedIndex = 0;

    }


    private async void OnRender(object sender, RoutedEventArgs e)
    {
        await webview.EnsureCoreWebView2Async();
       var wanted = ChartOption.SelectedItem.ToString();

        var func = DemoCharts.Demos[wanted];
        var spec = func()
            .SetTitle("Demo Chart")
           
            .FillContainer(); ;
        

        //generate the html and display it
        var html = VegaMaker.MakeHtml("demo", spec.Serialize(),Theme.SelectedItem.ToString());
        try
        {
            webview.NavigateToString(html);
        }
        catch
        {
        }
    }
}