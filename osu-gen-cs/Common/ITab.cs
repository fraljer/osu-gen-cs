using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Common
{
    // won't use this. yet.
    internal interface ITab
    {
        string TabLabel { get; }
        string TabName { get; }
        void OnSelect(); // self-explanatory
        void Insides(); // content
        void OnDeselect(); // ditto
    }
}
