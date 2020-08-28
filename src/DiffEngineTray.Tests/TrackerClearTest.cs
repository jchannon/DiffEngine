﻿using System.IO;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

public class TrackerClearTest :
    XunitContextBase
{
    [Fact]
    public async Task Simple()
    {
        await using var tracker = new RecordingTracker();
        tracker.AddDelete(file1);
        tracker.AddMove(file2, file2, "theExe", "theArguments", true, null, null);
        tracker.Clear();
        Assert.Equal(1, tracker.ActiveReceivedCount);
        Assert.Equal(1, tracker.InactiveReceivedCount);
        tracker.AssertEmpty();
    }

    public TrackerClearTest(ITestOutputHelper output) :
        base(output)
    {
        file1 = Path.GetTempFileName();
        file2 = Path.GetTempFileName();
    }

    public override void Dispose()
    {
        File.Delete(file1);
        File.Delete(file2);
        base.Dispose();
    }

    string file1;
    string file2;
}