using System.Text;
using NotNullStrings;

public class VegaComposer
{
    private readonly StringBuilder _builder = new();
    private readonly Dictionary<string,VegaDivStyle> _styles = new();
    private int divId;
    private VegaDivStyle _defaultStyle = VegaMaker.FullScreenStyle;
    public VegaComposer(string pageTitle, string theme)
    {
        PageTitle = pageTitle;
        Theme = theme.OrWhenBlank(VegaMaker.DefaultTheme);
        AddStyle(VegaMaker.FullScreenStyle);
    }
    public bool ShowActionsMenu { get;  set; }
    public string PageTitle { get; }
    public string Theme { get; set; }

    public void AddStyle(VegaDivStyle style)
    {
        _styles[style.Name] =style;
        _defaultStyle = style;
    }

    public void AddChart(VegaChart chart)
    {
        AddChart(chart,_defaultStyle);
    }
    public void AddChart(VegaChart chart, VegaDivStyle style)
    {
        var actions = ShowActionsMenu? "true" : "false";
        AddStyle(style);
        divId++;
        _builder.AppendLine(VegaMaker.AddChart($"chart_{divId}", style.Name, chart.Serialize(), Theme,actions));
    }
    public void AddRawHtml(string html)
    {
       
        _builder.AppendLine(html);
    }


    public string Render()
    {
        var styleBlock = _styles.Values
            ;
        return $$"""
                 {{VegaMaker.CreateHeader(PageTitle, styleBlock)}}
                   <body>
                   {{_builder}}
                   </body>
                 </html>
                 """;
    }
}