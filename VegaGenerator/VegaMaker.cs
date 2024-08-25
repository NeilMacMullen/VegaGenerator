using NotNullStrings;

public static class VegaMaker
{
    public const string DefaultTheme = "carbong100";

    public static readonly IReadOnlyCollection<string> KnownThemes =
        @"
    excel
    ggplot2
    quartz
    vox
    fivethirtyeight
    dark
    latimes
    urbaninstitute
    googlecharts
    powerbi
    carbonwhite
    carbong10
    carbong90
    carbong100"
            .Tokenize("\r\n ");

    public static string MakeHtml(string pageTitle, string spec)
    {
        return MakeHtml(pageTitle, spec, DefaultTheme);
    }

    public static VegaDivStyle FullScreenStyle =>
     new("fullscreen","""
                            width: 99vw;
                            height: 90vh;
                    """);
    

    public static string WrapStyle(VegaDivStyle style)
    {
        return $$"""
               <style>
                      .{{style.Name}}
                      {
                        {{style.Body}}
                      }
                       </style>
               """;
    }

    public static string CreateHeader(string pageTitle, IEnumerable<VegaDivStyle> styles)
    {
        var wrappedStyles = styles.Select(WrapStyle).ToArray();
        var header = $$"""
                       <!doctype html>
                       <html>
                       {{wrappedStyles.JoinAsLines()}}
                       <head>
                         <title>{{pageTitle}}</title>
                         <script src="https://cdn.jsdelivr.net/npm/vega@5"></script>
                         <script src="https://cdn.jsdelivr.net/npm/vega-lite@5"></script>
                         <script src="https://cdn.jsdelivr.net/npm/vega-embed@6"></script>
                       </head>
                       """;
        return header;
    }

    public static string AddChart(string divName, string style, string spec, string theme,string actions)
    {
        var specName = $"{divName}_spec";
        var header = $$"""
                           <div id="{{divName}}" class="{{style}}">
                           <title>{{divName}}</title>
                           </div>
                           <script type="text/javascript">
                          var {{specName}} =
                       """;
        var footer = $$"""
                            ;
                             vegaEmbed('#{{divName}}', {{specName}},{theme: '{{theme}}',actions:{{actions}}});
                           </script>
                       """;
        return header + spec + footer;
    }

    public static string MakeHtml(string pageTitle, string spec, string theme)
    {
        return $$"""
                 {{CreateHeader(pageTitle, [FullScreenStyle])}}
                   <body>
                   {{AddChart("vis", FullScreenStyle.Name, spec, theme,"true")}}
                   </body>
                 </html>
                 """;
    }
}
public record VegaDivStyle(string Name,string Body);