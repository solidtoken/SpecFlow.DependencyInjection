using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolidToken.SpecFlow.DependencyInjection.Tests.Support
{
    public class Calculator : ICalculator
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
            Thread.Sleep(1000);
            _operands.Push(_operands.Pop() * _operands.Pop());
        }

        public int Result
        {
            get => _operands.Peek();
        }
    }
}
