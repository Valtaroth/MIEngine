﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace MICore
{
    public delegate void OnCommand(string cmd);

    public interface ITransport
    {
        void Init(ITransportCallback transportCallback, LaunchOptions options);
        void Send(string cmd);
        void Close();
    }

    /// <summary>
    /// Interface implemented by the Debugger class to recieve notifications from the transport
    /// </summary>
    public interface ITransportCallback
    {
        /// <summary>
        /// Fired when a line of text is sent by the MI debugger over stdout
        /// </summary>
        /// <param name="line">[Required] Line of text that the target program wrote</param>
        void OnStdOutLine(string line);

        /// <summary>
        /// Fired when a line of text is sent by the MI debugger (or plink.exe or similar
        /// program connecting us to the MI debugger) over stderr.
        /// 
        /// ***NOTE*** at least using plink, if the target debugger/shell script writes
        /// text on unix to stderr, it still shows up in stdout on the Windows side.
        /// </summary>
        /// <param name="line">[Required] Line of text that the target program wrote</param>
        void OnStdErrorLine(string line);

        /// <summary>
        /// Fired when either the target process exits or when the stdout stream is closed.
        /// </summary>
        void OnDebuggerProcessExit();

        /// <summary>
        /// Appends a line of text to the initialization log which is dumped to the output
        /// window on a launch error.
        /// </summary>
        /// <param name="line">[Required] line of text to write</param>
        void AppendToInitializationLog(string line);
    };
}
