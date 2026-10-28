using System;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;

public sealed class CsvTraceWriter : IDisposable
{
    private static readonly char[] SpecialChars = { ',', '"', '\n', '\r' };

    private readonly StringBuilder sb = new StringBuilder(256);
    private readonly bool autoFlush;
    private StreamWriter writer;

    public bool IsActive => writer != null;

    public CsvTraceWriter(bool autoFlush = true)
    {
        this.autoFlush = autoFlush;
    }

    public void Start(string filePath, string header)
    {
        Stop();

        try
        {
            string dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);

            Debug.Log($"Starting trace at {filePath}");
            var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            writer = new StreamWriter(stream, new UTF8Encoding(false));
            writer.WriteLine("SystemTime," + header);
            writer.Flush();
        }
        catch (Exception e)
        {
            Debug.LogError($"CsvTraceWriter failed to start: {e.Message}");
            Stop();
        }
    }

    public void Stop()
    {
        if (writer == null) return;
        Debug.Log("Stopping trace.");
        writer.Dispose();
        writer = null;
    }

    public void WriteRow(params object[] values)
    {
        if (writer == null) return;

        try
        {
            sb.Clear();
            sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture));

            foreach (var v in values)
                sb.Append(',').Append(Format(v));

            writer.WriteLine(sb.ToString());
            if (autoFlush) writer.Flush();
        }
        catch (Exception e)
        {
            Debug.LogError($"CsvTraceWriter write failed, stopping trace: {e.Message}");
            Stop();
        }
    }

    public void Flush() => writer?.Flush();

    private static string Format(object v)
    {
        string s = v is IFormattable f ? f.ToString(null, CultureInfo.InvariantCulture) : v?.ToString() ?? "";
        return s.IndexOfAny(SpecialChars) >= 0
            ? $"\"{s.Replace("\"", "\"\"")}\""
            : s;
    }

    public void Dispose() => Stop();
}