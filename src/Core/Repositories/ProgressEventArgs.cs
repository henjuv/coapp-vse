﻿using System;

namespace CoApp.VsExtension
{
    public class ProgressEventArgs : EventArgs
    {
        public ProgressEventArgs(int percentComplete)
            : this(null, percentComplete)
        {
        }

        public ProgressEventArgs(string operation, int percentComplete)
        {
            Operation = operation;
            PercentComplete = percentComplete;
        }

        public string Operation { get; private set; }
        public int PercentComplete { get; private set; }
    }
}