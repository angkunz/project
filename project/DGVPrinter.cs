using System;

namespace project
{
    internal class DGVPrinter
    {
        public object Title { get; internal set; }
        public object SubTitle { get; internal set; }
        public object SubTitleFormatflags { get; internal set; }
        public object PageNumbers { get; internal set; }
        public object PageNumbersInHeader { get; internal set; }
        public object PorportionalColumns { get; internal set; }
        public object HeaderCallAlignment { get; internal set; }
        public string Footer { get; internal set; }
        public int FooterSpacing { get; internal set; }

        internal void PrintDataGridView(System.Windows.Forms.DataGridView dataGridView) => throw new NotImplementedException();
    }
}