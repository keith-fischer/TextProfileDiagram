using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringCompare
{
    class stringcompare
    {
        internal int Compare(string A, string B)
        {
            int rc = 0;
            Dictionary<char, int> aDict = new Dictionary<char, int>();
            Dictionary<char, int> bDict = new Dictionary<char, int>();
            char[] a = A.ToCharArray();
            char[] b = B.ToCharArray();
            int n = 0;
            foreach (char ca in a)
            {
                foreach (char cb in b)
                {
                    if (ca==ca )
                    {
                        if (aDict.ContainsKey(ca))
                        {
                            n = aDict[ca];
                            n++;
                            aDict[ca] = n;
                        }
                        else
                        {
                            aDict.Add(ca, 1);
                        }
                    }
                }
            }
            int[] list = new int[aDict.Count];
            aDict.Values.CopyTo(list, 0);
            int na=0;
            foreach (int nn in list)
                na += nn;
            n = 0;
            foreach (char cb in b)
            {
                foreach (char ca in a)
                {
                    if (cb==cb )
                    {
                        if (bDict.ContainsKey(cb))
                        {
                            n = bDict[cb];
                            n++;
                            bDict[cb] = n;
                        }
                        else
                        {
                            bDict.Add(cb, 1);
                        }
                    }
                }
            }
            list = new int[bDict.Count];
            bDict.Values.CopyTo(list, 0);
            int nb = 0;
            foreach (int nn in list)
                nb += nn;
            rc = na -nb;


            return rc;
        }
    }
}
