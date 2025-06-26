using System.Diagnostics;
using System.Runtime.CompilerServices;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.ClientUnitTests.AnalyticsTests;
using ToSic.Cre8magic.Settings.Internal;
using Xunit.Abstractions;
using static Xunit.Assert;

namespace ToSic.Cre8magic.ClientUnitTests.GenericStackTest;

/// <summary>
/// 2025-03-26 2dm
/// This was experimental to decide if we should improve our settings classes to be more generic.
/// ATM using the best kind of dictionary seems to cost 8-10x more, so we won't pursue it ATM.
///
/// To pick up work again, just replace the [Fact] with [Fact] and run the tests.
/// </summary>
/// <param name="output"></param>
[Collection(nameof(NoParallel))]
public class PerformanceTest(ITestOutputHelper output)
{
    /// <summary>
    /// For real analysis, increase to 10_000_000
    /// </summary>
    public int Iterations = 10_000;

    private void RunWithStopwatch(Action action, [CallerMemberName] string testName = default!)
    {
        var time = Stopwatch.StartNew();
        action();
        time.Stop();
        output.WriteLine($"{testName}: {time.ElapsedMilliseconds}ms");
    }

    [Fact]
    public void AccessEmptyV0Old() => RunWithStopwatch(() =>
    {
        var x = new MagicAnalyticsSettings();
        for (var i = 0; i < Iterations; i++)
        {
            var y = x.GtmId;
        }
    });


    [Fact]
    public void AccessEmptyV1Meta() => RunWithStopwatch(() =>
    {
        var x = new V1AnalyticsSettingsMeta();
        for (var i = 0; i < Iterations; i++)
        {
            var y = x.GtmId;
        }
    });

    [Fact]
    public void AccessEmptyV2Object() => RunWithStopwatch(() =>
    {
        var x = new V2AnalyticsSettingsObject();
        for (var i = 0; i < Iterations; i++)
        {
            var y = x.GtmId;
        }
    });

    [Fact]
    public void AccessEmptyV3Object() => RunWithStopwatch(() =>
    {
        var x = new V3AnalyticsSettingsObjectNonGeneric();
        for (var i = 0; i < Iterations; i++)
        {
            var y = x.GtmId;
        }
    });

    [Fact]
    public void AccessEmptyV4Object() => RunWithStopwatch(() =>
    {
        var x = new V4AnalyticsSettingsObjectSafer();
        for (var i = 0; i < Iterations; i++)
        {
            var y = x.GtmId;
        }
    });


    [Fact]
    public void AccessNonEmptyV0Old() => RunWithStopwatch(() =>
    {
        var x = new MagicAnalyticsSettings { GtmId = "abc" };
        for (var i = 0; i < Iterations; i++)
        {
            var y = x.GtmId;
        }
    });


    [Fact]
    public void AccessNonEmptyV1Meta() => RunWithStopwatch(() =>
    {
        var x = new V1AnalyticsSettingsMeta { GtmId = "abc" };
        for (var i = 0; i < Iterations; i++)
        {
            var y = x.GtmId;
        }
    });

    [Fact]
    public void AccessNonEmptyV2Object() => RunWithStopwatch(() =>
    {
        var x = new V2AnalyticsSettingsObject { GtmId = "abc" };
        for (var i = 0; i < Iterations; i++)
        {
            var y = x.GtmId;
        }
    });

    [Fact]
    public void AccessNonEmptyV3Object() => RunWithStopwatch(() =>
    {
        var x = new V3AnalyticsSettingsObjectNonGeneric { GtmId = "abc" };
        for (var i = 0; i < Iterations; i++)
        {
            var y = x.GtmId;
        }
    });

    [Fact]
    public void AccessNonEmptyV4Object() => RunWithStopwatch(() =>
    {
        var x = new V4AnalyticsSettingsObjectSafer { GtmId = "abc" };
        for (var i = 0; i < Iterations; i++)
        {
            var y = x.GtmId;
        }
    });




    [Fact]
    public void ConstructEmptyV0Old() => RunWithStopwatch(() =>
    {
        for (var i = 0; i < Iterations; i++)
        {
            var x = new MagicAnalyticsSettings();
        }
    });


    [Fact]
    public void ConstructEmptyV1Meta() => RunWithStopwatch(() =>
    {
        for (var i = 0; i < Iterations; i++)
        {
            var x = new V1AnalyticsSettingsMeta();
        }
    });

    [Fact]
    public void ConstructEmptyV2Object() => RunWithStopwatch(() =>
    {
        for (var i = 0; i < Iterations; i++)
        {
            var x = new V2AnalyticsSettingsObject();
        }
    });

    [Fact]
    public void ConstructEmptyV3Object() => RunWithStopwatch(() =>
    {
        for (var i = 0; i < Iterations; i++)
        {
            var x = new V3AnalyticsSettingsObjectNonGeneric();
        }
    });




