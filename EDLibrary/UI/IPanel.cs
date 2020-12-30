using System;
using System.Collections.Generic;
using System.Text;

namespace EDLibrary.UI
{
    public interface IPanel
    {
        public void SetTextThreadSafe(int position, string text, bool invert);
        public void InvertStateThreadSafe(int position);

        public void ClearThreadSafe();
    }
}
