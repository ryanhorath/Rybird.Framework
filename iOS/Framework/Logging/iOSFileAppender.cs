using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Rybird.Framework
{
    //public class iOSFileAppender : BaseAppender
    //{
    //    private readonly string _folderName;
    //    private readonly CommonStorageLocations _storageLocations = new CommonStorageLocations();

    //    private ICommonStorageFolder _logFolder;
    //    private ICommonStorageFile _currentLogFile;
    //    private readonly object _lock = new object();

    //    private Task _initializeTask;

    //    public iOSFileAppender(string folderName = "ProTips")
    //    {
    //        _folderName = folderName;
    //    }

    //    public Task InitializeAsync()
    //    {
    //        // Only run task once. See http://blogs.msdn.com/b/pfxteam/archive/2011/10/24/10229468.aspx.
    //        return _initializeTask ?? (_initializeTask = Task.Run(
    //                async () =>
    //                {
    //                    _logFolder =
    //                            await
    //                            _storageLocations.LogsFolder.CreateFolderAsync(_folderName, CommonCreationCollisionOption.OpenIfExists);

    //                    var nowShort = DateTime.Now.ToString("yyyy-MM-dd-HH-mm");
    //                    _currentLogFile =
    //                            await
    //                            _logFolder.CreateFileAsync(
    //                                    string.Format("logs{0}.txt", nowShort),
    //                                    CommonCreationCollisionOption.OpenIfExists);
    //                }));
    //    }

    //    public override void Append(string message, AppenderData data)
    //    {
    //        var shortenedName = data.FilePath.Split('\\').Last();
    //        var toAppend = string.Format(
    //                "[{4},{5}]{0} - {1}[{2}] => {3}",
    //                shortenedName,
    //                data.MemberName,
    //                data.LineNumber,
    //                message,
    //                data.Severity,
    //                data.CategoryName);

    //        lock (_lock)
    //        {
    //            if (_currentLogFile == null)
    //            {
    //                return;
    //            }
    //            var info = new FileInfo(_currentLogFile.Path);
    //            using (var sw = info.AppendText())
    //            {
    //                sw.WriteLine(toAppend + Environment.NewLine);
    //            }
    //        }
    //    }
    //}
}