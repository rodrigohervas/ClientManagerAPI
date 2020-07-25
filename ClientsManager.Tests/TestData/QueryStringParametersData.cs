using ClientsManager.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Tests.TestData
{
    /// <summary>
    /// Returns QueryStringParameters objects to test methods that need Paging parameters of type QueryStringParameters
    /// To be used with xUnit.ClassData attribute
    /// </summary>
    public class QueryStringParametersData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new QueryStringParameters() { pageNumber = 1, pageSize = 2 } };
            yield return new object[] { new QueryStringParameters() { pageNumber = 2, pageSize = 2 } };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
