using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matching_Game
{
    static class ListExtensions
    {
        static public void Add<T>(this List<T> list, params T[] additions)
        {
            foreach (T addition in additions)
            {
                list.Add(addition);
            }
        }
    }
}