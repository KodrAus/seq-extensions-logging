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

using Serilog.Events;

namespace Serilog.Parsing;

/// <summary>
/// An element parsed from a message template string.
/// </summary>
abstract class MessageTemplateToken
{
    /// <summary>
    /// Render the token to the output.
    /// </summary>
    /// <param name="properties">Properties that may be represented by the token.</param>
    /// <param name="output">Output for the rendered string.</param>
    /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
    public abstract void Render(IReadOnlyDictionary<string, LogEventPropertyValue> properties, TextWriter output, IFormatProvider? formatProvider = null);
}