using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallBookLibrary.Test
{
    public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;

        public TestAsyncEnumerator(IEnumerator<T> enumerator)
        {
            _enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
        }

        public T Current => _enumerator.Current;

        public async ValueTask<bool> MoveNextAsync()
        {
            await Task.Yield(); // Simulate asynchronous operation
            return _enumerator.MoveNext();
        }

        public ValueTask DisposeAsync()
        {
            _enumerator.Dispose();
            return default;
        }
    }
}
