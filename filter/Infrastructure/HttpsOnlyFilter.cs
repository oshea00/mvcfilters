using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filter.Infrastructure
{
    public interface IHttpsOnlyFilter
    {
        bool CheckForHttps(bool isHttps);
    }

    public class HttpsOnlyFilter : IHttpsOnlyFilter
    {
        public bool CheckForHttps(bool isHttps)
        {
            return !isHttps;
        }
    }
}
