using System.Collections.Specialized;

namespace Demo;

public static class DemoCharts
{

    private static OrderedDictionary[] Create(int n, Func<int, object> xFunc, Func<int, object> yFunc, string series)
    {
        return Enumerable.Range(0, n)
            .Select(x =>
            {
                var o = new OrderedDictionary();
                o["x"] = xFunc(x);
                o["y"] = yFunc(x);
                o["series"] = series;
                return o;
            })
            .ToArray();
    }
    static VegaChart SinCosWithTimeAxis()
    {
        var now = DateTime.Now;
        OrderedDictionary[] data =
        [
            ..Create(100, i => now + TimeSpan.FromHours(i), i => Math.Sin(Math.PI * 2 * i / 100), "sin"),
            ..Create(100, i => now +TimeSpan.FromHours(i), i => Math.Cos(Math.PI * 2 * i / 100), "cos")
        ];
        var spec = VegaChart.CreateVegaChart(
                VegaMark.Line,
                new ColumnDescription("x", "x", VegaAxisType.Temporal),
                new ColumnDescription("y", "y", VegaAxisType.Quantitative),
                new ColumnDescription("series", "series", VegaAxisType.Nominal)
            )
            .InjectData(data)
            .UseCursorTooltip();
           
        return spec;
    }

    static VegaChart SinCosWithTextAxis()
    {
        var now = DateTime.Now;
        var alphabet = "EFGHABCDMNOPQIJKLRWXYZSTUV".ToCharArray();
        OrderedDictionary[] data =
        [
            ..Create(alphabet.Length, i => $"{alphabet[i]}", i => Math.Sin(Math.PI * 2 * i / alphabet.Length), "sin"),
        ];
        var spec = VegaChart.CreateVegaChart(
                VegaMark.Line,
                new ColumnDescription("x", "x", VegaAxisType.Nominal),
                new ColumnDescription("y", "y", VegaAxisType.Quantitative),
                new ColumnDescription("series", "series", VegaAxisType.Nominal)
            )
            .InjectData(data)
            .UseCursorTooltip();
        return spec;
    }

    static VegaChart HiLoScatterPlot()
    {
     
        var r = new Random();
        OrderedDictionary[] data =
        [
            ..Create(100, i => r.Next(100), i =>r.Next(60)+40, "higher"),
            ..Create(100, i => r.Next(100), i =>r.Next(60), "lower"),
        ];
        var spec = VegaChart.CreateVegaChart(
                VegaMark.Point,
                new ColumnDescription("x", "x", VegaAxisType.Quantitative),
                new ColumnDescription("y", "y", VegaAxisType.Quantitative),
                new ColumnDescription("series", "series", VegaAxisType.Nominal)
            )
            .InjectData(data);
        return spec;
    }



    public static Dictionary<string, Func<VegaChart>> Demos = new Dictionary<string, Func<VegaChart>>()
    {
        [nameof(SinCosWithTimeAxis)] = SinCosWithTimeAxis,
        [nameof(SinCosWithTextAxis)] = SinCosWithTextAxis,
        [nameof(HiLoScatterPlot)] = HiLoScatterPlot,
    };


}