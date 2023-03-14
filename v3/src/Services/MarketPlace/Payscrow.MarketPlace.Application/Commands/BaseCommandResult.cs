﻿using System.Collections.Generic;

namespace Payscrow.MarketPlace.Application.Commands
{
    public class BaseCommandResult
    {
        public bool Success => Errors.Count == 0;
        public List<(string, string)> Errors { get; set; } = new List<(string, string)>();
    }
}
