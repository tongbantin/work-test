﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreasuryShadowSystem.Helper
{
    /// <summary>
    /// The supported operations in where-extension
    /// </summary>
    public enum ActionOperation
    {
        [StringValue("edit")]
        Edit,
        [StringValue("add")]
        Add,
        [StringValue("del")]
        Delete
    }
}
