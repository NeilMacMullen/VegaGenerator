using NotNullStrings;

public static class VegaMaker
{
    public static string MakeHtml(string title, string spec)
    {
        return MakeHtml(title, spec, "carbong100");
    }

    public static string MakeHtml(string title, string spec, string theme)
    {
        var header = $$"""
                       <!doctype html>
                       <html>
                       <style>
                       .fullscreen {
                         width: 100vw;
                         height: 90vh;
                       }
                       </style>
                         <head>
                           <title>{{title}}</title>
                           <script src="https://cdn.jsdelivr.net/npm/vega@5"></script>
                           <script src="https://cdn.jsdelivr.net/npm/vega-lite@5"></script>
                           <script src="https://cdn.jsdelivr.net/npm/vega-embed@6"></script>
                         </head>
                         <body>
                           <div id="vis" class="fullscreen"></div>
                       
                           <script type="text/javascript">
                          var yourVlSpec =
                       """;
        var footer = $$"""
                            ;
                             vegaEmbed('#vis', yourVlSpec,{theme: '{{theme}}'});
                           </script>
                         </body>
                       </html>
                       """;
        return header + spec + footer;
    }

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
}