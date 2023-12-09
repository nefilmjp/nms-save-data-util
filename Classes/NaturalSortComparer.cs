using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSSaveDataUtil.Classes
{
    internal class NaturalSortComparer : System.Collections.IComparer
    {
        private readonly Dictionary<string, string[]> table;
        private readonly string columnName;

        public NaturalSortComparer(string columnName)
        {
            this.columnName = columnName;
            table = new Dictionary<string, string[]>();
        }

        public void Dispose()
        {
            table.Clear();
        }

        public int Compare(object? x, object? y)
        {
            if (x is null || y is null) { return 0; }

            DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
            DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

            string? xStr = DataGridViewRow1.Cells[columnName].Value.ToString();
            string? yStr = DataGridViewRow2.Cells[columnName].Value.ToString();

            // System.Diagnostics.Debug.WriteLine(xStr);
            // System.Diagnostics.Debug.WriteLine(yStr);

            if (xStr is null || yStr is null || xStr == yStr) { return 0; }
            if (xStr == "accountdata.hg") { return -1; }
            if (yStr == "accountdata.hg") { return 1; }
            if (xStr == "save.hg") { return -1; }
            if (yStr == "save.hg") { return 1; }

            System.Text.RegularExpressions.Regex pattern = new(@"^save([0-9]+).hg$");

            System.Text.RegularExpressions.Match xMatch = pattern.Match(xStr);
            System.Text.RegularExpressions.Match yMatch = pattern.Match(yStr);

            System.Diagnostics.Debug.WriteLine(xMatch.Groups.Count);

            if (!xMatch.Success || xMatch.Groups.Count < 2) { return 1; }
            if (!yMatch.Success || yMatch.Groups.Count < 2) { return -1; }

            _ = int.TryParse(xMatch.Groups[1].Value, out int xInt);
            _ = int.TryParse(yMatch.Groups[1].Value, out int yInt);

            System.Diagnostics.Debug.WriteLine(xInt);

            return xInt - yInt;
        }
    }
}
