﻿// Copyright 2013-2015 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Seq.Extensions.Logging;

/// <summary>
/// A simple source of information generated the logging provider,
/// for example when exceptions are thrown and caught internally.
/// </summary>
public static class SelfLog
{
    static Action<string>? _output;

    /// <summary>
    /// Set the output mechanism for self-log messages.
    /// </summary>
    /// <param name="output">A synchronized <see cref="TextWriter"/> to which
    /// self-log messages will be written.</param>
    // ReSharper disable once MemberCanBePrivate.Global
    public static void Enable(TextWriter output)
    {
        if (output == null) throw new ArgumentNullException(nameof(output));

        Enable(m =>
        {
            output.WriteLine(m);
            output.Flush();
        });
    }

    /// <summary>
    /// Set the output mechanism for self-log messages.
    /// </summary>
    /// <param name="output">An action to invoke with self-log messages.</param>
    /// // ReSharper disable once MemberCanBePrivate.Global
    public static void Enable(Action<string> output)
    {
        _output = output ?? throw new ArgumentNullException(nameof(output));
    }

    /// <summary>
    /// Clear the output mechanism and disable self-log events.
    /// </summary>
    /// // ReSharper disable once MemberCanBePrivate.Global
    public static void Disable()
    {
        _output = null;
    }

    /// <summary>
    /// Write a message to the self-log.
    /// </summary>
    /// <param name="format">Standard .NET format string containing the message.</param>
    /// <param name="arg0">First argument, if supplied.</param>
    /// <param name="arg1">Second argument, if supplied.</param>
    /// <param name="arg2">Third argument, if supplied.</param>
    /// <remarks>
    /// The name is historical; because this is used from third-party sink packages, removing the "Line"
    /// suffix as would seem sensible isn't worth the breakage.
    /// </remarks>
    public static void WriteLine(string format, object? arg0 = null, object? arg1 = null, object? arg2 = null)
    {
        var o = _output;

        o?.Invoke(string.Format(DateTime.UtcNow.ToString("o") + " " + format, arg0, arg1, arg2));
    }
}