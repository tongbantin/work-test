using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreasuryShadowSystem.Model
{
    public class Utilities
    {
        public static int[] GetRowColumn(string CellName)
        {
            try
            {
                string ch = "0ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                int[] xcel = new int[2];
                xcel[0] = Int16.Parse(CellName.Substring(1)) - 1;
                xcel[1] = ch.IndexOf(CellName.Substring(0, 1)) - 1;
                return xcel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
