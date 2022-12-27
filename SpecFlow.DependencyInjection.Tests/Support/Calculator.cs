using System;
using System.Collections.Generic;
using System.Threading;

namespace SolidToken.SpecFlow.DependencyInjection.Tests.Support
{
    public class Calculator : ICalculator, IDisposable
    {
        private readonly Stack<int> _operands = new Stack<int>();

        public void Enter(int operand)
        {
            _operands.Push(operand);
        }

        public void Add()
        {
            _operands.Push(_operands.Pop() + _operands.Pop());
        }

        public void Multiply()
        {
            // Artificial delay for testing parallelism
            Thread.Sleep(1000);
            _operands.Push(_operands.Pop() * _operands.Pop());
        }

        public int Result
        {
            get => _operands.Peek();
        }

        public int Size
        {
            get => _operands.Count;
        }

        public override string ToString() => "[" + String.Join(",", _operands) + "]";

        public void Dispose()
        {
            // Dummy dispose for testing IDisposable
            _operands.Clear();
        }
    }
}