    [Fact]
    public void ConstructWithValueV0Old() => RunWithStopwatch(() =>
    {
        for (var i = 0; i < Iterations; i++)
        {
            var x = new MagicAnalyticsSettings
            {
                GtmId = "123",
                PageViewTrack = true,
                PageViewTrackFirst = false,
            };
        }
    });

    [Fact]
    public void ConstructWithValueV1Meta() => RunWithStopwatch(() =>
    {
        for (var i = 0; i < Iterations; i++)
        {
            var x = new V1AnalyticsSettingsMeta
            {
                GtmId = "123",
                PageViewTrack = true,
                PageViewTrackFirst = false,
            };
        }
    });

    [Fact]
    public void ConstructWithValueV2Object() => RunWithStopwatch(() =>
    {
        for (var i = 0; i < Iterations; i++)
        {
            var x = new V2AnalyticsSettingsObject
            {
                GtmId = "123",
                PageViewTrack = true,
                PageViewTrackFirst = false,
            };
        }
    });

    [Fact]
    public void ConstructWithValueV3NonGeneric() => RunWithStopwatch(() =>
    {
        for (var i = 0; i < Iterations; i++)
        {
            var x = new V3AnalyticsSettingsObjectNonGeneric
            {
                GtmId = "123",
                PageViewTrack = true,
                PageViewTrackFirst = false,
            };
        }
    });


    [Fact]
    public void MergeSamePropV0Old() => RunWithStopwatch(() =>
    {
        var x = new MagicAnalyticsSettings { GtmId = "123" };
        var y = new MagicAnalyticsSettings { GtmId = "abc" };
        var result = x;
        for (var i = 0; i < Iterations; i++)
        {
            result = x.CloneUnderTac(y);
        }

        Equal("abc", result.GtmId);
    });

    [Fact]
    public void MergeSamePropV3NonGeneric() => RunWithStopwatch(() =>
    {
        var x = new V3AnalyticsSettingsObjectNonGeneric { GtmId = "123" };
        var y = new V3AnalyticsSettingsObjectNonGeneric { GtmId = "abc" };
        Dictionary<string, object> result = new();
        for (var i = 0; i < Iterations; i++)
        {
            result = x.Meta.Stack.CloneUnder(y.Meta.Stack);
        }
        Equal("abc", result[nameof(V3AnalyticsSettingsObjectNonGeneric.GtmId)].ToString());
    });

    [Fact]
    public void MergeSamePropV3NonGenericPro() => RunWithStopwatch(() =>
    {
        var x = new V3AnalyticsSettingsObjectNonGeneric { GtmId = "123" };
        var y = new V3AnalyticsSettingsObjectNonGeneric { GtmId = "abc" };
        Dictionary<string, object> result = new();
        for (var i = 0; i < Iterations; i++)
        {
            result = x.Meta.CloneUnderAlt(y.Meta.Stack);
        }
        Equal("abc", result[nameof(V3AnalyticsSettingsObjectNonGeneric.GtmId)].ToString());
    });



    [Fact]
    public void MergeDiffPropV0Old() => RunWithStopwatch(() =>
    {
        var x = new MagicAnalyticsSettings { GtmId = "123" };
        var y = new MagicAnalyticsSettings { PageViewJs = "abc" };
        var result = x;
        for (var i = 0; i < Iterations; i++)
        {
            result = x.CloneUnderTac(y);
        }

        Equal("abc", result.PageViewJs);
    });

    [Fact]
    public void MergeDiffPropV3NonGeneric() => RunWithStopwatch(() =>
    {
        var x = new V3AnalyticsSettingsObjectNonGeneric { GtmId = "123" };
        var y = new V3AnalyticsSettingsObjectNonGeneric { PageViewJs = "abc" };
        Dictionary<string, object> result = new();
        for (var i = 0; i < Iterations; i++)
        {
            result = x.Meta.Stack.CloneUnder(y.Meta.Stack);
        }
        Equal("abc", result[nameof(V3AnalyticsSettingsObjectNonGeneric.PageViewJs)].ToString());
    });

    [Fact]
    public void MergeDiffPropV3NonGenericPro() => RunWithStopwatch(() =>
    {
        var x = new V3AnalyticsSettingsObjectNonGeneric { GtmId = "123" };
        var y = new V3AnalyticsSettingsObjectNonGeneric { PageViewJs = "abc" };
        Dictionary<string, object> result = new();
        for (var i = 0; i < Iterations; i++)
        {
            result = x.Meta.CloneUnderAlt(y.Meta.Stack);
        }
        Equal("abc", result[nameof(V3AnalyticsSettingsObjectNonGeneric.PageViewJs)].ToString());
    });

}